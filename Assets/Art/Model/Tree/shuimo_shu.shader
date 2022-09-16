Shader "dqw/shuimo_tree"
{
    Properties{
    
     _MainTex ("BaseColor", 2D) = "white" {}
     _Ramp ("rampTexture", 2D) = "white"{}
     _StrokeTex ("内部噪声贴图1", 2D) = "white" {}
     _InteriorNoise ("内部噪声贴图2", 2D) = "white" {}
     _NoiseLevel ("噪声扰动程度", range(0, 1)) = 1
     _weight ("混合权重", range(0, 1)) = 0.5
     _resolution ("Resolution", float) = 100
     _Thred("Edge Thred" , Range(0.01,1)) = 0.25
	 _Range("Edge Range" , Range(1,10)) = 1		
	 _Pow("Edge Intensity",Range(0,10))=1
     _outlineColor("描边颜色", color) = (0, 0, 0, 0)
        
    }
    SubShader{
    Tags{  "RenderPipeline"="UniversalPipeline" //urp
        "RenderType" = "Opaque" "Queue" = "Geometry"}

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        //除了贴图外，将暴露在Inspector面板上的变量缓存到CBUFFER中
        CBUFFER_START(UnityPerMaterial)
        float4 _Ramp_ST;
        float4 _StrokeTex_ST;
        float _NoiseLevel;
        float _resolution;
        float _Thred;
        float _Range;
        float _Pow;
        float4 _outlineColor;
        float _weight;
        CBUFFER_END
        ENDHLSL 

    pass {
        Tags { "LightMode" = "UniversalForward" }

        HLSLPROGRAM

        #pragma vertex vert
        #pragma fragment frag

        //声明变量
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        TEXTURE2D(_Ramp);
        SAMPLER(sampler_Ramp);        
        TEXTURE2D(_StrokeTex);
        SAMPLER(sampler_StrokeTex);        
        TEXTURE2D(_InteriorNoise);
        SAMPLER(sampler_InteriorNoise);
        
        float radius;
        
        float hstep;
        float vstep;
                 
         
        struct a2v
        {
            float4 vertex : POSITION;  //模型空间顶点
            float3 normal : NORMAL;   //模型空间法线
            float4 uv0 : TEXCOORD0;  //uv
        };

        struct v2f
        {
            float4 pos : SV_POSITION; //裁剪空间顶点
            float3 nDirWS : TEXCOORD0; //世界法向量
            float2 uv1 : TEXCOORD1; //uv传递
            float2 uv2 : TEXCOORD3; //uv传递
            float3 posWS : TEXCOORD2; //世界空间顶点坐标
            float vdotn : TEXCOORD4; //模型空间视角方向点乘法向量
        };
        v2f vert(a2v v) {
            v2f o;
            o.pos = TransformObjectToHClip(v.vertex.xyz);  //裁剪空间顶点坐标
            o.uv1 = v.uv0.xy * _Ramp_ST.xy + _Ramp_ST.zw;  //噪声2贴图uv
            o.uv2 = v.uv0.zw * _StrokeTex_ST.xy + _StrokeTex_ST.zw; //内部噪声1uv
            o.nDirWS = TransformObjectToWorldNormal(v.normal); //世界法向量
            o.posWS = TransformObjectToWorld( v.vertex.xyz); // 世界顶点坐标
            float3 viewDir = normalize( TransformWorldToObjectDir(GetWorldSpaceViewDir(o.posWS))); //模型空间视角方向
            o.vdotn = dot(normalize(viewDir), v.normal); //模型空间观察方向与法线点乘

            return o;
        }

        float4 frag(v2f i) : SV_Target 
			{ 
				half3 worldNormal = normalize(i.nDirWS);
                Light light = GetMainLight();
				half3 worldLightDir = light.direction;
                 //半兰伯特
				half diff =  dot(worldNormal, worldLightDir);
				diff = (diff * 0.5 + 0.5);
                //噪声扰动uv
                
                float2 burn = SAMPLE_TEXTURE2D(_InteriorNoise, sampler_InteriorNoise,i.uv1).xy;
				float2 k = SAMPLE_TEXTURE2D(_StrokeTex, sampler_StrokeTex,i.uv2).xy;
				float2 cuv = float2(diff, diff) + k * burn * _NoiseLevel;

				if (cuv.x > 0.95)
				{
					cuv.x = 0.95;
					cuv.y = 1;
				}
				if (cuv.y >  0.95)
				{
					cuv.x = 0.95;
					cuv.y = 1;
				}
				cuv = clamp(cuv, 0, 1);

				//高斯模糊
				float4 sum = float4(0.0, 0.0, 0.0, 0.0);
                float2 tc = cuv;
                radius = 30;
                
                hstep = 0.5;
                vstep = 0.5;
                float blur = radius/_resolution/32;     
                sum += SAMPLE_TEXTURE2D(_Ramp, sampler_Ramp, float2(tc.x - 4.0*blur*hstep, tc.y - 4.0*blur*vstep)) * 0.0162162162;
                sum += SAMPLE_TEXTURE2D(_Ramp, sampler_Ramp, float2(tc.x - 3.0*blur*hstep, tc.y - 3.0*blur*vstep)) * 0.0540540541;
                sum += SAMPLE_TEXTURE2D(_Ramp, sampler_Ramp, float2(tc.x - 2.0*blur*hstep, tc.y - 2.0*blur*vstep)) * 0.1216216216;
                sum += SAMPLE_TEXTURE2D(_Ramp, sampler_Ramp, float2(tc.x - 1.0*blur*hstep, tc.y - 1.0*blur*vstep)) * 0.1945945946;
                sum += SAMPLE_TEXTURE2D(_Ramp, sampler_Ramp, float2(tc.x, tc.y)) * 0.2270270270;
                sum += SAMPLE_TEXTURE2D(_Ramp, sampler_Ramp, float2(tc.x + 1.0*blur*hstep, tc.y + 1.0*blur*vstep)) * 0.1945945946;
                sum += SAMPLE_TEXTURE2D(_Ramp, sampler_Ramp, float2(tc.x + 2.0*blur*hstep, tc.y + 2.0*blur*vstep)) * 0.1216216216;
                sum += SAMPLE_TEXTURE2D(_Ramp, sampler_Ramp, float2(tc.x + 3.0*blur*hstep, tc.y + 3.0*blur*vstep)) * 0.0540540541;
                sum += SAMPLE_TEXTURE2D(_Ramp, sampler_Ramp, float2(tc.x + 4.0*blur*hstep, tc.y + 4.0*blur*vstep)) * 0.0162162162;

                sum = lerp(sum, SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex,i.uv1),_weight);

                //描边
                half edge = pow(i.vdotn, 2) / _Range;  //
                edge = edge > _Thred ? 1 : edge;
                // edge = SAMPLE_TEXTURE2D(_InteriorNoise, sampler_InteriorNoise,i.uv1).x *edge;
	            edge = pow(abs(edge), _Pow);
	            half4 edgeColor = half4(edge, edge, edge, edge);
                // edgeColor = edgeColor * SAMPLE_TEXTURE2D(_InteriorNoise, sampler_InteriorNoise,i.uv1).x;
                //混合
                float4 col = edgeColor * (1 - edgeColor.a) * _outlineColor + sum * (edgeColor.a);

				return float4(col.rgb, 1.0);
        }
        ENDHLSL
    }
    }
 
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace Scripts.Fog
{
    public class FogManager : MonoBehaviour
    {
        public VisualEffect vfxRenderer;

        [SerializeField] Transform player;
        
        private void Update()
        {
            vfxRenderer.SetVector3("ColliderPos", player.position);
        }

    }
}


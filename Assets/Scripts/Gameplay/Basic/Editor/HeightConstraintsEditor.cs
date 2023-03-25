using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Scripts.Gameplay.Basic
{
    [CustomEditor(typeof(HeightConstraints))]
    public class HeightConstraintsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            HeightConstraints hc = target as HeightConstraints;
            base.OnInspectorGUI();
            if(GUILayout.Button("Set current as higher constraint"))
            {
                hc.higherConstraint = hc.transform.position.y;
            }
            if(GUILayout.Button("Set current as lower constraint"))
            {
                hc.lowerConstraint = hc.transform.position.y;
            }
        }
    }
}
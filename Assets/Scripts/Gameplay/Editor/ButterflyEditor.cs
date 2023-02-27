using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Scripts.Gameplay.Chase
{
    [CustomEditor(typeof(Butterfly))]
    public class ButterflyEditor : Editor
    {
        private void OnSceneGUI()
        {
            Butterfly butterfly = target as Butterfly;
            Quaternion q = butterfly.transform.rotation;
            if (butterfly.positions.Count > 0)
            {
                Handles.DrawPolyLine(butterfly.positions.ToArray());
                Handles.color = Color.red;
                Handles.SphereHandleCap(0, butterfly.positions[0], q, .1f, EventType.Repaint);
                Handles.color = Color.black;
                Handles.SphereHandleCap(0, butterfly.positions[butterfly.positions.Count - 1], q, .1f, EventType.Repaint);
                for (int i = 0; i < butterfly.positions.Count; ++i)
                {
                    butterfly.positions[i] = Handles.PositionHandle(butterfly.positions[i], q);
                }
            }
        }
    }
}
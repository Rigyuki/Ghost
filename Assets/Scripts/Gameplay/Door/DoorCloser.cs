using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.CustomTool.DesignPatterns;
namespace Scripts.Gameplay.Door
{
    public class DoorCloseSubject : SubjectSingleton<DoorCloseSubject> { }
    [RequireComponent(typeof(Collider))]
    public class DoorCloser : MonoBehaviour
    {
        public int area;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
                return;
            DoorCloseSubject.Instance.Notify(area);
        }
    }
}
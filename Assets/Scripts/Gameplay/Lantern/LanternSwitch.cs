using System.Collections;
using UnityEngine;

namespace Scripts.Gameplay.Lantern
{
    public class LanternSwitch : MonoBehaviour
    {
        public LanternEnum lanternGroup;
        public int lanternGroupInt { get => (int)lanternGroup; }
        public MeshRenderer meshRenderer;
        public Material onMat;
        public Material offMat;
        public bool isOn { get; private set; }

        private void OnEnable()
        {
            LanternSwitchSubject.Instance.Register(SwitchLantern);
        }
        private void OnDisable()
        {
            LanternSwitchSubject.Instance.Unregister(SwitchLantern);
        }

        void SwitchLantern(object target)
        {
            int targetInt = (int)target;
            if ((lanternGroupInt & targetInt) != 0)
            {
                if (isOn)
                    SwitchOff();
                else
                    SwitchOn();
            }
            LanternSwitchSubject.Instance.allLanternOn &= isOn;
        }
        void SwitchOn()
        {
            isOn = true;
            meshRenderer.material = onMat;
        }
        void SwitchOff()
        {
            isOn = false;
            meshRenderer.material = offMat;
        }
    }
}
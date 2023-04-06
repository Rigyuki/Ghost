using System.Collections;
using UnityEngine;

namespace Scripts.Gameplay.Lantern
{
    public class LanternSwitch : MonoBehaviour
    {
        public LanternEnum lanternGroup;
        public int lanternGroupInt { get => (int)lanternGroup; }
        //public MeshRenderer meshRenderer;
        public SpriteRenderer spriteRenderer;
        //public Material onMat;
        public Sprite onSprite;
        //public Material offMat;
        public Sprite offSprite;
        public bool isOn { get; private set; }

        private void OnEnable()
        {
            LanternSwitchSubject.Instance.Register(SwitchLantern);
            LanternResetSubject.Instance.Register(SwitchOffForObserver);
        }
        private void OnDisable()
        {
            LanternSwitchSubject.Instance.Unregister(SwitchLantern);
            LanternResetSubject.Instance.Unregister(SwitchOffForObserver);
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
            //meshRenderer.material = onMat;
            spriteRenderer.sprite = onSprite;
        }
        void SwitchOff()
        {
            isOn = false;
            //meshRenderer.material = offMat;
            spriteRenderer.sprite = offSprite;
        }
        void SwitchOffForObserver(object arg)
        {
            SwitchOff();
        }
    }
}
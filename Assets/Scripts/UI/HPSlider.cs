using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    [RequireComponent(typeof(Slider))]
    public class HPSlider : MonoBehaviour
    {
        Slider slider;
        private void Awake()
        {
            slider = GetComponent<Slider>();
            HPChangeSubject.Instance.Register(SetSlider);
        }
        private void OnDestroy()
        {
            HPChangeSubject.Instance.Unregister(SetSlider);
        }
        void SetSlider(object percentage)
        {

            slider.value = (float)percentage;
        }
    }
}
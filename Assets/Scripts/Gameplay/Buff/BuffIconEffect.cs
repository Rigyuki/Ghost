using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Gameplay.BuffSystem
{
    // TODO����������ʾ��������
    public class BuffIconEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] float offsetX = 0;
        [SerializeField] float offsety = 0;
        [SerializeField] Image infoImage;
        [SerializeField] Text _skilDescription;

        private void Start()
        {
            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
             
    
        }

        public void OnPointerExit(PointerEventData eventData)
        {
             
        }        

    }
}



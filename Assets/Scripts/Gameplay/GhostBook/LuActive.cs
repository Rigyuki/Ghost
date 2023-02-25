using Scripts.CustomTool.DesignPatterns.ObserverPattern;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Gameplay.GhostBook
{
    [DisallowMultipleComponent]
    public class LuActive : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
    {
    
        [SerializeField] private LuType luType;


        public void OnPointerClick(PointerEventData eventData)
        {

            UnityEngine.Debug.Log($"当前{name}被点击了");
            this.gameObject.SetActive(false);

            //InventoryManager.Instance.addItem(luType);
            MsgCenterByList.SendMessage(new CommonMsg()
            {                
                MsgId = MsgCenterByList.COLLECTION_LU,
                intParam = (int)luType
            });
        }

 

        public void OnPointerUp(PointerEventData eventData)
        {
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }
    }

}

    

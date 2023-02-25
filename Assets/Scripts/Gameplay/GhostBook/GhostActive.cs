using Scripts.CustomTool.DesignPatterns.ObserverPattern;
using Scripts.Gameplay.GhostBook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Scripts.Gameplay.GhostBook {

    [DisallowMultipleComponent]
    public class GhostActive : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
    {

        [SerializeField] private GhostType ghostType;


        public void OnPointerClick(PointerEventData eventData)
        {

            UnityEngine.Debug.Log($"当前{name}被点击了");
            this.gameObject.SetActive(false);

            //InventoryManager.Instance.addItem(luType);
            MsgCenterByList.SendMessage(new CommonMsg()
            {
                MsgId = MsgCenterByList.COLLECTION_GHOST,
                intParam = (int)ghostType
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
    


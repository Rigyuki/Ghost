using Scripts.CustomTool.DesignPatterns.ObserverPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Gameplay.GhostBook {
    public class GBook : MonoBehaviour
    {
        [SerializeField]
        private GhostType _rightGhostType;

        public Image showImage;
        //public List<Image> ghostImages = new List<Image>();

        private GhostItemDetails  currentDetails;

        public  ItemDataList_SO itemData;

        private void Start()
        {
            showImage=GetComponent<Image>();
        }

        private void OnEnable()
        {            
            MsgCenterByList.AddListener(OnMsg);
        }

        private void OnDisable()
        {
            MsgCenterByList.RemoveListener(OnMsg);
        }

        private void OnMsg(CommonMsg obj)
        {
            if (obj.MsgId != MsgCenterByList.COLLECTION_GHOST) return;
            OnGhostActive((GhostType)obj.intParam); 
        }

        public void OnGhostActive(GhostType ghostType)
        {
            if(ghostType == _rightGhostType)
            {
                //TODO: 激活妖怪类型确认之后
                setItem(itemData.GetItemDetails(ghostType));
                 //itemData.GetItemDetails(ghostType);
            }
        }

        public void setItem(GhostItemDetails itemDetails)
        {
            currentDetails = itemDetails;
            showImage.sprite = itemDetails.gSprite;
            showImage.SetNativeSize();

        }

    }

}



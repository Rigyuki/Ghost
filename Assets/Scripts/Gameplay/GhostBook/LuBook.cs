using Scripts.CustomTool.DesignPatterns.ObserverPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Gameplay.GhostBook
{
    public class LuBook : MonoBehaviour
    {
        [SerializeField] private LuType _rightLuType;

        private LuItemDetails currentDetails;

        public Image showImage;

        public LuItemDataList_SO luItemData;
        private void Awake()
        {
            
        }

        private void Start()
        {
            //getImageAlpha.color = new Color(1, 1, 1, 0);
            showImage = GetComponent<Image>();
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
            //Debug.Log("msg");
            if (obj.MsgId != MsgCenterByList.COLLECTION_LU) return;
            OnLuActive((LuType)obj.intParam);
        }

        public void OnLuActive(LuType luType)
        {
            if (luType == _rightLuType)
            {
                //TODO: 激活禄类型确认之后
                //Debug.Log(luType.ToString());                
                setItem(luItemData.GetLuItemDetails(luType));
            }            

        }

        public void setItem(LuItemDetails itemDetails)
        {
            currentDetails = itemDetails;
            showImage.sprite = itemDetails.luSprite;
            showImage.SetNativeSize();

        }

    }

}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.CustomTool.DesignPatterns.ObserverPattern;

namespace Scripts.Gameplay.Lantern {

    public class Scene1Lantern : MonoBehaviour
    {
      
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
            if (obj.MsgId == MsgCenterByList.SAFE_DOOR_OPEN)
            {
                
                this.gameObject.SetActive(false);
                Debug.Log(obj.MsgId);

            }
        }
    }

}


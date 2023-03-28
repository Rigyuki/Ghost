using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.CustomTool.DesignPatterns.ObserverPattern;

namespace Scripts.Gameplay.Lantern {

    public class Scene1Lantern : MonoBehaviour
    {
        public void changeLanternScene(bool isChange)
        {
            if (isChange)
            {
                SceneManager.UnloadSceneAsync("TestScene1Main");
                SceneManager.LoadSceneAsync("Scene1_Lantern");
            }
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
            if (obj.MsgId == MsgCenterByList.SAFE_DOOR_OPEN)
            {
                SceneManager.LoadSceneAsync("TestScene1Main");
                SceneManager.UnloadSceneAsync("Scene1_Lantern");
            }
        }
    }

}


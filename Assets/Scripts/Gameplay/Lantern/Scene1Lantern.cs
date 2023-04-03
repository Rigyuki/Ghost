using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.CustomTool.DesignPatterns.ObserverPattern;

namespace Scripts.Gameplay.Lantern {

    public class Scene1Lantern : MonoBehaviour
    {
       // [SerializeField] Scene _Scene1_Lantern => SceneManager.GetSceneByName("Scene1_Lantern");
        public void changeLanternScene(bool isChange)
        {
            if (isChange)
            {
                 
               // SceneManager.LoadScene("Scene1_Lantern", LoadSceneMode.Additive);
                
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
                //SceneManager.UnloadSceneAsync("Scene1_Lantern");
                this.gameObject.SetActive(false);
                Debug.Log(obj.MsgId);

            }
        }
    }

}


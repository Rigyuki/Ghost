using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.CustomTool.DesignPatterns.ObserverPattern;

namespace Scripts.Gameplay.Lantern {

    public class Scene1Lantern : MonoBehaviour
    {
        [SerializeField] private GameObject player_Scene1;
         [SerializeField] Scene _Scene1_Lantern => SceneManager.GetSceneByName("Scene1_Lantern");
        public void changeLanternScene(bool isChange)
        {
            if (isChange)
            {
                 player_Scene1.gameObject.SetActive(false);
                 SceneManager.LoadScene("Scene1_Lantern", LoadSceneMode.Additive);
                
            }
        }

        private void playerScene()
        {
            Scene scene = SceneManager.GetActiveScene();
            if(scene.name== "Scene1_Lantern")
            {
                player_Scene1.gameObject.SetActive(false);
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
                player_Scene1.gameObject.SetActive(true);
                SceneManager.UnloadSceneAsync("Scene1_Lantern");
                //this.gameObject.SetActive(false);
                Debug.Log(obj.MsgId);

            }
        }
    }

}


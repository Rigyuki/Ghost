using Scripts.CustomTool.EditorTools;
using Scripts.Gameplay.Basic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Gameplay.SceneChange
{
    public class SceneChange : MonoBehaviour
    {
        public string _SceneName;

        

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.Log("1");
                ChangeScene(_SceneName);
            }

        }
        public void ChangeScene(string sceneName)
        {            
                SceneManager.LoadSceneAsync(sceneName);         
        }     
    }
}



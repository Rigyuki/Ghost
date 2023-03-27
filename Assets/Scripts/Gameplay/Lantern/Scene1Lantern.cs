using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Gameplay.Lantern {

    public class Scene1Lantern : MonoBehaviour
    {

        private void Update()
        {
            
        }

        public void changeLanternScene(bool isChange)
        {
            if (isChange)
            {
                SceneManager.UnloadSceneAsync("TestScene1Main");
                SceneManager.LoadSceneAsync("Scene1_Lantern");
            }
        }



    }

}


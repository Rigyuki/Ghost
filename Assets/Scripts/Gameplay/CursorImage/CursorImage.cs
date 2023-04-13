using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Scripts.Gameplay.CursorImage
{
    public class CursorImage : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler,IPointerClickHandler
    {
        [SerializeField] Image gameImg;
         
      
        //private GameObject go;
        string goName;
        private void Update()
        {
            goName=this.name.ToString();
            /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                go = hitInfo.collider.gameObject;
                goName = go.name.ToString();
                Debug.Log(goName);
            }    
                */
        }

        /*    private void OnMouseEnter()
            {
                startImg.sprite = Resources.Load($"GameStart/{goName}_Cursor", typeof(Sprite)) as Sprite;

            }

            private void OnMouseExit()
            {
                startImg.sprite = Resources.Load($"GameStart/{goName}", typeof(Sprite)) as Sprite;
            }*/

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("Àë¿ª");
            if (goName == "StartGame" || goName =="QuitGame")
                gameImg.sprite = Resources.Load($"GameStart/{goName}", typeof(Sprite)) as Sprite;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (goName == "StartGame" || goName == "QuitGame")
                gameImg.sprite = Resources.Load($"GameStart/{goName}_Cursor", typeof(Sprite)) as Sprite;
            Debug.Log("½øÈë");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(goName == "StartGame")
            {
                SceneManager.LoadSceneAsync("MainScene");
            }else if(goName == "QuitGame")
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
            }

        }


    }

}


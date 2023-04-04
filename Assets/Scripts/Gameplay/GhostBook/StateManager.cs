using Scripts.CustomTool.DesignPatterns.ObserverPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Gameplay.GhostBook
{
    // TODO: find object with correct name...change sprite to correct file name 
    public class StateManager : MonoBehaviour
    {
        private Image img_Book;
        private Image img_Taichi;

        private Button btn_book;
        private Button btn_Taichi;
        private void Start()
        {
            img_Book = GameObject.Find("Canvas/btn_OpenBook").GetComponent<Image>();
            //img_Taichi = GameObject.Find("Canvas/Taichi").GetComponent<Image>();

            btn_book = GameObject.Find("Canvas/btn_OpenBook").GetComponent<Button>();
            btn_book.onClick.AddListener(OnStartBtnBookClick);

           /* btn_Taichi = GameObject.Find("btn_Start").GetComponent<Button>();
            btn_Taichi.onClick.AddListener(OnStartBtnTaichiClick);*/
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
            if (obj.MsgId == MsgCenterByList.COLLECTION_GHOST || obj.MsgId== MsgCenterByList.COLLECTION_LU)
            {
                img_Book.sprite = Resources.Load("GhostBook/UnreadBook", typeof(Sprite)) as Sprite;
            }

            // TODO: change taichi image
            /*if (obj.MsgId == MsgCenterByList.COLLECTION_GHOST)
            {
                img_Taichi.sprite = Resources.Load("GhostBook/UnreadTaichi", typeof(Sprite)) as Sprite;
            }*/
        }

       private void OnStartBtnBookClick()
        {
            img_Book.sprite = Resources.Load("GhostBook/NormalBook", typeof(Sprite)) as Sprite;
        }

      /*  private void OnStartBtnTaichiClick()
        {
            img_Taichi.sprite = Resources.Load("GhostBook/NormalTaichi", typeof(Sprite)) as Sprite;

        }*/

    }

}


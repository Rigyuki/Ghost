using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Gameplay.GhostBook
{
    public class OpenOrClose : MonoBehaviour
    {
        private Button exitButton;
        private Button openBookButton;

        private Button openTaichiTrigramsButton;
        [SerializeField] private CanvasGroup GhostBook;
        [SerializeField] private CanvasGroup TaichiTrigrams;

        private bool isBookOpen;
        private void Start()
        {
            openBookButton = GameObject.Find("Canvas/btn_OpenBook").GetComponent<Button>();
            exitButton = GameObject.Find("Canvas/GhostBook/btn_Exit").GetComponent<Button>();
            openTaichiTrigramsButton = GameObject.Find("Canvas/btn_Taichi").GetComponent<Button>();

            exitButton.onClick.AddListener(delegate { OpenGhostBook(0); });
            openBookButton.onClick.AddListener(delegate { OpenGhostBook(1); });
            openTaichiTrigramsButton.onClick.AddListener(delegate { OpenTaichiTrigrams(); });

            
            isBookOpen = TaichiTrigrams.alpha == 0 ? false : true;
        }

        public  void OpenGhostBook(int i)
        {
            GhostBook.alpha = i;             
        }

        public void OpenTaichiTrigrams()
        {
            isBookOpen = !isBookOpen;
            if (isBookOpen)
            {
                TaichiTrigrams.alpha = 1;                
            }
            else TaichiTrigrams.alpha = 0;
        }
    }
}


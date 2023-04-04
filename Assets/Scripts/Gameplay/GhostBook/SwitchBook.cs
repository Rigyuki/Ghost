using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Gameplay.GhostBook {
    public class SwitchBook : MonoBehaviour
    {
        public Image showImage;
        public List<Image> ghostImages = new List<Image>();

        private Button leftButton;
        private Button rightButton;
        private Button anecdoteButton;
        private Button collectionButton;

        private Button exitButton;
        private Button openBookButton;

        public int currentAmount = 0;

        private Transform anecdote;
        private Transform collection;

        [SerializeField]private Transform GhostBook;


         
        private void Start()
        {
          
            // GhostBook.gameObject.SetActive(false);

            leftButton = GameObject.Find("Canvas/GhostBook/btn_Left").GetComponent<Button>();
            rightButton = GameObject.Find("Canvas/GhostBook/btn_Right").GetComponent<Button>();

            anecdoteButton = GameObject.Find("Canvas/GhostBook/btn_Anecdote").GetComponent<Button>();
            collectionButton = GameObject.Find("Canvas/GhostBook/btn_Collection").GetComponent<Button>();

            exitButton=GameObject.Find("Canvas/GhostBook/btn_Exit").GetComponent<Button>();
            openBookButton = GameObject.Find("Canvas/btn_OpenBook").GetComponent<Button>();

            leftButton.onClick.AddListener(delegate { switchAnecdotePage(-1); });
            rightButton.onClick.AddListener(delegate { switchAnecdotePage(1); });
            anecdoteButton.onClick.AddListener(delegate { switchBookItemImage(true); });
            collectionButton.onClick.AddListener(delegate { switchBookItemImage(false); });

            exitButton.onClick.AddListener(delegate { CloseBook(); });
            openBookButton.onClick.AddListener(delegate { OpenBook(); });

            anecdote = GameObject.Find("Canvas/GhostBook/Anecdote").transform;
            collection = GameObject.Find("Canvas/GhostBook/Collection").transform;

            //GhostBook = GameObject.Find("Canvas/GhostBook").transform;
        }

        public void switchAnecdotePage(int amount)
        {
            int i = Mathf.Abs(currentAmount) % ghostImages.Count;
            showImage.sprite = ghostImages[i].sprite;

            currentAmount = currentAmount + amount;
        }

        public void switchBookItemImage(bool isAnecdote)
        {
            if (isAnecdote)
            {
                anecdote.gameObject.SetActive(true);
                collection.gameObject.SetActive(false);
                anecdoteButton.enabled = false;
                collectionButton.enabled = true;
            }
            else
            {
                anecdote.gameObject.SetActive(false);
                collection.gameObject.SetActive(true);
                anecdoteButton.enabled = true;
                collectionButton.enabled = false;
            }
        }

        /* private void OpenOrCloseBook(bool status)
         {
             GhostBook.gameObject.SetActive(status);
         }*/

        private void CloseBook()
        {
            GhostBook.gameObject.SetActive(false);
        }

        private void OpenBook()
        {
            GhostBook.gameObject.SetActive(true);
        }

    }
}


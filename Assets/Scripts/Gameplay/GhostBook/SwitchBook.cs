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


        public int currentAmount = 0;

        private Transform anecdote;
        private Transform collection;


        private void Start()
        {
            leftButton = GameObject.Find("Canvas/GhostBook/btn_Left").GetComponent<Button>();
            rightButton = GameObject.Find("Canvas/GhostBook/btn_Right").GetComponent<Button>();

            anecdoteButton = GameObject.Find("Canvas/GhostBook/btn_Anecdote").GetComponent<Button>();
            collectionButton = GameObject.Find("Canvas/GhostBook/btn_Collection").GetComponent<Button>();


            leftButton.onClick.AddListener(delegate { switchAnecdotePage(-1); });
            rightButton.onClick.AddListener(delegate { switchAnecdotePage(1); });
            anecdoteButton.onClick.AddListener(delegate { switchBookItemImage(true); });
            collectionButton.onClick.AddListener(delegate { switchBookItemImage(false); });

            anecdote = GameObject.Find("Canvas/GhostBook/Anecdote").transform;
            collection = GameObject.Find("Canvas/GhostBook/Collection").transform;


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

    }
}


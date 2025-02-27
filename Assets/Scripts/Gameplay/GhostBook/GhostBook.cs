using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using System;
using Scripts.Gameplay.Basic;
using Scripts.CustomTool.DesignPatterns;


namespace Scripts.Gameplay.GhostBook {
    public class GhostBook : MonoBehaviour
    {
        private Image Sheyao;
        private Image Jiangxue;
        private Image Xiangyu;

        //public Image[] ghostImages;
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


            Sheyao = GameObject.Find("Canvas/GhostBook/Anecdote/Sheyao").GetComponent<Image>();
            Jiangxue = GameObject.Find("Canvas/GhostBook/Anecdote/Jiangxue").GetComponent<Image>();
            Xiangyu = GameObject.Find("Canvas/GhostBook/Anecdote/Xiangyu").GetComponent<Image>();

            leftButton.onClick.AddListener(delegate { switchAnecdotePage(-1); });
            rightButton.onClick.AddListener(delegate { switchAnecdotePage(1); });
            anecdoteButton.onClick.AddListener(delegate { switchBookItemImage(true); });
            collectionButton.onClick.AddListener(delegate { switchBookItemImage(false); });

            anecdote = GameObject.Find("Canvas/GhostBook/Anecdote").transform;
            collection = GameObject.Find("Canvas/GhostBook/Collection").transform;


        }

        private void Awake()
        {


        }

        public void SheyaoImage(bool isSheyaoAwake)
        {
            if (isSheyaoAwake)
            {
                Sheyao.sprite = Resources.Load("GhostBook/Sheyao_After", typeof(Sprite)) as Sprite;
            }
        }

        public void JiangxueImage(bool isJiangxueAwake)
        {
            if (isJiangxueAwake)
            {
                Jiangxue.sprite = Resources.Load("GhostBook/Jiangxue_After", typeof(Sprite)) as Sprite;
            }
        }

        public void XiangyuImage(bool isXiangyuAwake)
        {
            if (isXiangyuAwake)
            {
                Xiangyu.sprite = Resources.Load("GhostBook/Xiangyu_After", typeof(Sprite)) as Sprite;
            }
        }

        public void switchAnecdotePage(int amount)
        {
            int i = Mathf.Abs(currentAmount) % ghostImages.Count;
            showImage.sprite = ghostImages[i].sprite;

            currentAmount = currentAmount + amount;
        }

        public void collectionItem(int item)
        {
            // TODO:Collection show item image.
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

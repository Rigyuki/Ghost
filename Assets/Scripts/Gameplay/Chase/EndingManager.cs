using Scripts.CustomTool.DesignPatterns.ObserverPattern;
using Scripts.Gameplay.GhostBook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

namespace Scripts.Gameplay.Chase
{
    public class EndingManager : MonoBehaviour
    {
        [SerializeField] SpriteRenderer trigram;
        [SerializeField] string ending;
        [SerializeField] string trigramEnabled;
        [SerializeField] string snakeDisappear;
        [SerializeField] Image whiteImage;
        public int chooseRoadNo;
        [SerializeField] GameObject snake;
        [SerializeField] DialogueSystemController dialogueManager;
        [SerializeField] GameObject dialogueBox;
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
            if (obj.MsgId == MsgCenterByList.ROAD_CHOOSING)
            {
                chooseRoadNo += obj.intParam;
            }
            else if (obj.MsgId == MsgCenterByList.ENDING)
            {
                OnShowEnding();
            }
        }

        private void OnShowEnding()
        {
            snake.SetActive(true);
            if (chooseRoadNo >= 1)
                ShowBE();
            else
                ShowGE();
        }
        void ShowBE()
        {
            StartCoroutine(SnakeChase());
        }
        void ShowGE()
        {
            StartCoroutine(TrigramAppear());
            StartCoroutine(TrigramRotate());
        }
        IEnumerator SnakeChase()
        {
            dialogueManager.StartConversation(ending);
            while (dialogueBox.activeSelf)
                yield return null;
            yield return new WaitForSeconds(1f);
            whiteImage.gameObject.SetActive(true);
            whiteImage.color = Color.clear;
            float a = 0;
            while (a < 1)
            {
                a += Time.deltaTime;
                whiteImage.color = new Color(0, 0, 0, a);
                yield return null;
            }
            whiteImage.color = Color.black;
            yield return new WaitForSeconds(1f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(gameObject.scene.name);
        }
        IEnumerator TrigramAppear()
        {
            dialogueManager.StartConversation(ending);
            while (dialogueBox.activeSelf)
                yield return null;
            float a = 0;
            while (a < 2)
            {
                a += Time.deltaTime;
                trigram.color = new Color(1, 1, 1, a / 2);
                yield return null;
            }
            trigram.color = Color.white;
            yield return new WaitForSeconds(0.5f);
            dialogueManager.StartConversation(trigramEnabled);
            while (dialogueBox.activeSelf)
            {
                yield return null;
            }
            a = 3;
            while (a > 1.5f)
            {
                a -= Time.deltaTime;
                snake.transform.localScale = a / 3 * Vector3.one;
                yield return null;
            }
            dialogueManager.StartConversation(snakeDisappear);
            while (a > 0)
            {
                a -= Time.deltaTime;
                snake.transform.localScale = a / 3 * Vector3.one;
                yield return null;
            }
            Destroy(snake);
            while (dialogueBox.activeSelf)
                yield return null;
            yield return new WaitForSeconds(2f);
            a = 0;
            whiteImage.gameObject.SetActive(true);
            whiteImage.color = new Color(1, 1, 1, 0);
            while (a < 1)
            {
                a += Time.deltaTime;
                whiteImage.color = new Color(1, 1, 1, a);
                yield return null;
            }
            whiteImage.color = Color.white;
            yield return new WaitForSeconds(1f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(gameObject.scene.name);
        }
        IEnumerator TrigramRotate()
        {
            while (true)
            {
                trigram.transform.Rotate(Vector3.back, Time.deltaTime * 60, Space.Self);
                yield return null;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay.Chase
{
    public class ChaseParent : MonoBehaviour
    {
        [SerializeField] GameObject snake;
        public int segment;
        void EnableSnake(object arg)
        {
            snake.SetActive(((int)arg) == segment);
        }
        private void OnEnable()
        {
            ChaseStartSubject.Instance.Register(EnableSnake);
        }
        private void OnDisable()
        {
            ChaseStartSubject.Instance.Unregister(EnableSnake);
        }
    }
}
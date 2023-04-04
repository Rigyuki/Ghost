using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.CustomTool.DesignPatterns.ObserverPattern;

namespace Scripts.Gameplay.Chase
{
    public class Butterfly : MonoBehaviour
    {
        public bool isYellowButterfly;
        Transform follower;
        public float speed;
        public float slowSpeed;
        public List<Vector3> positions = new List<Vector3>();
        public float maxDistance;
        int targetPosition;
        bool started = false;
        bool finished = false;
        private void Awake()
        {
            transform.position = positions[0];
        }
        private float Distance()
        {
            return Vector2.Distance(new Vector2(follower.position.x, follower.position.z), new Vector2(transform.position.x, transform.position.z));
        }
        private void Update()
        {
            if (!started || finished)
                return;
            transform.Translate((positions[targetPosition] - transform.position).normalized * Time.deltaTime * (Distance() < maxDistance ? speed : slowSpeed));
            if (Vector3.Distance(positions[targetPosition], transform.position) < 0.01f)
            {
                ++targetPosition;
                if (targetPosition == positions.Count)
                    Disappear();
            }
        }
        public void Disappear()
        {
            finished = true;
            Destroy(gameObject);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                started = true;
                follower = other.transform;
            }
            MsgCenterByList.SendMessage(new CommonMsg()
            {
                MsgId = MsgCenterByList.ROAD_CHOOSING,
                intParam = isYellowButterfly ? 1 : -1
            });
            ButterflySelectedSubject.Instance.Notify(this);
            Destroy(GetComponent<Collider>());
        }
    }
}
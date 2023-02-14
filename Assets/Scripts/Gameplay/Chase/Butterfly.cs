using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Gameplay.Chase
{
    public class Butterfly : MonoBehaviour
    {
        private UnityEngine.AI.NavMeshAgent agent;
        public GameObject target;
        public bool continuallyUpdated;


        void Start()
        {
            //获取角色上的NavMeshAgent组件
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (continuallyUpdated)
                InvokeRepeating(nameof(UpdateDestination), 5, 1);
        }

        private void Update()
        {
            //鼠标左键
            if (Input.GetMouseButtonDown(0))
            {
                //射线检测
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit);
                if (isCollider)
                {
                    //hit.point射线触碰的Position
                    //SetDestination设置下一步的位置
                    agent.SetDestination(hit.point);
                }
            }
        }

        public void UpdateDestination()
        {
            agent.SetDestination(target.transform.position);
        }
    }
}
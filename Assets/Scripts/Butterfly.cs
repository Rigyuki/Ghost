using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour, ISignalReceiver
{
    private UnityEngine.AI.NavMeshAgent agent;
    public GameObject target;

    void Start()
    {
        //获取角色上的NavMeshAgent组件
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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

    public void TrapSignalReceiver(object args)
    {
        agent.SetDestination(target.transform.position);
    }
}

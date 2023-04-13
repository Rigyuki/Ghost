using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Scripts.CustomTool.DesignPatterns.ObserverPattern;


public class AIController : MonoBehaviour
{
    public Transform target;

    [Range(1, 10)] public int updatePerXFrames = 5;
    int temp = 0;
    NavMeshAgent agent;


    public float patrolSpeed = 3f;  //巡逻速度
    public float patrolWaitTime = 0.5f;  //巡逻等待时间
    public Transform patrolWalPoints;  //巡逻路线

    private float patrolTimer = 0f; //计时器    
    private int wayPointIndex = 0;  //巡逻路线目录

    [SerializeField] float ChaseDistance;
    

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }


    private void Update()
    {
        
        if (temp % updatePerXFrames != 0)
        {
            ++temp;
            return;
        }
        temp = 1;

        float dis = Vector3.Distance(target.transform.position, transform.position);
        if (dis < agent.stoppingDistance)
        {
            MsgCenterByList.SendMessage(new CommonMsg()
            {
                MsgId = MsgCenterByList.ENEMY_AI_ATTACK,                
            });
            agent.SetDestination(target.position);
        }
        else if(dis> agent.stoppingDistance && dis< ChaseDistance)
        {
            MsgCenterByList.SendMessage(new CommonMsg()
            {
                MsgId = MsgCenterByList.ENEMY_AI_CHASE,
            });
            agent.SetDestination(target.position);
        }
        else if (dis > ChaseDistance)
        {
            MsgCenterByList.SendMessage(new CommonMsg()
            {
                MsgId = MsgCenterByList.ENEMY_AI_PATROL,
            });
            EnemyPartrol();
        }


        
    }

    private void EnemyPartrol()
    {
        agent.isStopped = false;
        agent.speed = patrolSpeed;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            //此时敌人停止，计时器开始计时
            patrolTimer += Time.deltaTime;
            //等待完毕，敌人可以继续行动，找到下一个目标点，重置计时器
            if (patrolTimer > patrolWaitTime)
            {
                if (wayPointIndex == patrolWalPoints.childCount - 1)
                {
                    wayPointIndex = 0;
                }
                else
                {
                    wayPointIndex++;
                }
                patrolTimer = 0;
            }
        }
        //设置目的地
        agent.destination = patrolWalPoints.GetChild(wayPointIndex).position;

    }

    private void OnDrawGizmos()
    {
        if (!target)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, target.transform.position);
    }
}

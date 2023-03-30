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


    public float patrolSpeed = 3f;  //Ѳ���ٶ�
    public float patrolWaitTime = 0.5f;  //Ѳ�ߵȴ�ʱ��
    public Transform patrolWalPoints;  //Ѳ��·��

    private float patrolTimer = 0f; //��ʱ��    
    private int wayPointIndex = 0;  //Ѳ��·��Ŀ¼

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
            //��ʱ����ֹͣ����ʱ����ʼ��ʱ
            patrolTimer += Time.deltaTime;
            //�ȴ���ϣ����˿��Լ����ж����ҵ���һ��Ŀ��㣬���ü�ʱ��
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
        //����Ŀ�ĵ�
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

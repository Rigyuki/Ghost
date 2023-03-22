using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Utils;
using Scripts.Gameplay.Basic;
using Spine.Unity;
using UnityEngine.UIElements.Experimental;
using UnityEngine.EventSystems;
using Scripts.CustomTool.DesignPatterns.ObserverPattern;
using Scripts.Gameplay.GhostBook;
using Scripts.Gameplay.Achievement;
using System;

public class EnemyController : MonoBehaviour
{
    
    [SerializeField] AnimationPlayer ani;
    [SerializeField] AnimationPlayer aniAttack;
    [SerializeField] AnimationPlayer aniAttackEffect;
   
    public string enemy_idle_base = "sheyao_stand";
    public string enemy_patrol_base="sheyao_walk";
    public string enemy_chase_base = "sheyao2_walk";
    public string enemy_attack_base = "sheyao2_attack";
    public string enemy_effect1_base = "1";
    public string enemy_effect2_base = "2";
    public string enemy_effect3_base = "3";

    int facing = 1;//1=前，2=后;4=左，8=右
 
    bool chasing;


    float prevAttackTime;



    private float originValuelr;

    [SerializeField]Transform player;


    public Transform cubeRed;//player
    public Transform cubeBlue;//myself


    [SerializeField] GameObject projectilePref;

    [SerializeField] GameObject AchievementCon;

     

    private void Start()
    {
        originValuelr = transform.position.x;

    }

    private void OnEnable()
    {
        MsgCenterByList.AddListener(OnMsg);
    }

    private void OnDisable()
    {

    }

    private void OnMsg(CommonMsg obj)
    {
        if (obj.MsgId == MsgCenterByList.ENEMY_AI_ATTACK) EnemyAttack();
        if (obj.MsgId == MsgCenterByList.ENEMY_AI_CHASE) EnemyChasing();
        if (obj.MsgId == MsgCenterByList.ENEMY_AI_PATROL) EnemyPatrol();
    }

    private void Update()
    {
        judgeFacing();
        //SetEnemyAnimation();        
    }

    private void EnemyAttack()
    {

        if (Time.time - prevAttackTime < 1.5f)
            return;
        prevAttackTime = Time.time;

        Debug.Log("attacking");
        aniAttack.gameObject.SetActive(true);
        aniAttack.Play(0, enemy_attack_base, facing, true);
        ani.gameObject.SetActive(false);
        // TODO: attact effect

        GameObject projectile = Instantiate(projectilePref);
        projectile.transform.position = transform.TransformPoint(Vector3.zero);
        projectile.GetComponent<ProjectileController>().Prepare(player.position + Vector3.down * 0.5f);

    }

    private void EnemyChasing()
    {
        Debug.Log("chasing");
        aniAttack.gameObject.SetActive(true);
        aniAttack.Play(0, enemy_chase_base, facing, true);
        ani.gameObject.SetActive(false);

        // 不赋值会报错，先注释掉了
        // TODO: 结局管理
        //GameObject achieve = Instantiate(AchievementCon);
        //achieve.GetComponent<AchievementController>().SendMessage("judgeRoad");

    }

    private void EnemyPatrol()
    {
        Debug.Log("patrol");
        ani.Play(0, enemy_patrol_base, facing, true);
        aniAttack.gameObject.SetActive(false);
        ani.gameObject.SetActive(true);
    }

    private void judgeFacing()
    {        
        Vector3 relativePosition = cubeRed.position - cubeBlue.position;
        Vector3 cubeForward = cubeBlue.forward;

        float result = Vector3.Dot(cubeForward, relativePosition);
        float angle = Vector3.Angle(cubeForward, relativePosition);    

        if ( originValuelr - transform.position.x == 0)
        {
            chasing = false;
        }
        else
        {
            chasing = true;
            if (angle < 90 )
            {
                if(originValuelr - transform.position.x > 0)
                {
                    facing = 6;
                }                   
                if (originValuelr - transform.position.x < 0)
                {
                    facing = 5;
                }
                   
            }else if (angle > 90)
            {
                if (originValuelr - transform.position.x > 0)
                {
                    facing = 6;
                }
                if (originValuelr - transform.position.x < 0)
                {
                    facing = 10;
                }
                   
            }
           
        }
        originValuelr = transform.position.x;
    }

}

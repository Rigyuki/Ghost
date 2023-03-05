using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Gameplay.Basic;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] AnimationPlayer animationPlayer;
    public string prepareAnim;
    public string flyAnim;
    public string hitAnim;
    bool prepared;
    bool hit;
    bool finished;
    Vector3 dir;
    [SerializeField] float speed;
    [SerializeField] float maxDistance;
    float totDistance;
    public void Prepare(Vector3 targetPos)
    {
        animationPlayer.Play(0, prepareAnim, false, true);
        this.dir = (targetPos - transform.position).normalized;
        Vector3 diff = Camera.main.WorldToScreenPoint(targetPos) - Camera.main.WorldToScreenPoint(transform.position);
        animationPlayer.transform.localEulerAngles = Vector3.forward * (Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 150);
    }
    public void Shoot()
    {
        animationPlayer.Play(0, flyAnim, true, false);
    }
    public void Hit()
    {
        animationPlayer.Play(0, hitAnim, false, false);
        hit = true;
    }
    void Update()
    {
        if(!prepared)
        {
            if (animationPlayer.sa.state.GetCurrent(0).IsComplete)
            {
                prepared = true;
                Shoot();
            }
        }
        if (prepared && !hit)
        {
            transform.Translate(dir * Time.deltaTime * speed);
            totDistance += Time.deltaTime * speed;
            if (totDistance >= maxDistance)
                hit = true;
        }
        if(hit&&!finished)
        {
            if(animationPlayer.sa.state.GetCurrent(0).IsComplete)
            {
                finished = true;
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer==LayerMask.NameToLayer("Player"))
        {
            ;
        }
        Hit();
    }
}
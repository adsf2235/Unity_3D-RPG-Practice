using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonstrerController : BaseController
{
    Stat _stat;

   
    protected override void Init()
    {
        _stat = GetComponent<Stat>();
      

        if (transform.gameObject.GetComponentInChildren<UI_HPBar>() == null)
        {
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
        }

        type = Define.ObjectType.Monster;
    }

    float _scanRange = 10f;
    float _attackRange = 2;



    protected override void UpdateIdle()
    {
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        _lockTarget = _player;
        _destPos = _lockTarget.transform.position - transform.position;

        float _distance = _destPos.magnitude;

        if (_distance <= _scanRange)
        {
            State = Define.State.Moving;
        }
    }
    protected override void UpdateDie()
    {
    }
    protected override void UpdateMoving()
    {
        NavMeshAgent nma = gameObject.GetComponent<NavMeshAgent>();
        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float _distance = (_lockTarget.transform.position - transform.position).magnitude;
            if (_distance <= _attackRange)
            {
                nma.SetDestination(transform.position);
                nma.speed = _stat.moveSpeed;
                State = Define.State.Skill;
                return;
            }
            
             
            
            else
            {
              
                nma.SetDestination(_destPos);
                nma.speed = _stat.moveSpeed;

                
            }
        }
        
        
    }
    protected override void UpdateSkill()
    {
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            Stat lockTargetStat = _lockTarget.GetComponent<Stat>();
            Stat myStat = GetComponent<Stat>();

            int damage = myStat.attack - lockTargetStat.defense;
            lockTargetStat.hp -= Mathf.Max(0, damage);
        }

        _destPos = _lockTarget.transform.position - transform.position;
        float _distance = _destPos.magnitude;
        if (_distance <= _attackRange)
        {
            State = Define.State.Skill;
        }
        else
        {
            State = Define.State.Moving;
        }
      
    }
}

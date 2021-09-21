using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{

    Stat _stat;

    float wait_run_ratio;

    int _mask = (1 << (int)Define.Layer.Floor | 1 << (int)Define.Layer.Monster);


    protected override void Init()
    {
        _stat = GetComponent<Stat>();
        Managers.Input.MouseAction -= OnMouseEvnet;
        Managers.Input.MouseAction += OnMouseEvnet;

        if (transform.gameObject.GetComponentInChildren<UI_HPBar>() == null)
        {
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
        }
       
    }




    void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            Stat lockTargetStat = _lockTarget.GetComponent<Stat>();
            PlayerStat myStat = GetComponent<PlayerStat>();

            int damage = myStat.attack - lockTargetStat.defense;
            lockTargetStat.hp -= Mathf.Max(0, damage);
        }


        if (_skilstop)
        {
            State = Define.State.Idle;
        }
        else
        {
            State = Define.State.Skill;
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
    protected override void UpdateIdle()
    {
    }
    protected override void UpdateDie()
    {

    }
    protected override void UpdateMoving()
    {
        if (_lockTarget != null)
        {

            float _distance = (_lockTarget.transform.position - transform.position).magnitude;
            if (_distance <= 1)
            {
                State = Define.State.Skill;
                return;
            }
        }
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {



            float moveDist = Mathf.Clamp(Time.deltaTime * _stat.moveSpeed, 0, dir.magnitude);
           

            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if (!Input.GetMouseButton(0))
                {
                    Debug.Log("H");
                    State = Define.State.Idle;
                    return;
                }

            }
            transform.position += dir.normalized * moveDist;


            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.black);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);


        }



    }


    void OnMouseEvnet(Define.MouseEvent evt)
    {
        switch (State)
        {
            case Define.State.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Skill:
                if (evt == Define.MouseEvent.PointerUp)
                {
                    _skilstop = true;
                }
                break;
        }
    }

    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);


        switch (evt)
        {
            case Define.MouseEvent.Press:
                {

                    if (_lockTarget != null)
                    {
                        _destPos = _lockTarget.transform.position;
                    }
                    else if (raycastHit)
                    {
                        _destPos = hit.point;
                    }

                }
                break;
            case Define.MouseEvent.PointerUp:
                {
                    _skilstop = true;
                }
                break;
            case Define.MouseEvent.PointerDown:
                {
                    if (raycastHit)
                    {
                        _destPos = hit.point;
                        State = Define.State.Moving;
                        _skilstop = false;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                        {
                            _lockTarget = hit.collider.gameObject;

                            Debug.Log("Monster Check!");
                        }
                        else
                        {
                            _lockTarget = null;
                        }
                    }



                }
                break;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CastState : PlayerState
{
    GameObject atkRound;
    PlayableDirector CastAnim;
    int Count;
    Rokcer Rokcer;
    public Transform pointA;  // 起点
    public Transform pointB;  // 终点
    public Transform controlPoint;  // 控制点
    public LineRenderer lineRenderer;
    public int segments = 30;
    public CastState(PlayerStateController controller) : base(controller)
    {
        MessAgeController<GameObject>.Instance.AddLister(1006, SetAtkRound);
        MessAgeController<Rokcer>.Instance.AddLister(1002, SetRocker);
        
    }

    private void SetRocker(Rokcer rokcer)
    {
        Rokcer = rokcer;
    }

    private void SetAtkRound(GameObject @object)
    {
        atkRound = @object;
        atkRound.SetActive(false);
    }
    public void Setstartpos()
    {
        atkRound.transform.position = new Vector3(-12, 0, 18);
    }
    public override void Enter()
    {
        MessAgeController<int>.Instance.SendMessAge(1005, 0);
        MessAgeController<int>.Instance.SendMessAge(1001, 0);
        Count = 3;        
        atkRound.SetActive(true);
        atkRound.transform.position = new Vector3(-10,0,20);
        Camera.main.transform.position = new Vector3(-13, 27, -14);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        Camera.main.transform.position = new Vector3(-0, 30, -36);
    }

    public override void Update()
    {
        base.Update();
        if (atkRound != null)
        {
            float speed = (Rokcer.GetComponent<DragComponent>() as DragComponent).speed;
            float ang = (Rokcer.GetComponent<DragComponent>() as DragComponent).ang;
            if (speed > 0)
            {
                atkRound.transform.position += new Vector3(Mathf.Sin(ang * Mathf.Deg2Rad), 0, Mathf.Cos(ang * Mathf.Deg2Rad)) * speed * 0.2f * Time.deltaTime;
                if(Input.GetMouseButtonUp(0))
                {
                    CastAnim.Play();
                    Count--;
                    if(Count == 0)
                    {
                        controller.ChangeState(PlayerStateEnum.CastEnd);
                    }
                }
                if(atkRound.transform.position.z<20)
                {
                    atkRound.transform.position = new Vector3(atkRound.transform.position.x, 0.1f, 20);
                }
            }
            
        }
    }
}

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
    
    public CastState(PlayerStateController controller) : base(controller)
    {
        MessAgeController<GameObject>.Instance.AddLister(1006, SetAtkRound);
       MessAgeController<Rokcer>.Instance.AddLister(1002, SetRocker);
       MessAgeController<int>.Instance.AddLister(1017, ChangeState);
    }

    private void ChangeState(int n)
    {
       controller.ChangeState(PlayerStateEnum.CastEnd);
    }
    private void SetRocker(Rokcer rokcer)
    {
        Rokcer = rokcer;
    }

   
    private void SetAtkRound(GameObject @object)
    {
        atkRound = @object;
    }
    public override void Enter()
    {
        MessAgeController<int>.Instance.SendMessAge(1005, 0);
        MessAgeController<int>.Instance.SendMessAge(1001, 0);
        
        base.Enter();
        MessAgeController<int>.Instance.SendMessAge(1015, 0);
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
                if(atkRound.transform.position.z<20)
                {
                    atkRound.transform.position = new Vector3(atkRound.transform.position.x, 0.1f, 20);
                }
            }        
        }
    }
    

    // 二次贝塞尔曲线公式
   
}

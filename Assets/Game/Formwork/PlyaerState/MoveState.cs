using Codice.CM.Common.Tree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MoveState : PlayerState
{
    Rokcer Rokcer;
    Vector3 CostPos;
    GameObject player;
    Transform meatpool;
    Transform DeskMeat;
    Transform deskcookerMeat;
    public MoveState(PlayerStateController controller) : base(controller)
    {
        MessAgeController<Rokcer>.Instance.AddLister(1002, SetRolcler);
        MessAgeController<Vector3>.Instance.AddLister(1004, SetCostPos);
        MessAgeController<GameObject>.Instance.AddLister(1010, SetPlayer);
        MessAgeController<Transform>.Instance.AddLister(1024, SetMeatPool);
        MessAgeController<Transform>.Instance.AddLister(1037,SetDeskMeat);
        MessAgeController<Transform>.Instance.AddLister(1038,SetDeskCookerMeat);
    }
    private void SetDeskMeat(Transform obj)
    {
        DeskMeat=obj;
    }

    private void SetDeskCookerMeat(Transform obj)
    {
        deskcookerMeat=obj;
    }

    private void SetMeatPool(Transform obj)
    {
        meatpool = obj;
    }
    private void SetPlayer(GameObject @object)
    {
        player = @object;
    }

    private void SetCostPos(Vector3 vector)
    {
        CostPos = vector;
    }

    private void SetRolcler(Rokcer rokcer)
    {
        this.Rokcer = rokcer;
    }

    public override void Enter()
    {
        MessAgeController<int>.Instance.SendMessAge(1001, 0);
        MessAgeController<int>.Instance.SendMessAge(1003, 0);
        MessAgeController<int>.Instance.SendMessAge(1023, 0);
         MessAgeController<int>.Instance.SendMessAge(1033, 0);
        MessAgeController<int>.Instance.SendMessAge(1034, 0);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        if (player != null)
        {
            float speed = (Rokcer.GetComponent<DragComponent>() as DragComponent).speed;
            float ang = (Rokcer.GetComponent<DragComponent>() as DragComponent).ang;
            if (speed > 0)
            {
                player.transform.position += new Vector3(Mathf.Sin(ang * Mathf.Deg2Rad), 0, Mathf.Cos(ang * Mathf.Deg2Rad)) * speed * 0.2f * Time.deltaTime;
            }
            if (Vector3.Distance(player.transform.position, CostPos) < 1f)
            {
                player.transform.position = CostPos;
                controller.ChangeState(PlayerStateEnum.Cast);
            }
            if (Vector3.Distance(player.transform.position, DeskMeat.position) < 1f)
            {
                controller.ChangeState(PlayerStateEnum.PutMeahOn);
            }
            if (Vector3.Distance(player.transform.position, deskcookerMeat.position) < 1f)
            {
                controller.ChangeState(PlayerStateEnum.GatherCookerMeat);
            }
            if (meatpool!=null)
            {
                if (Vector3.Distance(player.transform.position, meatpool.position) < 6f)
                {
                    controller.ChangeState(PlayerStateEnum.GatherMeat);
                }
            }  
            if(Vector3.Distance(player.transform.position,DeskMeat.position)<1f)
            {
                controller.ChangeState(PlayerStateEnum.PutMeahOn);
            }
            if (Vector3.Distance(player.transform.position, deskcookerMeat.position) < 1f)
            {
                controller.ChangeState(PlayerStateEnum.GatherCookerMeat);
            }
        }
        base.Update();
        
    }
}

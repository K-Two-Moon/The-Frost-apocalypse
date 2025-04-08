using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostEndState : PlayerState
{
    Rokcer Rokcer;
    Vector3 CostPos;
    GameObject player;
    public CostEndState(PlayerStateController controller) : base(controller)
    {
        MessAgeController<GameObject>.Instance.AddLister(1010, SetPlayer);
        MessAgeController<Rokcer>.Instance.AddLister(1002, SetRolcler);
        MessAgeController<Vector3>.Instance.AddLister(1004, SetCostPos);      
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
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (player != null)
        {
            float speed = (Rokcer.GetComponent<DragComponent>() as DragComponent).speed;
            float ang = (Rokcer.GetComponent<DragComponent>() as DragComponent).ang;
            if (speed > 0)
            {
                player.transform.position += new Vector3(Mathf.Sin(ang * Mathf.Deg2Rad), 0, Mathf.Cos(ang * Mathf.Deg2Rad)) * speed * 0.2f * Time.deltaTime;
            }
            if (Vector3.Distance(player.transform.position, CostPos) > 2f)
            {           
                controller.ChangeState(PlayerStateEnum.Move);
            }
        }
    }


}

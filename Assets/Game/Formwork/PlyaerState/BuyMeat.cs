using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMeat : PlayerState
{
    GameObject player;
    Transform busy;
    Rokcer rocker;
    public BuyMeat(PlayerStateController controller) : base(controller)
    {
         MessAgeController<Rokcer>.Instance.AddLister(1002, SetRolcler);
        MessAgeController<Transform>.Instance.AddLister(1051, SetCookerMeatPos);
        MessAgeController<GameObject>.Instance.AddLister(1010, SetPlayer);
    }
    private void SetRolcler(Rokcer rokcer)
    {
        this.rocker = rokcer;
    } 

     private void SetPlayer(GameObject @object)
    {
        player = @object;
    }

    private void SetCookerMeatPos(Transform trs)
    {
        busy=trs;
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(player != null)
        {
            float speed = (rocker.GetComponent<DragComponent>() as DragComponent).speed;
            float ang = (rocker.GetComponent<DragComponent>() as DragComponent).ang;       
            if (speed > 0)
            {
                player.transform.position += new Vector3(Mathf.Sin(ang * Mathf.Deg2Rad), 0, Mathf.Cos(ang * Mathf.Deg2Rad)) * speed * 0.2f * Time.deltaTime;
            }
            if (Vector3.Distance(player.transform.position, busy.position) > 1)
            {   
                controller.ChangeState(PlayerStateEnum.Move);
            }
        }
    }
}

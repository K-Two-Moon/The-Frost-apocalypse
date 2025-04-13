using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherMeatState : PlayerState
{
    GameObject player;
    Transform meatpool;
    Rokcer rokcer;
    Vector3 CostPos;
    float timer=0.1f;
    public GatherMeatState(PlayerStateController controller) : base(controller)
    {
        MessAgeController<Rokcer>.Instance.AddLister(1002, SetRolcler);
        MessAgeController<GameObject>.Instance.AddLister(1010, SetPlayer);
        MessAgeController<Transform>.Instance.AddLister(1024, SetMeatPool);
    }
     private void SetMeatPool(Transform obj)
    {
        meatpool = obj;
    }
    private void SetPlayer(GameObject @object)
    {
        player = @object;
    }

    private void SetRolcler(Rokcer rokcer)
    {
        this.rokcer = rokcer;
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
        if(player!=null)
        {
            float speed = (rokcer.GetComponent<DragComponent>() as DragComponent).speed;
            float ang = (rokcer.GetComponent<DragComponent>() as DragComponent).ang;
            if (speed > 0)
            {
                player.transform.position += new Vector3(Mathf.Sin(ang * Mathf.Deg2Rad), 0, Mathf.Cos(ang * Mathf.Deg2Rad)) * speed * 0.2f * Time.deltaTime;
            }
            if(meatpool!=null)
            {
                 if(Vector3.Distance(player.transform.position, meatpool.position )> 5f)
                {
                    controller.ChangeState(PlayerStateEnum.Move);
                }
            } 
             timer-=Time.deltaTime;
             if(timer<=0)
             {
                MessAgeController<int>.Instance.SendMessAge(1025, 0);
                timer = 0.1f;
             }        
        }
    }
}

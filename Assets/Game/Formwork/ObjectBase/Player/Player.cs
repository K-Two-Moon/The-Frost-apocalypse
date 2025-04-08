using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Object3D
{
    PlayerData data;
    public PlayerStateController stateController;
    public Player()
    {
        stateController=new PlayerStateController();
        stateController.Initialize();
    }
    public override void Initialize()
    {
        base.Initialize();
        data=new PlayerData();
    }

    public override void Destroy(bool isRecycle = false)
    {
        base.Destroy(isRecycle);
    }
    public override void Create()
    {
        if (obj == null)
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>("Player"));
            MessAgeController<GameObject>.Instance.SendMessAge(1010,obj);
        }
        base.Create();
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        //�������

    }

    public override void Update()
    {
        stateController.Update();
    }
}

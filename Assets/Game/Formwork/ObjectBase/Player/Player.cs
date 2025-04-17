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
        base.Update();
        if (obj != null&&!(stateController.m_state is CastState))
        {
            // 平滑移动相机到玩家位置的上方和后方
            Camera.main.transform.position = Vector3.Lerp(
                Camera.main.transform.position,
                obj.transform.position + new Vector3(0, 8, -5), // 相机相对玩家的位置
                Time.deltaTime * 2f
                ) // 平滑速度
            ;

            // 平滑旋转相机以始终面向玩家
            Quaternion targetRotation = Quaternion.LookRotation(obj.transform.position - Camera.main.transform.position);
            Camera.main.transform.rotation = Quaternion.Slerp(
                Camera.main.transform.rotation,
                targetRotation,
                Time.deltaTime * 2 // 平滑旋转速度
            );
        }
    }
}

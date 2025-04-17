using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
     // 保护的场景状态控制器实例
    protected PlayerStateController controller;

    // 构造函数，初始化场景状态控制器
    public PlayerState(PlayerStateController controller)
    {
        this.controller = controller;
    }

    // 虚方法，用于进入场景状态时的处理
    public virtual void Enter()
    {
        
    }

    // 虚方法，用于退出场景状态时的处理
    public virtual void Exit()
    {
       
    }

    // 虚方法，用于场景状态更新时的处理
    public virtual void Update()
    {
        
    }
    
}

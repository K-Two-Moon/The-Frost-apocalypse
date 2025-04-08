using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerStateEnum
{
    /// <summary>
    /// 移动
    /// </summary>
    Move,
    /// <summary>
    /// 投射
    /// </summary>
    Cast,
    /// <summary>
    /// 捡肉
    /// </summary>
    GatherMeat,
    /// <summary>
    ///放肉
    /// <summary>
    PutMeahOn,
    ///投射结束
    CastEnd
}
public class PlayerStateController
{
    // 当前场景状态
    private PlayerState m_state;

    // 存储场景状态的字典
    private Dictionary<PlayerStateEnum, PlayerState> stateDict;

    /// <summary>
    /// 初始化场景状态字典并注册不同场景状态
    /// </summary>
    public void Initialize()
    {
        // 初始化场景状态字典
        stateDict = new Dictionary<PlayerStateEnum, PlayerState>
        {
            { PlayerStateEnum.Move, new MoveState(this) },
            { PlayerStateEnum.Cast, new CastState(this) },
            { PlayerStateEnum.GatherMeat, new GatherMeatState(this) },
            {PlayerStateEnum.PutMeahOn,new PutMeatOnState(this)},
             {PlayerStateEnum.CastEnd,new CostEndState(this)}
        };

        ChangeState(PlayerStateEnum.Move);
    }
    

    // 更改当前场景状态
    public void ChangeState(PlayerStateEnum state)
    {
        if (stateDict.ContainsKey(state))
        {
            //异步 设置新的场景状态，切换场景
            //SetStateAsync(stateDict[state]);


            //不切换场景，只切换状态
            SetStateNoLoadScene(stateDict[state]);


        }
    }

    /// <summary>
    /// 单纯切换状态，不切换场景
    /// </summary>
    private void SetStateNoLoadScene(PlayerState newState)
    {
        if (m_state != null)
        {
            m_state.Exit();
        }

        m_state = newState;

        if (m_state != null)
        {
            m_state.Enter();
        }
    }

   

    // 更新当前场景状态
    public void Update()
    {
        m_state?.Update();
    }

}

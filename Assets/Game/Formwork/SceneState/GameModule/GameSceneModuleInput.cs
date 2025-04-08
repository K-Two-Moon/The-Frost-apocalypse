using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// 输入模块
/// 处理玩家的输入
/// </summary>
public class GameSceneModuleInput : IModule
{
    public GameSceneModuleInput(GameState gameState) : base(gameState)
    {
    }

    public override void Destroy()
    {

    }

    public override void Initialize()
    {
        
    }

    public override void Update()
    {
        (gameState.objectModule as GameSceneModuleObject).player.Update();
    }
}

using JKFrame;


/// <summary>
/// 事件模块
/// 负责注册场景事件
/// </summary>
public class GameSceneModuleEvent : IModule
{
    public GameSceneModuleEvent(GameState gameState) : base(gameState)
    {
    }



    public override void Initialize()
    {
        //添加命令到命令队列
        EventSystem.AddEventListener<ICommand>(CMD.ADD_COMMAND, OnAddCommand);
    }

    public override void Destroy()
    {
        EventSystem.RemoveEventListener<ICommand>(CMD.ADD_COMMAND, OnAddCommand);
    }

    private void OnAddCommand(ICommand command)
    {
        (gameState.commandModule as GameSceneModuleCommand).AddCommand(command);
    }

    public override void Update()
    {

    }
}

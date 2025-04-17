using UnityEngine;

public class GameState : ISceneState
{
    public IModule eventModule;
    public IModule commandModule;
    public IModule inputModule;
    public IModule objectModule;
    public GameState(SceneStateController controller) : base(controller)
    {
        sceneName = SceneStateEnum.Game.ToString();
    }


    public override void Enter()
    {
        base.Enter();
        //添加模块，模块的添加顺序决定了模块的更新顺序
        eventModule = new GameSceneModuleEvent(this);
        commandModule = new GameSceneModuleCommand(this);
        inputModule = new GameSceneModuleInput(this);
        objectModule = new GameSceneModuleObject(this);

        facade.AddModule(eventModule);
        facade.AddModule(commandModule);
        facade.AddModule(inputModule);
        facade.AddModule(objectModule);
    }

    public override void Exit()
    {
        //基类释放所有模块
        base.Exit();
    }

    public override void Update()
    {
        //更新所有模块
        facade.Update();

        //数据更新了，世界再更新
        base.Update();
    }
}

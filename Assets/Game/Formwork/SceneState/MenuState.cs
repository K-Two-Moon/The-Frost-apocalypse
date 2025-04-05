using UnityEngine;

internal class MenuState : ISceneState
{
    public MenuState(SceneStateController controller) : base(controller)
    {
        sceneName = SceneStateEnum.Menu.ToString();
    }

    public override void Enter()
    {
        //基类中初始化世界
        base.Enter();
        //添加模块，模块的添加顺序决定了模块的更新顺序
        //facade.AddModule();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        facade.Update();

        //数据更新了，世界再更新
        base.Update();
    }
}

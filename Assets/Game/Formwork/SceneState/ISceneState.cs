public abstract class ISceneState
{
    // 保护的场景状态控制器实例
    protected SceneStateController controller;
    //模块管理器实例
    public GameSystemFacade facade = new GameSystemFacade();

    // 保护的场景名称字符串
    protected string sceneName;
    // 公开的只读属性，用于获取场景名称
    public string SceneName => sceneName;

    // 构造函数，初始化场景状态控制器
    public ISceneState(SceneStateController controller)
    {
        this.controller = controller;
    }

    // 虚方法，用于进入场景状态时的处理
    public virtual void Enter()
    {
        World.Instance.Initialize();
    }

    // 虚方法，用于退出场景状态时的处理
    public virtual void Exit()
    {
        facade.Destroy();
        facade = null;
        World.Instance.Destroy();
    }

    // 虚方法，用于场景状态更新时的处理
    public virtual void Update()
    {
        World.Instance.Update();
    }
}

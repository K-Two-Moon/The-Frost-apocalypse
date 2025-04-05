using JKFrame;

/// <summary>
/// 组件
/// </summary>
public abstract class IComponent
{
    /// <summary>
    /// 通过中介模式与GameObject交互
    /// </summary>
    protected IGameObject obj;
    public bool enabled;
    protected IComponent()
    {

    }

    /// <summary>
    /// 基类方法没有逻辑
    /// </summary>
    public virtual void Initialize(IGameObject obj)
    {
        this.obj = obj;
        enabled = true;
    }
    /// <summary>
    /// 基类方法卸载组件
    /// </summary>
    public virtual void Destroy()
    {
        //obj = null;  // 这里不能置空,否则会报错,因为在卸载组件时,可能会调用到obj的方法
        enabled = false;
    }
    /// <summary>
    /// 没有逻辑
    /// </summary>
    public virtual void Update()
    {

    }
}

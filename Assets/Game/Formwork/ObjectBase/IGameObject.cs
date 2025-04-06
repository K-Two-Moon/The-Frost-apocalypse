
using System.Collections.Generic;
using JKFrame;
using UnityEngine;

public abstract class IGameObject
{
    protected GameObject obj;
    string name;
    private uint id;

    private bool active;

    /// <summary>
    /// 组件容器
    /// </summary>
    protected List<IComponent> componentList = new List<IComponent>();
    public GameObject Obj => obj;
    public uint Id => id;

    public bool Active => active;

    public IGameObject()
    {
        name = GetType().Name;
    }

    public virtual void Initialize()
    {
        active = true;
        obj?.SetActive(true);
    }

    public virtual void Destroy()
    {
        active = false;

        //销毁组件
        for (int i = 0; i < componentList.Count; i++)
        {
            IComponent component = componentList[i];
            component.Destroy();
        }
        //在World中移除id
        World.Instance.AddToDestoryObjectBuffer(id);
        componentList.Clear();
        //Object.Destroy(obj);
        obj.SetActive(false);
        PoolSystem.PushObject(this);
    }

    /// <summary>
    /// 重写后,后调用基类
    /// </summary>
    public virtual void Create()
    {
        World.Instance.AddObject(this);
        obj.name = name + "_" + id.ToString();
        OnCreate();
    }

    /// <summary>
    /// 基类没有逻辑
    /// </summary>
    protected virtual void OnCreate()
    {

    }


    public void SetId(uint id)
    {
        this.id = id;
    }

    /// <summary>
    /// 子类实现自己的Update,父类逻辑为空
    /// </summary>
    public virtual void Update()
    {
        //更新组件
        for (int i = 0; i < componentList.Count; i++)
        {
            IComponent component = componentList[i];
            if (component.enabled == false)
            {
                continue;
            }
            else component.Update();
        }
    }

    #region 组件管理
    /// <summary>
    /// 添加组件
    /// </summary>
    public void AddComponent<T>() where T : IComponent, new()
    {
        //先判断是否已经有该组件
        IComponent component = componentList.Find(x => x.GetType() == typeof(T));
        if (component != null)
        {
#if UNITY_EDITOR
            Debug.LogError($"{obj.name}已经有{typeof(T).Name}组件,不需要重复添加");
#endif
        }
        else
        {

            //创建组件
            component = new T();
            component.Initialize(this);
            componentList.Add(component);
        }
    }

    /// <summary>
    /// 移除组件
    /// </summary>
    public void RemoveComponent<T>() where T : IComponent, new()
    {
        IComponent component = componentList.Find(x => x.GetType() == typeof(T));
        if (component != null)
        {
            component.Destroy();
            componentList.Remove(component);
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log($"{obj.name}没有{typeof(T).Name}组件,不需要移除");
#endif
        }
    }

    /// <summary>
    /// 获取自定义组件
    /// </summary>
    public IComponent GetComponent<T>() where T : IComponent, new()
    {
        IComponent component = componentList.Find(x => x.GetType() == typeof(T));
        if (component == null)
        {
#if UNITY_EDITOR
            Debug.Log($"{obj.name}没有{typeof(T).Name}组件,无法获取");
#endif
        }
        return component;
    }

    /// <summary>
    /// 建议使用
    /// </summary>
    public bool TryGetComponent<T>(out T component) where T : IComponent, new()
    {
        component = componentList.Find(x => x.GetType() == typeof(T)) as T;
        return component != null;
    }
    #endregion
}


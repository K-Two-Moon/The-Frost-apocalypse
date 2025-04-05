using System.Collections.Generic;
using JKFrame;
using UnityEngine;


public partial class World : Singleton<World>
{
    /// <summary>
    /// All objects in the world.
    /// </summary>
    Dictionary<uint, IGameObject> allObjectDict;
    /// <summary>
    /// 销毁队列缓存
    /// </summary>
    Queue<uint> destroyQueue;

    uint nextId = 0;

    public void Initialize()
    {
        allObjectDict = new Dictionary<uint, IGameObject>();
        destroyQueue = new Queue<uint>();
    }

    /// <summary>
    /// 不要再对象更新中调用，只能是场景状态切换时调用，否则报错
    /// </summary>
    public void Destroy()
    {
        foreach (IGameObject obj in allObjectDict.Values)
        {
            obj.Destroy();
        }
        allObjectDict.Clear();
    }


    public void AddObject(IGameObject obj)
    {
        allObjectDict.Add(nextId, obj);
        obj.SetId(nextId);
        nextId++;
    }

    void RemoveObject(uint id)
    {
        if (allObjectDict.ContainsKey(id))
        {
            IGameObject obj = allObjectDict[id];
            obj.Destroy();
            //世界中移除对象的注册
            allObjectDict.Remove(id);
        }
    }

    public IGameObject GetObjectById(uint id)
    {
        if (allObjectDict.ContainsKey(id))
        {
            return allObjectDict[id];
        }
        else
        {
            Debug.LogError("没有这个对象");
            return null;
        }
    }



    /// <summary>
    /// 外部销毁对象先放在这个缓存中，再帧最后统一销毁
    /// </summary>
    public void AddToDestoryObjectBuffer(uint id)
    {
        destroyQueue.Enqueue(id);
    }

    public GameObject king;



    public void Update()
    {
        foreach (IGameObject obj in allObjectDict.Values)
        {
            if (obj.Active)
            {
                obj.Update();
            }
        }
        
        //销毁缓存中的对象
        if (destroyQueue.Count > 0)
        {
            while (destroyQueue.Count > 0)
            {
                uint id = destroyQueue.Dequeue();
                RemoveObject(id);
            }
        }

    }
}


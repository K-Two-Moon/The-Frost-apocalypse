using JKFrame;
using System.Collections.Generic;
using UnityEngine;

public interface IConfig
{

}

public class ConfigManager : Singleton<ConfigManager>
{
    /// <summary>
    /// ScriptObject配置文件
    /// </summary>
    Dictionary<string, IConfig> dict = new Dictionary<string, IConfig>();

    public void Initialize()
    {

    }

    public void SetPlayerSneakData()
    {

    }



    public void Dispose()
    {
        dict.Clear();
        dict = null;
    }

    /// <summary>
    /// 获取ScriptObject配置文件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public IConfig GetConfig<T>()
    {
        string name = typeof(T).Name;
        if (dict.ContainsKey(name))
        {
            return dict[name];
        }
        else
        {
            Debug.LogError("没有这个数据");
            return null;
        }
    }
}

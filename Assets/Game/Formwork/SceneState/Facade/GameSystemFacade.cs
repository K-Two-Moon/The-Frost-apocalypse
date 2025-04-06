using System.Collections.Generic;

public class GameSystemFacade
{
    List<IModule> moduleList = new List<IModule>();

    public void Initialize()
    {
    }

    public void Destroy()
    {
        foreach (var moduled in moduleList)
        {
            moduled.Destroy();
        }
        moduleList.Clear();
    }

    public void AddModule(IModule moduled)
    {
        moduled.Initialize();
        moduleList.Add(moduled);
    }

    // 统一对外方法
    public void Update()
    {
        foreach (var moduled in moduleList)
        {
            moduled.Update();
        }
    }
}

using JKFrame;
using UnityEngine;

public class GameLoop : SingletonMono<GameLoop>
{
    // 场景状态
    private SceneStateController m_SceneStateController;

    void Start()
    {

        // 设置帧率
        Application.targetFrameRate = 60;

        // 切换场景不被删除
        DontDestroyOnLoad(gameObject);

        //配置表初始化
        ConfigManager.Instance.Initialize();
        //场景管理初始化
        m_SceneStateController = new SceneStateController();
        m_SceneStateController.Initialize();
        

        JKLog.Succeed("启动成功");
    }


    void Update()
    {
        m_SceneStateController.Update();
    }
}

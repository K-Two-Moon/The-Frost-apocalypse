using JK.Log;
using UnityEngine;

public class Cubes : Object3D
{
    public Cubes()
    {
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Create()
    {
        if (obj == null)
        {
            obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            JKLog.Log("不是对象池创建");
        }
        else JKLog.Log("对象池创建");
        base.Create();
    }

    protected override void OnCreate()
    {
        base.OnCreate();
    }
}

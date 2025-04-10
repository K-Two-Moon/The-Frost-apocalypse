using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : Object3D
{
    public override void Create()
    {
        base.Create();
        if(obj==null)
        {
            GameObject meat = GameObject.Instantiate(Resources.Load<GameObject>("Ив"));                                                                                               
        }
    }

    public override void Destroy(bool isRecycle = false)
    {
        base.Destroy(isRecycle);
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Update()
    {
        base.Update();
    }

    protected override void OnCreate()
    {
        base.OnCreate();
    }
}

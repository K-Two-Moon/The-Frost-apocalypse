using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pig : Object3D
{
    Slider Hp;
    public override void Create()
    {
        if (obj == null)
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>("pig"));
            obj.transform.SetParent(parent);
            obj.transform.position = new Vector3(-45 ,0, 15);
           
        }
        base.Create();
    }

    public override void Destroy(bool isRecycle = false)
    {
        base.Destroy(isRecycle);
        //Destroy(Hp.gameObject);
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

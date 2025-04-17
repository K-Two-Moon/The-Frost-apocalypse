using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastPos : Object3D
{
   override public void Create()
    {
        if(obj==null)
        {
            obj=GameObject.Instantiate(Resources.Load<GameObject>("pos"),parent); 
        }
        base.Create();
        
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
        
    }
    protected override void OnCreate()
    {
        
        base.OnCreate();
        
    }
}

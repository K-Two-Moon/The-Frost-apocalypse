using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money :Object3D
{
    
    
    public override void Create()
    {
        if(obj == null)
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>("money"));
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
        base.Update();        
    }

    
    protected override void OnCreate()
    {       
        base.OnCreate();  
          
    }
}

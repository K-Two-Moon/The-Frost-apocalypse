using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastPrefab :Object3D
{
 
    public CastPrefab()
    {

         
    }
    
   
    public override void Create()
    {
        if(obj == null)
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>("Cast"));
            obj.transform.position = new Vector3(-12 ,0, 7);
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

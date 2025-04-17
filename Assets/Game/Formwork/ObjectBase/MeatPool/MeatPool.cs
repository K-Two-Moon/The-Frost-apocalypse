using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatPool :Object3D
{
    Transform player;
    public MeatPool()
    {
        MessAgeController<Transform>.Instance.AddLister(1023, setPlayer);
    }
    public void setPlayer(Transform obj)
    {
        player = obj;
    }   
    public override void Create()
    {
        if(obj == null)
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>("MeatPool"));
            obj.transform.position = new Vector3(0 ,0, 6);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AtkRound :Object3D
{
    PlayableDirector anim;
    public int Count=3;
     override public void Create()
    {
        if(obj==null)
        {
            obj=GameObject.Instantiate(Resources.Load<GameObject>("TouRound"),parent);
            
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
        if(Input.GetMouseButtonUp(0))
        {
            anim.Play();
            Count--;
        }
    }
    protected override void OnCreate()
    {       
        base.OnCreate();   
    }
}

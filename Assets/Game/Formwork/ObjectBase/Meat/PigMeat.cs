using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigMeat : Object3D
{
    public Vector3 pos;
    public Vector3 Chipos;
    public int poolid=0;
    public PigMeat()
    {
        MessAgeController<Vector3>.Instance.AddLister(1030,SetChipos);
    }

    public void SetChipos(Vector3 pos)
    {
        Chipos = pos;
    }
    public override void Create()
    {
        if(obj==null)
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>("meat"));                                                                                               
        }
        
        pos = new Vector3(Random.Range(-5, 5), 0.3f, Random.Range(-5, 5));
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
        if (poolid == 0)
        {
            if (Vector3.Distance(Chipos+pos,obj.transform.position)>0.1f)
            {
                obj.transform.position=Vector3.Lerp(obj.transform.position, Chipos + pos, Time.deltaTime * 2f);
            }
            else
            {        
                poolid=1;
                 MessAgeController<PigMeat>.Instance.SendMessAge(1031, this);
            }
        }
    }

    protected override void OnCreate()
    {
        base.OnCreate();
    }
}

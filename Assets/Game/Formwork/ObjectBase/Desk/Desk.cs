using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : Object3D
{
    public Transform Meatpool;
    public Transform CookedMeat;
    Stack<PigMeat> meatpool=new Stack<PigMeat>();
    Stack<PigMeat> cookedmeat=new Stack<PigMeat>();
    float timer=0.5f;
    public Desk()
    {       
        MessAgeController<PigMeat>.Instance.AddLister(1035, AddMeatPool);
        MessAgeController<int>.Instance.AddLister(1041, SendMyCooker);

    }

    private void SendMyCooker(int obj)
    {
        if(cookedmeat.Count>0)
        {
            MessAgeController<PigMeat>.Instance.SendMessAge(1042, cookedmeat.Pop());
        }     
    }

    public override void Create()
    {
        if(obj==null)
        {
            obj=GameObject.Instantiate(Resources.Load<GameObject>("Desk"));
            obj.transform.position=new Vector3(-12,0.7f,-9);
            Meatpool=obj.transform.Find("MeatPool");
            CookedMeat=obj.transform.Find("CookedMeatPool");
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
        if(meatpool.Count>0)
        {
            timer-=Time.deltaTime;
            if(timer<=0)
            {
                timer=0.5f;
                AddCooker(meatpool.Pop());
            }
        }
       
    }

    protected override void OnCreate()
    {
        base.OnCreate();
    }

    public void AddMeatPool(PigMeat meat)
    {
        meat.Obj.transform.parent=Meatpool;
        meat.Obj.transform.localPosition=new Vector3(0,0.2f,0)*meatpool.Count;
        meatpool.Push(meat);
    }

    public void AddCooker(PigMeat meat)
    {
        meat.Obj.transform.parent=CookedMeat;
        meat.Obj.transform.localPosition=new Vector3(0,0.2f,0)*cookedmeat.Count;
        cookedmeat.Push(meat);
    }
}

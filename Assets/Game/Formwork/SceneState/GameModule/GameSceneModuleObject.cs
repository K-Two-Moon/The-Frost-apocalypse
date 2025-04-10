using JKFrame;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class GameSceneModuleObject : IModule
{
    public CastPrefab castPrefab;
    public Player player;
    public Rokcer rokcer;

    public CastPos castPos;

    public AtkRound AtkRound;

    public Dictionary <Vector3,Pig> Pigs = new Dictionary<Vector3,Pig>();
    List<Meat> meats = new List<Meat>();
    List<Meat> meatschi= new List<Meat>();
    List<Mesh> DeskMeat=new List<Mesh>();
    
    

    bool isCreatPig=false;

    public GameSceneModuleObject(GameState gameState) : base(gameState)
    {
        MessAgeController<int>.Instance.AddLister(1001, GetRoler);
        MessAgeController<int>.Instance.AddLister(1003, GetCastPos);
        MessAgeController<int>.Instance.AddLister(1005, CreatAtkRround);
         MessAgeController<int>.Instance.AddLister(1011, GetCastprefab);
        MessAgeController<int>.Instance.AddLister(1007, Getpig);
        MessAgeController<Dictionary<Vector3,Pig>>.Instance.AddLister(1018, DestroyPigs);
        MessAgeController<int>.Instance.AddLister(1020, DestoryatkRound);
    }

    public void DestoryatkRound(int n)
    {
        AtkRound.Destroy();
        AtkRound = null;

    }
    private void DestroyPigs(Dictionary<Vector3,Pig> @object)
    {
        foreach (var item in @object)
        {
            choisepigs.Add(item.Key, item.Value);
            //Meat meat = new Meat();
            //meat.Initialize();
            //meat.Create();
            //meat.Obj.transform.position=item.Key+new Vector3(Random.Range(-3,3),0,Random.Range(-3,3));
           
            item.Value.Destroy();
            Pigs[item.Key]=null;                   
        }   
        isCreatPig=true;
         MessAgeController<Dictionary<Vector3,Pig>>.Instance.SendMessAge(1014, Pigs);
        
    }
    private void Getpig(int obj)
    {
        MessAgeController<Dictionary<Vector3,Pig>>.Instance.SendMessAge(1014, Pigs);
    }

    private void GetCastprefab(int n)
    {
        MessAgeController<Transform>.Instance.SendMessAge(1012, castPrefab.Obj.transform);
    }

    private void CreatAtkRround(int pos)
    {
        AtkRound = new AtkRound();
        AtkRound.Initialize();
        AtkRound.Create();
        MessAgeController<GameObject>.Instance.SendMessAge(1006, AtkRound.Obj);
    }

    private void GetCastPos(int  n)
    {
        MessAgeController<Vector3>.Instance.SendMessAge(1004,castPos.Obj.transform.position);
    }

    private void GetRoler(int obj)
    {
        MessAgeController<Rokcer>.Instance.SendMessAge(1002,rokcer);
    }

    public override void Initialize()
    {
        base.Initialize();
        castPrefab=new CastPrefab();
        castPrefab.Initialize();
        castPrefab.Create();

        castPos = new CastPos();
        castPos.Initialize();
        castPos.Create();
        castPos.Obj.transform.position = new Vector3(-12.5f, 0, 3.5f);

        rokcer = new Rokcer();
        rokcer.Initialize();
        rokcer.Create();

        player = new Player();
        player.Initialize();
        player.Create();
        player.Obj.transform.position = Vector3.zero + Vector3.up * 0.5f;

        CreatPig();
    }

    public void CreatPig()
    {
        for(int i=0;i<10;i++)
        {
            for(int j=0;j<5;j++)
            {
                Pig pig = new Pig();
                pig.Initialize();
                pig.Create();
                pig.Obj.transform.position+= new Vector3(i * 10,0,j*8);
                pig.Obj.transform.eulerAngles += new Vector3(0, Random.Range(-30, 30), 0);
                Pigs.Add(pig.Obj.transform.localPosition, pig);
            }
        }
        MessAgeController<Dictionary<Vector3,Pig>>.Instance.SendMessAge(1014, Pigs);
    }

    public override void Destroy()
    {

    }
    Dictionary<Vector3,Pig> choisepigs=new Dictionary<Vector3, Pig>();
    private void CreatDestoryPig()
    {
        int count=choisepigs.Count;
        foreach (var item in choisepigs)
        {
            Pig pig = new Pig();
            pig.Initialize();
            pig.Create();
            pig.Obj.transform.position = item.Key;
            Pigs[item.Key] = pig;
        }   
        choisepigs.Clear();
        //CreatMeat(count);
    }

    private void CreatMeat(Vector3 pos)
    {
        int count = 5;
        for(int i=0;i< count; i++)
        {
            Meat meat= new Meat();
            meat.Initialize();
            meat.Create();
            meats.Add(meat);

        }
    }

    float timer=0;
    public override void Update()
    {
       if(isCreatPig)
       {
            timer+=Time.deltaTime;
            if(timer>=1)
            {               
                isCreatPig=false;
                timer=0;     
                CreatDestoryPig();          
            }
       }
    }
}

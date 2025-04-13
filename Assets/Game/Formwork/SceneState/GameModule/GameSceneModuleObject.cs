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

    public  MeatPool meatPool;

    public Desk Desk;

    public CastPos Deskmeat;

    public CastPos DeskCookerMeatPos;

    public Dictionary <Vector3,Pig> Pigs = new Dictionary<Vector3,Pig>();
    List<PigMeat> meats = new List<PigMeat>();
    Stack<PigMeat> meatschi = new Stack<PigMeat>();
    Stack<PigMeat> MyMeats=new Stack<PigMeat>();
    Stack<PigMeat> DeskMeat=new Stack<PigMeat>();
    Stack<PigMeat> CookerMeat=new Stack<PigMeat>();
    
    

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
        MessAgeController<int>.Instance.AddLister(1023, GetMeatPool);
        MessAgeController<int>.Instance.AddLister(1025,GatherMeat); 
        MessAgeController<PigMeat>.Instance.AddLister(1031,AddmeatPool);  
        MessAgeController<int>.Instance.AddLister(1033,SetDeskMeatpos);
        MessAgeController<int>.Instance.AddLister(1034,SetDeskCookerMeatPos);
        MessAgeController<int>.Instance.AddLister(1040, AddDeskMeat);
        MessAgeController<PigMeat>.Instance.AddLister(1042, AddCookerMeat);
    }

    private void AddCookerMeat(PigMeat meat)
    {
        meat.Obj.transform.parent = player.Obj.transform;
        meat.Obj.transform.localPosition=new Vector3(0,0,-2f)+new Vector3(0,0.2f,0)* CookerMeat.Count;
        CookerMeat.Push(meat);
    }

    public void AddDeskMeat(int n)
    {
        if(MyMeats.Count>0)
        {
            MessAgeController<PigMeat>.Instance.SendMessAge(1035, MyMeats.Pop());
        }
    }

    public void SetDeskMeatpos(int n)
    {
        MessAgeController<Transform>.Instance.SendMessAge(1037, Deskmeat.Obj.transform);
    }

    public void SetDeskCookerMeatPos(int n)
    {
        MessAgeController<Transform>.Instance.SendMessAge(1038, DeskCookerMeatPos.Obj.transform);
    }
    public void AddmeatPool(PigMeat meat)
    {
        if(meatPool!=null)
        {
            meatschi.Push(meat);         
        }
    }

    public void GatherMeat(int n)
    {   
        if(meatschi.Count>0)
        {
                meatschi.Peek().Obj.transform.parent=player.Obj.transform;
                meatschi.Peek().Obj.transform.localPosition=new Vector3(0,0,-1)+new Vector3(0,0.2f,0)*MyMeats.Count;
                MyMeats.Push(meatschi.Pop());
        }           
    }
    public void GetMeatPool(int n)
    {
        MessAgeController<Transform>.Instance.SendMessAge(1024, meatPool.Obj.transform);
    }
    public void DestoryatkRound(int n)
    {
        AtkRound.Destroy();
        AtkRound = null;

    }
    private void DestroyPigs(Dictionary<Vector3,Pig> @object)
    {
        List<Vector3> pigposs = new List<Vector3>();
        foreach (var item in @object)
        {
            choisepigs.Add(item.Key, item.Value);
            Vector3 pos = item.Key;
            pigposs.Add(pos);         
            item.Value.Destroy();
            Pigs[item.Key]=null;                   
        }        
        isCreatPig=true;
         MessAgeController<Dictionary<Vector3,Pig>>.Instance.SendMessAge(1014, Pigs);
        for (int i=0;i<pigposs.Count;i++)
        {
            CreatMeat(pigposs[i]);
        }
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
        meatPool = new MeatPool();
        meatPool.Initialize();
        meatPool.Create();


        castPrefab=new CastPrefab();
        castPrefab.Initialize();
        castPrefab.Create();

        castPos = new CastPos();
        castPos.Initialize();
        castPos.Create();
        castPos.Obj.transform.position = new Vector3(-12.5f, 0, 3.5f);

        Desk=new Desk();
        Desk.Initialize();
        Desk.Create();

        Deskmeat=new CastPos();
        Deskmeat.Initialize();
        Deskmeat.Create();
        
        Deskmeat.Obj.transform.position=new Vector3(-9,0,-6);

        DeskCookerMeatPos=new CastPos();
        DeskCookerMeatPos.Initialize();
        DeskCookerMeatPos.Create();
        DeskCookerMeatPos.Obj.transform.position = new Vector3(-9,0,-11);



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
        base.Destroy();
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
            PigMeat meat= new PigMeat();
            meat.Initialize();
            meat.Create();
            meat.Obj.transform.position = pos + new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
            meats.Add(meat);     
        }
        MessAgeController<Vector3>.Instance.SendMessAge(1030, meatPool.Obj.transform.position);
    }

    float timer=0;
    public override void Update()
    {
       if(isCreatPig)
       {
            timer+=Time.deltaTime;
            if(timer>=3)
            {               
                isCreatPig=false;
                timer=0;     
                CreatDestoryPig();          
            }
       }
      
    }
}

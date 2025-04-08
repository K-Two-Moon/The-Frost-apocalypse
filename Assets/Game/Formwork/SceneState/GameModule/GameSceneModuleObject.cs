using JKFrame;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class GameSceneModuleObject : IModule
{
    public Player player;
    public Rokcer rokcer;

    public CastPos castPos;

    public AtkRound AtkRound;

    public Dictionary <Vector3,GameObject> Pigs = new Dictionary<Vector3,GameObject>();
    public GameSceneModuleObject(GameState gameState) : base(gameState)
    {
        MessAgeController<int>.Instance.AddLister(1001, GetRoler);
        MessAgeController<int>.Instance.AddLister(1003, GetCastPos);
        MessAgeController<int>.Instance.AddLister(1005, CreatAtkRround);
        MessAgeController<int>.Instance.AddLister(1007, DestoryRound);
    }

    private void CreatAtkRround(int pos)
    {
        AtkRound = new AtkRound();
        AtkRound.Initialize();
        AtkRound.Create();
        MessAgeController<GameObject>.Instance.SendMessAge(1006, AtkRound.Obj);
    }

    public void DestoryRound(int n)
    {
        AtkRound.Destroy();
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
                Pigs.Add(pig.Obj.transform.localPosition, pig.Obj);

            }
        }
    }

    public override void Destroy()
    {

    }


    public override void Update()
    {
       
    }
}

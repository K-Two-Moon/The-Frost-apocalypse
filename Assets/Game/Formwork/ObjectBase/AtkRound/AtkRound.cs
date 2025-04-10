using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;

public class AtkRound :Object3D
{
    PlayableDirector anim;
    GameObject Ballistic;
    public int Count=3;
    public float timer=0;
    public float duration=3f;
    bool isCast=false;
    Vector3 startPos;
     public Transform pointA;  // 起点
    public Transform pointB;  // 终点
    public Transform controlPoint;  // 控制点
    public LineRenderer lineRenderer;
    public int segments = 30;
   Dictionary<Vector3,Pig> pigs= new Dictionary<Vector3,Pig>();
    Dictionary<Vector3,Pig> choisedPigs= new Dictionary<Vector3,Pig>();
    bool isCastFinsh=false;
    public AtkRound()
    {
        MessAgeController<Transform>.Instance.AddLister(1012, SetCostTrs);
        MessAgeController<Dictionary<Vector3,Pig>> .Instance.AddLister(1014, SetPigs);
        MessAgeController<int>.Instance.AddLister(1015, Openatk);
    }
     public void SetPigs(Dictionary<Vector3,Pig> @object)
    {
        pigs = @object;
    }
     override public void Create()
    {
        if(obj==null)
        {
            obj=GameObject.Instantiate(Resources.Load<GameObject>("TouRound"),parent);
            Ballistic=GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Combat/Missiles/Mystic/MysticMissilePurple"));
            Ballistic.SetActive(false);
            startPos=new Vector3(-14,2,7);
        }
        base.Create();       
    }
    private void SetCostTrs(Transform Costobj)
    {
        pointA = Costobj;
    }
    public override void Destroy(bool isRecycle = false)
    {
        GameObject.Destroy(lineRenderer.gameObject);
        GameObject.Destroy(Ballistic);
        base.Destroy(isRecycle);
        
    }

    public void Openatk(int n)
    {
        obj.SetActive(true);    
        obj.transform.position = new Vector3(-10,0,20);
        Count = 3;
        isCastFinsh=false;
    }
    public override void Initialize()
    {
        base.Initialize();
        MessAgeController<int>.Instance.SendMessAge(1013,0);
        MessAgeController<int>.Instance.SendMessAge(1006, 0);
        MessAgeController<int>.Instance.SendMessAge(1011, 0);
        MessAgeController<int>.Instance.SendMessAge(1013, 0);
        MessAgeController<int>.Instance.SendMessAge(1007, 0);
        base.Initialize();              
        Camera.main.transform.position = new Vector3(-13, 27, -14);
        GameObject line = GameObject.Instantiate(Resources.Load<GameObject>("Line"));
        lineRenderer=line.GetComponent<LineRenderer>();
    }

   
    private Vector3 CalculateBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
         float u = 1 - t;
        float uu = u * u;
        float uuu = uu * u;
        float tt = t * t;
        float ttt = tt * t;

        Vector3 point = uuu * p0;          // (1-t)³ * P0
        point += 3 * uu * t * p1;          // 3(1-t)²t * P1
        point += 3 * u * tt * p2;          // 3(1-t)t² * P2
        point += ttt * p3;                 // t³ * P3

        return point;
    }
    public override void Update()
    {
        base.Update();
        pointB = obj.transform;
        if (obj != null)
        {
        
             foreach(var item in  pigs)
             {
                if (Vector3.Distance(item.Key,obj.transform.position)<10)
                {
                    if(obj.activeInHierarchy)
                    {
                        item.Value.Obj.transform.Find("WolfBossmon_body").GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
                        Debug.Log(item.Key);
                    }                 
                }
                else
                {
                    if(item.Value!=null)
                    {
                        item.Value.Obj.transform.Find("WolfBossmon_body").GetComponent<SkinnedMeshRenderer>().material.color = Color.white;
                    }
                    
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    obj.transform.position = new Vector3(-10, 0, 20);
                    lineRenderer.gameObject.SetActive(false);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (obj.activeInHierarchy)
                {
                    foreach (var item in pigs)
                    {
                        if (Vector3.Distance(item.Key, obj.transform.position) < 10)
                        {
                            choisedPigs.Add(item.Key, item.Value);
                        }
                    }
                    if (Ballistic != null)
                    {
                        Ballistic.SetActive(true);
                        isCast = true;
                        Ballistic.transform.position = startPos;
                        lineRenderer.gameObject.SetActive(false);
                    }                
                }                                   
                //anim.Play();              
            }
            if (isCast)
            {
                timer += Time.deltaTime;
                float t = timer / duration;
                Vector3 controlerpos = (Ballistic.transform.position + obj.transform.position) / 2 + Vector3.up * 20;
                Ballistic.transform.position = CalculateBezierPoint(
                    new Vector3(-14, 2, 7),
                    startPos,
                    controlerpos,
                    obj.transform.position,
                    t
                );
                if (timer >= 3)
                {
                    timer = 0;
                    isCast = false;
                    Ballistic.SetActive(false);
                    GameObject.Destroy(GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Combat/Explosions/MysticExplosion/MysticExplosionOrange"), obj.transform.position + Vector3.up * 2, obj.transform.rotation), 1);
                    obj.SetActive(false);
                    if(choisedPigs.Count >0)
                    {
                        MessAgeController<Dictionary<Vector3, Pig>>.Instance.SendMessAge(1018, choisedPigs);
                    }
                    choisedPigs.Clear();
                    Count--;
                    if (Count <= 0)
                    {
                        if(!isCastFinsh)
                        {
                            MessAgeController<int>.Instance.SendMessAge(1020, 0);
                            MessAgeController<int>.Instance.SendMessAge(1021, 0);
                            isCastFinsh = true;                            
                        }                      
                    }
                }
            }
            if (lineRenderer != null)
            {
                DrawBezierCurve();
            }
        }
        
       
    }

    void DrawBezierCurve()
    {
        lineRenderer.positionCount = segments + 1;
        for (int i = 0; i <= segments; i++)
        {
            float t = i / (float)segments;
            Vector3 midpoint = (pointA.position + pointB.position) / 2f+new Vector3(0, 20, 0);
            Vector3 point = CalculateBezierPoint(t, pointA.position, midpoint, pointB.position);
            lineRenderer.SetPosition(i, point);
        }
    
    }
    public Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        return (uu*p0)+(2*u*t*p1)+(tt*p2);
    }
    protected override void OnCreate()
    {       
        base.OnCreate();   
    }
}

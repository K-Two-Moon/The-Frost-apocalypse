using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Creatpig : MonoBehaviour
{
    public GameObject pihprefab;
    public int pigcount = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<30;i++)
        {
            GameObject pig=Instantiate(pihprefab);
            Vector2 pos=Random.insideUnitSphere*15;
            pig.transform.position=new Vector3(pos.x,0,pos.y)+transform.position;
            pigcount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(pigcount<30)
        {
            GameObject pig=Instantiate(pihprefab);
            Vector2 pos=Random.insideUnitCircle*15;
            pig.transform.position=new Vector3(pos.x,0,pos.y)+transform.position;
            pigcount++;
        }
    }
}

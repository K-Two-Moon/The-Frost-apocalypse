using log4net.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Rokcer : ObjectUI
{
    Image joy;
    RectTransform JoyRect;
    float R = 100;
    Vector3 startpos;
    float dis;

    Vector3 dir;
    public override void Create()
    {
        AddComponent<DragComponent>();
        base.Create();
    }

    public override void Destroy(bool isRecycle = false)
    {
        base.Destroy(isRecycle);
    }

    public override void Initialize()
    {
        base.Initialize();  
        obj = GameObject.Instantiate(Resources.Load<GameObject>("Rocker"), cavansTransform);
        joy= obj.transform.GetChild(0).GetComponent<Image>();
        JoyRect = joy.GetComponent<RectTransform>();
        startpos = joy.transform.position;
    }

    public override void Update()
    {
       
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        
    }
   
}

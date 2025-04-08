using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class DragComponent :IComponent
{
    public float speed;
    public float ang;
    RectTransform joy;
    public override void Initialize(IGameObject obj)
    {
        base.Initialize(obj);
        joy=obj.Obj.transform.GetChild(0).GetComponent<RectTransform>();
        DragManager.Get(obj.Obj).IDrag+=onDrag;
        DragManager.Get(obj.Obj).IEndDrag+=onEndDrag;
    }

    public override void Destroy()
    {
        base.Destroy();
    }

    public override void Update()
    {
        base.Update();      
    }
    private void onDrag(PointerEventData eventData)
    {
        Debug.Log("拖动中"+obj.Obj.name);
        Vector2 pos=obj.Obj.transform.InverseTransformPoint(eventData.position);
        float r=obj.Obj.GetComponent<RectTransform>().sizeDelta.x/2;
        joy.anchoredPosition=Vector2.ClampMagnitude(pos,r);
        speed=Vector2.Distance(Vector2.zero,joy.anchoredPosition);
        ang=Mathf.Atan2(joy.anchoredPosition.x,joy.anchoredPosition.y)*Mathf.Rad2Deg;

    }

    private void onEndDrag(PointerEventData eventData)
    {
        Debug.Log("结束拖动"+obj.Obj.name);
        joy.anchoredPosition=Vector2.zero;
        speed=0;
    }

}

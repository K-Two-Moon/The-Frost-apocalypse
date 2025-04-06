using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class EctJoy : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public RectTransform Rect;
    float R = 100;
    Vector3 startpos;
    float dis;

    Vector3 dir;

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        dis=Vector3.Distance(Input.mousePosition,startpos);
        if(dis<R)
        {
            dir=Input.mousePosition - startpos;
            transform.position=dir.normalized*R+startpos;
        }
        else
        {
            transform.position=Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Rect.anchoredPosition = startpos;
    }

    public float SetMove(string str)
    {
       if(str=="H")
       {
            return Rect.anchoredPosition.x/R;
       }
       else if(str=="V")
       {
            return Rect.anchoredPosition.y/R;
       }
       return 0;
    }
    private void Start()
    {
        Rect = GetComponent<RectTransform>();
        startpos=Rect.anchoredPosition;
    }
}

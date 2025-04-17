using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public event System.Action<PointerEventData> IIPointer;
    public event System.Action<PointerEventData> IBegin;
    public event System.Action<PointerEventData> IDrag;
    public event System.Action<PointerEventData> IEndDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        IBegin?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        IDrag?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        IEndDrag?.Invoke(eventData);
    }

    // Start is called before the first frame update
    public static DragManager Get(GameObject go)
    {
        DragManager bangDing = null;
        if(go.TryGetComponent<DragManager>(out bangDing))
        {
            
        }
        else
        {
            bangDing=go.AddComponent<DragManager>();
        }
        return bangDing;
    }
}

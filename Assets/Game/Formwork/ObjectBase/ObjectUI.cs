using UnityEngine;

public abstract class ObjectUI : IGameObject
{
    protected static Transform cavansTransform;

    public ObjectUI() 
    {
        if (cavansTransform == null)
        {
            cavansTransform = GameObject.Find("Canvas").transform;
            if (cavansTransform == null)
            {
                Debug.LogError("Canvas not found");
            }
        }
    }

    public override void Create()
    {
        Obj.transform.SetParent(cavansTransform);
        base.Create();
    }
}




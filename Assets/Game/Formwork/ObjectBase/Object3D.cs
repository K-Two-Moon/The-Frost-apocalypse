using UnityEngine;

public abstract class Object3D : IGameObject
{
    protected static Transform parent;

    public Object3D(string name) : base(name)
    {
        if (parent == null)
        {
            parent = GameObject.Find("Parent3D")?.transform;
            if (parent == null)
            {
                parent = new GameObject("Parent3D").transform;
                parent.position = Vector3.zero;
            }
        }
    }

    public override void Create()
    {
        base.Create();
        obj.name = Id.ToString();
    }
}
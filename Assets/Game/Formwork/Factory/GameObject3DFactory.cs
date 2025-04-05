using UnityEngine;


/// <summary>
/// 产品类型
/// </summary>
public enum Object3DType
{


}


/// <summary>
/// 3D对象工厂
/// </summary>
public static class Object3DFactory
{
    public static IGameObject CreateProduct(Object3DType type)
    {
        IGameObject product = null;
        product = SwitchBuilder(type);

        if (product == null)
            Debug.LogError("创建失败");

        // 返回产品
        return product;
    }

    private static IGameObject SwitchBuilder(Object3DType type) => type switch
    {

        _ => null,
    };
}



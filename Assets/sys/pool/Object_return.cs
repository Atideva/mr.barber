using UnityEngine;

public class Object_return : MonoBehaviour
{
    pool myPool;
    public void SetPool(pool obj) => myPool = obj;
    void OnDisable() => myPool.ReturnToPool(gameObject);
}

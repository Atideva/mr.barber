using UnityEngine;

public class pool : MonoBehaviour
{
    public virtual GameObject Create(Vector2 pos, Quaternion rot) { return null; }
    public virtual void ReturnToPool(GameObject obj) { }

}

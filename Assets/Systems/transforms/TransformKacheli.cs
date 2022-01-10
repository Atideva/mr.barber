using UnityEngine;

public class TransformKacheli : MonoBehaviour
{
    public float speed;
    public float angle;
    float z, z0;

    void OnEnable()
    {
        z0 = transform.localEulerAngles.z;
        z = z0;
    }


    bool way = true;
    void Update()
    {
        float step = speed * 10 * Time.deltaTime;

        if (way)
        {
            z += step;
            if (z > z0 + angle) way = !way;
        }
        else
        {
            z -= step;
            if (z < z0 - angle) way = !way;
        }
        transform.localEulerAngles = new Vector3(0, 0, z);

    }



}

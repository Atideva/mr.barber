
using System.Collections;
using UnityEngine;

public class CameraDynamicSize : MonoBehaviour
{
   Camera cam;
    public bool dynamicSize;
    public float sizeAddPerSpd;
    public float size;
    public float sizeStart=20;
    public float camSize;
    public iMotor2D_human motor;
    public float stepTriger;
    void Start()
    {
        if (!dynamicSize) enabled = false;
        if (!cam) cam=GetComponent<Camera>();
        cam.orthographicSize = sizeStart;
        size = cam.orthographicSize;
        camSize = size;
        desiredSize = size;
    }
    float desiredSize;
    void  Update()
    {
        float newSize = size + motor.currentSpeed * sizeAddPerSpd;
        float change = newSize - desiredSize;
        if (Mathf.Abs(change) > stepTriger)
        {
            desiredSize = newSize;
            StartCoroutine(changeMake(change));
        }
        cam.orthographicSize = camSize;
    }
    public float changeTime=1f;
    IEnumerator changeMake(float dif)
    {
        float t = 0;
        float timer = changeTime;
        float step = dif / timer;
        while (t < timer)
        {
            t += Time.deltaTime;
            camSize += step * Time.deltaTime;
            yield return null;
        }
    }
}

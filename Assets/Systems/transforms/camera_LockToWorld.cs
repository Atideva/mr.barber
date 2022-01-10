using UnityEngine;

public class camera_LockToWorld : MonoBehaviour
{
    Camera cam;
    Transform t;

    World_size World_Data;
    Vector2 screenBounds;
    float Xsize, Ysize;
    float x, y;
    float z;

    void Start()
    {
        //cams
        cam = GetComponent<Camera>();
        t = cam.transform;
        z = t.position.z;

        //world size
        World_Data = World_size.Instance;
        Xsize = World_Data.width ;
        Ysize = World_Data.height ;
        x = 0;y = 0;
        //x = World_Data.World_Position.x;
        //y = World_Data.World_Position.y;
    }

    void  LateUpdate()
    {
        t.localPosition = new Vector3(0, 0, z);
        //cam position
        Vector2 camPos = t.position;

        //cam bounds
        screenBounds = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        //cam screen width & height
        float camWidth = screenBounds.x - camPos.x;
        float camHeight = screenBounds.y - camPos.y;

        //clamp it to World Size, do not allow to go out
        camPos.x = Mathf.Clamp(camPos.x, -(Xsize - camWidth) + x, (Xsize - camWidth) + x);
        camPos.y = Mathf.Clamp(camPos.y, -(Ysize - camHeight) + y, (Ysize - camHeight) + y);
        t.position = new Vector3(camPos.x, camPos.y, z);


    }

}

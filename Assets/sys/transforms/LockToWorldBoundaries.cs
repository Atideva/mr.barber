using UnityEngine;

public class LockToWorldBoundaries : MonoBehaviour
{
    [SerializeField] float objectSize;

    World_size World_Data;
   float XSize, YSize;
    float x, y;

    Vector3 p;
    [SerializeField] Transform main;


    void Start()
    {
        World_Data = World_size.Instance;
        XSize = World_Data.width;
        YSize = World_Data.height;
        x = 0; y = 0;
        //x = World_Data.World_Position.x;
        //y = World_Data.World_Position.y;
    }


    void Update()
    {
        p = main.position;
        p.x = Mathf.Clamp(p.x, -(XSize - objectSize) + x, (XSize - objectSize) + x);
        p.y = Mathf.Clamp(p.y, -(YSize - objectSize) + y, (YSize - objectSize) + y);
        main.position = p;
    }
}

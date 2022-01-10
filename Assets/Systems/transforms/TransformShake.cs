using UnityEngine;

public class TransformShake : MonoBehaviour
{

    [Header("X")]
    [SerializeField] float startX;
    [SerializeField] float endX;
    [SerializeField] float Xspeed;
    [Header("Y")]
    [SerializeField] float startY;
    [SerializeField] float endY;
    [SerializeField] float Yspeed;
    bool yWay;
    bool xWay;
    Transform t;
    float x, y;
    void Start()
    {
        t = transform;
        x = startX;
        y = startY;
    }

    void FixedUpdate()
    {
        if (yWay)
        {
            y += Yspeed * Time.fixedDeltaTime;
            if (y > endY) yWay = false;
        }
        else
        {
            y -= Yspeed * Time.fixedDeltaTime;
            if (y < startY) yWay = true;
        }

        if (xWay)
        {
            x += Xspeed * Time.fixedDeltaTime;
            if (x > endX) xWay = false;
        }
        else
        {
            x -= Xspeed * Time.fixedDeltaTime;
            if (x < startX) xWay = true;
        }

        t.localScale = new Vector2(x, y);
    }

}

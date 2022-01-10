using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPosMove : MonoBehaviour
{
    [Header("X")]
    [SerializeField] float offxetX;
    [SerializeField] float Xspeed;
    bool xWay;
    Transform t;
    float x, startX;
    float y, startY;
    public bool loopX;
    public float xDelay,xdel;
    void Start()
    {
        t = transform;
        startX = t.localPosition.x;
        startY = t.localPosition.y;
        x = startX;
        y = startY;

    }
    float ofst;
    void Update()
    {
        if (xWay)
        {
            xdel += Time.deltaTime;
            if (xdel >= xDelay)
            {
                x += Xspeed * Time.deltaTime;
                if (x > startX + offxetX)
                {
                    xWay = false;
                    xdel = 0;
                }
            }
        }
        else
        {
            if (loopX)
            {
                x -= Xspeed * Time.deltaTime;
                if (x < startX) xWay = true;
            }
            else
            {
                xdel += Time.deltaTime;
                if (xdel >= xDelay)
                {
                    xdel = 0;
                    xWay = true;
                    x = startX;
                }
            }
        }

        t.localPosition = new Vector2(x, startY);
    }
}

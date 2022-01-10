using UnityEngine;

public class Human_Anim_foots : MonoBehaviour
{
    public Transform left;
    public Transform right;
    public iMotor2D_human engine;
    public float animSpeed;
    [Header("Anim Settings")]
    public float footUP;
    public bool up;
    public bool dependsOnSpeed;

   public bool right_anim;
    public float y;
    float xL, xR;
    void Start()
    {
        xL = left.transform.localPosition.x;
        xR = right.transform.localPosition.x;
    }
    public void Shock()
    {
        //animSpeed *= 2;
    }
    void Update()
    {
        float spd = engine.currentSpeed / 10;
        float step;
        if (dependsOnSpeed)
         step = animSpeed * Time.deltaTime * spd;
        else
         step = animSpeed * Time.deltaTime / 10;

        if (up)
        {
            y += step;
            if (y >= footUP) { y = footUP; up = !up; }
        }
        else
        {
            y -= step;
            if (y <= 0) { y = 0; up = !up; right_anim = !right_anim; }
        }

        if (right_anim)
        {
            right.localPosition = new Vector2(xR, y);
        }
        else
        {
            left.localPosition = new Vector2(xL, y);
        }
    }
}

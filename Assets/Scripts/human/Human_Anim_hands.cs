using UnityEngine;

public class Human_Anim_hands : MonoBehaviour
{
    public Transform left;
    public Transform right;

    public float animSpeed;
    public float handUpY;
    float y0;
    [Header("Anim Settings")]
    public float footUP;
    public bool up;

    public bool right_anim;

    float xL, xR, y;
    void Start()
    {
        xL = left.transform.localPosition.x;
        xR = right.transform.localPosition.x;
    }
        bool shakeHands;
    public void Shock()
    {
        left.transform.localPosition = new Vector2(xL, handUpY);
        right.transform.localPosition = new Vector2(xR, handUpY);
        footUP += handUpY;
        y0 = handUpY;
        shakeHands = true;
    }
    void Update()
    {
        if (!shakeHands) return;

        float step = animSpeed * Time.deltaTime / 10;

        if (up)
        {
            y += step;
            if (y >= footUP) { y = footUP; up = !up; }
        }
        else
        {
            y -= step;
            if (y <= y0) { y = y0; up = !up; right_anim = !right_anim; }
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

using System.Collections;
using UnityEngine;

public class Police_anim : MonoBehaviour
{
    public AudioEvent whistleSound;
    public SpriteRenderer star;
    public Sprite wantedSprite;
    public Sprite normalSprite;

    public Transform whistle;
    public Transform mouth;
    public Transform hand;
    public Transform glasses;
    public Vector2 whistleMouthPos;
    public float whistleMouthZrot = 75;
    Vector2 whistleHandPos;
    bool wantedStage;
    public Transform stick;
    public GameObject stickShadow;
    public GameObject whistlehadow;
    public GameObject stickFlare;
    public float stickSpeed;
    public float stickAngle;
    public Human_Anim_shocked animEye;

    void Start()
    {
        whistleHandPos = whistle.localPosition;
    }

    public void DropItems()
    {
        StopCoroutine(stickAnim);
        stickFlare.SetActive(false);
        stick.localEulerAngles = new Vector3(0, 0, 90);
        whistle.localEulerAngles = new Vector3(0, 0, 270);
        glasses.transform.localPosition = new Vector2(0, 0.6f);
        animEye.PoliceEye();
        StartCoroutine(DropItem(whistle, 3, whistlehadow));
        StartCoroutine(DropItem(stick, 5, stickShadow));
    }

    IEnumerator DropItem(Transform t, float dist, GameObject shadow)
    {
        t.parent = null;
        float a = 0;
        float fallSpeed = 9f;
        while (a < dist)
        {
            yield return null;
            a += Time.deltaTime * fallSpeed;
            t.Translate(Vector2.down * Time.deltaTime * fallSpeed, Space.World);
        }

        shadow.SetActive(true);
    }

    public void WhistleSound()
    {
        if (whistleSound) AudioManager.Instance.PlaySimpleEvent(whistleSound);
    }

    public void CrimeCommited()
    {
        if (!wantedStage)
        {
            wantedStage = true;
            star.sprite = wantedSprite;
            whistle.parent = mouth;
            whistle.localPosition = whistleMouthPos;
            whistle.localEulerAngles = new Vector3(0, 0, whistleMouthZrot);
            stickAnim = StartCoroutine(StickAnimation());
        }
    }

    Coroutine stickAnim;

    IEnumerator StickAnimation()
    {
        stickFlare.SetActive(true);
        bool way = true;
        float z0 = stick.localEulerAngles.z;
        float z = z0;
        while (true)
        {
            yield return null;
            float step = stickSpeed * Time.deltaTime;

            if (way)
            {
                z += step;
                if (z > z0 + stickAngle) way = !way;
            }
            else
            {
                z -= step;
                if (z < z0 - stickAngle) way = !way;
            }

            stick.localEulerAngles = new Vector3(0, 0, z);
        }
    }
}
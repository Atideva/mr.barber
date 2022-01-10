
using System.Collections;
using UnityEngine;

public class Barber_busted_anim : MonoBehaviour
{
    public Transform hairClipper;
    public GameObject hairClipperShadow;
    public GameObject handcliff;
    public Transform glasses;
    public SpriteRenderer eyeLeft;
    public SpriteRenderer eyeRight;
    public SpriteRenderer mouth;
    public Sprite mouthShocked;
    public Sprite eyeShocked;
    public AudioEvent bustedSound;
    void Start()
    {
        Events_Barber.Instance.On_BarberBusted += Busted;
    }

    void Busted()
    {
        Events_Barber.Instance.On_BarberBusted -= Busted;
        if (bustedSound) AudioManager.Instance.PlaySimpleEvent(bustedSound);
        handcliff.SetActive(true);
        eyeLeft.sprite = eyeShocked;
        eyeRight.sprite = eyeShocked;
        //mouth.sprite = mouthShocked;
        float x = Random.Range(1.5f, 1.5f);
        float y = Random.Range(1.5f, 1.5f);
        float xN = (Random.value > 0.5f) ? 1 : -1;
        float yN = (Random.value > 0.5f) ? 1 : -1;
        glasses.transform.localPosition = new Vector2(0, -1.9f);
        hairClipper.eulerAngles = new Vector3(0, 0, 0);
        StartCoroutine(dropItem(hairClipper, 2f, hairClipperShadow));
    }

    IEnumerator dropItem(Transform t, float dist, GameObject shadow)
    {
        t.parent = null;
        float a = 0;
        float fallSpeed = 5f;
        while (a < dist)
        {
            yield return null;
            a += Time.deltaTime * fallSpeed;
            t.Translate(Vector2.down * Time.deltaTime * fallSpeed, Space.World);
        }
        shadow.SetActive(true);
    }

}

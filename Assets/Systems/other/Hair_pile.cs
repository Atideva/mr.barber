using System.Collections;
using UnityEngine;

public class Hair_pile : MonoBehaviour
{
    public Transform sprite;
    public SpriteRenderer pile;
    public bool randSize;
    public float sizeMin=0.75f;
    public float sizeMax=1.25f;
    public bool fallDown;
    public float fallDistance=3;
    public float fallTime=1;
    [Header("WindFlow")]
    public float windRange=2;
    public float windSpeed = 2;
    public TransformRotation rot;
    public GameObject shadow;
    public void SetColor(Color clr)
    {
        pile.color = clr;
    }
    void Start()
    {
        if (randSize)
        {
            float x = Random.Range(sizeMin, sizeMax);
            float y = Random.Range(sizeMin, sizeMax);
            sprite.localScale = new Vector2(x, y);
        }
        if (fallDown) StartCoroutine(Fall());
    }

    IEnumerator Fall()
    {
        shadow.SetActive(false);
        float d = 0;
        float speed = fallDistance / fallTime;
        Coroutine wind = StartCoroutine(WindFlow());
        float r = Random.Range(0.8f, 1.2f);
        speed *= r;
        while (d < fallDistance)
        {
            d += speed * Time.deltaTime;
            transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
            yield return null;
        }
        StopCoroutine(wind);
        rot.enabled = false;
        shadow.SetActive(true);
    }
    IEnumerator WindFlow()
    {
        bool left = Random.value > 0.5f ? true : false;
        float d = 0;
        float x = left ? -windRange : windRange;
        sprite.localPosition = new Vector2(x, 0);
        float r = Random.Range(0.5f, 1.5f);
        windSpeed *= r;
        while (true)
        {
            d += Time.deltaTime * windSpeed;
            if (d >= windRange) { d = 0; left = !left; }
            Vector2 dir = left ? Vector2.left : Vector2.right;
            sprite.Translate(dir * Time.deltaTime * windSpeed, Space.World);
            yield return null;
        }
    }
}

using System.Collections;
using UnityEngine;

public class Banana : MonoBehaviour
{
    bool move;
    public Vector2 poz;
    public GameObject banan;
    public GameObject peel;
    public AudioEvent bananaSmashed;
    public GameObject banan_peel_shadow;
    public TransformRotation rotate;
    public bool rotateAtstart;
    void Start()
    {
        rotate.enabled = rotateAtstart ? true : false;
        peel.SetActive(false);
    }

    public void RotateBanana(bool rot) => rotate.enabled = rot;
    public void TrhowBanana(Vector2 pos, float speed)
    {
        poz = pos;
        if (!move)
        {
            StartCoroutine(Move(pos, speed));
            Instantiate(banan_peel_shadow, pos, Quaternion.identity);
        }
    }

    IEnumerator Move(Vector2 pos, float speed)
    {
        move = true;
        transform.parent = null;
        float dist = Vector2.Distance(pos, transform.position);
        while (dist > 0.5f)
        {
            dist = Vector2.Distance(pos, transform.position);
            Vector2 dir = pos - (Vector2)transform.position;
            transform.Translate(dir * speed * Time.deltaTime, Space.World);
            yield return null;
        }
        transform.position = pos;
        transform.eulerAngles = new Vector3(0, 0, 0);
        ActiavtePeel();
    }
    void OnDrawGizmos()
    {
        if (move) Gizmos.DrawWireSphere(poz, 1);
    }
    void ActiavtePeel()
    {
        banan.SetActive(false);
        peel.SetActive(true);
        if (bananaSmashed) AudioManager.Instance.PlaySimpleEvent(bananaSmashed);
    }
}

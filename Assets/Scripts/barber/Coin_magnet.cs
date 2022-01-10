using UnityEngine;

public class Coin_magnet : MonoBehaviour
{
    Transform barber;
    float speed;
    public Transform main;
    bool magnet;
    void OnEnable()
    {
        if (My_GameManager.Instance.magnet)
        {
            magnet = false;
            barber = My_GameManager.Instance.barber.transform;
            speed = My_GameManager.Instance.magnetSpeed;
            Invoke("Magnet", My_GameManager.Instance.magnetDelay);
        }
    }
    void Magnet() => magnet = true;

    void Update()
    {
        if (!magnet) return;
        Vector2 dir = (Vector2)barber.position - (Vector2)transform.position;
        main.Translate(dir * speed * Time.deltaTime, Space.World);
    }
}

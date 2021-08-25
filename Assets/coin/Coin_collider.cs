using UnityEngine;

public class Coin_collider : MonoBehaviour
{
    public GameObject main;
    public AudioEvent pickupSound;
    public int value=1;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Barber"))
        {
            if (pickupSound) AudioManager.Instance.PlaySimpleEvent(pickupSound);
            Events_Resourses.Instance.CoinPickup(value);
            main.SetActive(false);
        }
    }
}

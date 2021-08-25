using UnityEngine;

public class Barber_busted_clipper : MonoBehaviour
{
    public GameObject colider;
    void Start()
    {
        Events_Barber.Instance.On_BarberBusted += Busted;
    }

   void Busted()
    {
        colider.SetActive(false);
    }

   
}

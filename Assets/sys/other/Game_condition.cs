
using TMPro;
using UnityEngine;

public class Game_condition : MonoBehaviour
{


    void Start()
    {
        Events_Barber.Instance.On_BarberBusted += Busted;
    }

    void Busted()
    {
        
    }

   
}

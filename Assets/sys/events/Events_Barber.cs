using System;
using UnityEngine;

public class Events_Barber : MonoBehaviour
{
    #region Singleton
    //-------------------------------------------------------------
    public static Events_Barber Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else gameObject.SetActive(false);
    }
    //-------------------------------------------------------------
    #endregion


    //------------------------------------------------------------------------------
    // HAIR CLIPPER
    //------------------------------------------------------------------------------
    public event Action<Vector2,bool,bool> On_HairClipped = delegate { };
    public void HairClipped(Vector2 pos,bool isPolice,bool policeHairOff)
    {
        On_HairClipped(pos, isPolice, policeHairOff);
    }

    //------------------------------------------------------------------------------
    // BARBER BUSTED!!!
    //------------------------------------------------------------------------------
    public event Action On_BarberBusted = delegate { };
    public void BarberBusted()
    {
        On_BarberBusted();
    }

    //------------------------------------------------------------------------------
    // HUMAN FLEE
    //------------------------------------------------------------------------------
    public event Action<Transform> On_HumanFlee = delegate { };
    public void HumanFlee(Transform coliderTransform)
    {
        On_HumanFlee(coliderTransform);
    }


}

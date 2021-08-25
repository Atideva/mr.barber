using System;
using UnityEngine;

public class Events_Police : MonoBehaviour
{
    #region Singleton
    //-------------------------------------------------------------
    public static Events_Police Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else gameObject.SetActive(false);
    }
    //-------------------------------------------------------------
    #endregion


    //------------------------------------------------------------------------------
    // CRIME COMMITED
    //------------------------------------------------------------------------------
    public event Action<Police,int> On_CrimeCommited = delegate { };
    public void CrimeCommited(Police police, int wantedLvl)
    {
        On_CrimeCommited(police,wantedLvl);
    }

    //------------------------------------------------------------------------------
    // POLICE FLEE
    //------------------------------------------------------------------------------
    public event Action<Police> On_PoliceFlee = delegate { };
    public void PoliceFlee(Police police)
    {
        On_PoliceFlee(police);
    }

    //------------------------------------------------------------------------------
    // On_Duty
    //------------------------------------------------------------------------------
    public event Action On_At_Duty = delegate { };
    public void At_Duty()
    {
        On_At_Duty();
    }
    //------------------------------------------------------------------------------
    // No_On_Duties
    //------------------------------------------------------------------------------
    public event Action  On_No_On_Duties = delegate { };
    public void No_On_Duties( )
    {
        On_No_On_Duties();
    }



    //------------------------------------------------------------------------------
    // CRIME COMMITED
    //------------------------------------------------------------------------------
    public event Action  On_MonkeyThrowBanana = delegate { };
    public void MonkeyThrowBanana()
    {
        On_MonkeyThrowBanana( );
    }
}

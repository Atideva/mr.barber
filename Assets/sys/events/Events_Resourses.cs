using System;
using UnityEngine;

public class Events_Resourses : MonoBehaviour
{
    #region Singleton
    //-------------------------------------------------------------
    public static Events_Resourses Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else gameObject.SetActive(false);
    }
    //-------------------------------------------------------------
    #endregion


    //------------------------------------------------------------------------------
    // COIN DROP
    //------------------------------------------------------------------------------
    public event Action<Vector2,int> On_CoinDrop = delegate { };
    public void CoinDrop(Vector2 pos, int count)
    {
        On_CoinDrop(pos, count);
    }


    //------------------------------------------------------------------------------
    // COIN PICKUP
    //------------------------------------------------------------------------------
    public event Action<int> On_CoinPickup = delegate { };
    public void CoinPickup(int count)
    {
        On_CoinPickup(count);
    }

}

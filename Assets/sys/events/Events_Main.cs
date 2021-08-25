using System;
using UnityEngine;

public class Events_Main : MonoBehaviour
{
    #region Singleton
    //-------------------------------------------------------------
    public static Events_Main Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else gameObject.SetActive(false);
    }
    //-------------------------------------------------------------
    #endregion


    //------------------------------------------------------------------------------
    // PLAY BUTTON PRESED
    //------------------------------------------------------------------------------
    public event Action On_PlayButton_Pressed = delegate { };
    public void PlayButton_Pressed( )
    {
        On_PlayButton_Pressed( );
    }

    //------------------------------------------------------------------------------
    // LEVEL COMPLETE
    //------------------------------------------------------------------------------
    public event Action On_LevelComplete = delegate { };
    public void LevelComplete()
    {
        On_LevelComplete();
    }

}

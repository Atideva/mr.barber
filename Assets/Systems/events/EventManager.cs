using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Singleton
    //-------------------------------------------------------------
    public static EventManager Instance;
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
    public void PlayButton_Pressed() => On_PlayButton_Pressed();


    //------------------------------------------------------------------------------
    // LEVEL COMPLETE
    //------------------------------------------------------------------------------
    public event Action On_LevelComplete = delegate { };
    public void LevelComplete() => On_LevelComplete();


    //------------------------------------------------------------------------------
    // LEVEL REWARD
    //------------------------------------------------------------------------------
    public event Action<int> On_LevelReward = delegate { };
    public void LevelReward(int gems) => On_LevelReward(gems);


}

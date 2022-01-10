using System.Collections.Generic;
using UnityEngine;

public class Barber_game_starter : MonoBehaviour
{
    public List<GameObject> objectsToEnable = new List<GameObject>();
    void Start()
    {
        EventManager.Instance.On_PlayButton_Pressed += StartGame;
        StatusChange(false);
    }

    void StartGame() => StatusChange(true);


    void StatusChange(bool status)
    {
        foreach (var item in objectsToEnable)
            item.SetActive(status);
    }
}

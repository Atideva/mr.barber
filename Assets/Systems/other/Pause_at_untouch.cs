using System;
using UnityEngine;

public class Pause_at_untouch : MonoBehaviour
{
    joysticks joy;
    public bool pauseAtUntouch=true;
    public float unhtouchTime=0.1f;
    float timer;
    bool pause;
    public GameObject pauseCanvas;
    void Start()
    {
        if (!pauseAtUntouch) enabled = false;
        joy = joysticks.Instance;
        EventManager.Instance.On_PlayButton_Pressed += Activate;
        EventManager.Instance.On_LevelComplete += Disable;
        Events_Barber.Instance.On_BarberBusted += Disable;
    }

    bool activate_module;
    void Activate()
    {
        activate_module = true;
        timer = 0;
    }
    void Disable()
    {
        activate_module = false;
        TimeManager.Instance.UN_PauseGame();
    }


#if UNITY_STANDALONE || UNITY_EDITOR
#else
    void Update()
    {
        if (!activate_module) return;

        if (!joy.LeftJoystick_Is_Working)
        {
            timer += Time.deltaTime;
            if (timer > unhtouchTime)
            {
                pause = true;
                TimeManager.Instance.PauseGame();
                pauseCanvas.SetActive(true);
            }
        }
        else
        {
            timer = 0;
            if (pause)
            {
                pause = false;
                TimeManager.Instance.UN_PauseGame();
                pauseCanvas.SetActive(false);
            }
        }

    }
#endif
}

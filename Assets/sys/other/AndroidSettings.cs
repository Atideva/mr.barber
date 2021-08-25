using System;
using UnityEngine;
 
public class AndroidSettings : MonoBehaviour
{
    #region Singleton
    //-------------------------------------------------------------
    public static AndroidSettings Instance;
    void Awake()
    {
        FakeAwake();
        if (Instance == null) Instance = this;
        else gameObject.SetActive(false);
    }
    //-------------------------------------------------------------
    #endregion

    [SerializeField] bool resetTimeScale;
    [SerializeField] bool neverSleepScreen = true;
    [SerializeField] bool androidBackButton = true;
    [SerializeField] bool debugLoggerDisable = true;
   public bool vibrate = true;
    void FakeAwake()
    {
        if (debugLoggerDisable)
            Debug.unityLogger.logEnabled = false;
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#endif
    }
    void Start()
    {
    Events_Barber.Instance.On_HairClipped += VibrateCreate;

        if (resetTimeScale)
            Time.timeScale = 1.0f;

        if (neverSleepScreen)
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if (!androidBackButton)
            gameObject.SetActive(false);
    }

  public   void VibrateCreate(Vector2 arg1, bool arg2, bool arg3)
    {
#if UNITY_STANDALONE
#else
        if (vibrate) Handheld.Vibrate();
#endif
    }
 

    void FixedUpdate()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
                // Insert Code Here (I.E. Load Scene, Etc)
                // OR Application.Quit();

                return;
            }
        }
    }

}

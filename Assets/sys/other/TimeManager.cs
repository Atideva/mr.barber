using UnityEngine;
using UnityEngine.Audio;

public class TimeManager : MonoBehaviour
{
    
    #region Singleton 
    //-------------------------------------------------------------
    public static TimeManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; Debug.Log("<color=yellow>pool</color> инициализирован", gameObject);
        }
        else
        {
            gameObject.SetActive(false); Debug.Log("<color=red>ERROR</color> нельзя использовать <color=yellow>Pool</color> скрипт больше одного раза на сцене", gameObject);
        }
    }
    //-------------------------------------------------------------
    #endregion

    [Range(0, 3)] public float timeSpeed = 1f;
    [Range(0, 3)] public float timeSlowedSpeed = 0.5f;

    [SerializeField] AudioMixerSnapshot normal;
    [SerializeField] AudioMixerSnapshot timeslow;
    [SerializeField] AudioMixerSnapshot paused;

    void Start()
    {
        //EventManager.Instance.OnSlowTime += SlowTime;
        //EventManager.Instance.OnNormalizeTime += NormalizeTime;
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        paused.TransitionTo(1f);
    }
    public void UN_PauseGame()
    {
        Time.timeScale = timeSpeed;
        normal.TransitionTo(1f);
    }
    public void SlowTime()
    {
        Time.timeScale = timeSlowedSpeed;
        timeslow.TransitionTo(2f);
    }

    void NormalizeTime()
    {
        Time.timeScale = timeSpeed;
        normal.TransitionTo(2f);
    }
}

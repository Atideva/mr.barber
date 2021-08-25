
using TMPro;
using UnityEngine;

public class Game_level : MonoBehaviour
{
    #region Singleton
    //-------------------------------------------------------------
    public static Game_level Instance;
    void Awake()
    {
        FakeAwake();

        if (Instance == null) Instance = this;
        else gameObject.SetActive(false);
    }
    //-------------------------------------------------------------
    #endregion

        [Header("TEST")]
        public int FAKE_TEST_LEVEL;
    [Header("Settings")]
    public TextMeshProUGUI txt;
    public GameObject canvas_lvl;
    public GameObject canvas_complete;
    public GameObject canvas_fail;
    public AudioEvent victorySound;
    public string lvlName = "Level";
    string lvlKey = "Levels_complete_";
    public int level;
    void FakeAwake()
    {
        if (FAKE_TEST_LEVEL != 0) PlayerPrefs.SetInt(lvlKey, FAKE_TEST_LEVEL);
        level = PlayerPrefs.GetInt(lvlKey) ;
        txt.text = lvlName + " " + (level+1).ToString();
    }

    void Start()
    {
        Events_Main.Instance.On_PlayButton_Pressed += LevelStarted;
        Events_Main.Instance.On_LevelComplete += LevelComplete;
        Events_Barber.Instance.On_BarberBusted += LevelFail;

    }
    bool lvlFail;
    void LevelFail()
    {
        if (lvlFail|| lvlComplete) return;
        lvlFail = true; 

        canvas_lvl.SetActive(true);
        canvas_fail.SetActive(true);
    }

    bool lvlComplete;
    void LevelComplete()
    {
        if (lvlFail || lvlComplete) return;
        lvlComplete = true;

        if (victorySound) AudioManager.Instance.PlaySimpleEvent(victorySound);

        canvas_lvl.SetActive(true);
        canvas_complete.SetActive(true);
        level++;
        PlayerPrefs.SetInt(lvlKey, level);
    }
    public float hideLvlTxtDelay = 1f;
    void LevelStarted()
    {
        Invoke(nameof(DisableCanvas), hideLvlTxtDelay);
    }
    void DisableCanvas()
    {
        canvas_lvl.SetActive(false);
    }
}

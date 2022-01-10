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
    public GameObject canvas_menu;
    public GameObject canvas_complete;
    public GameObject canvas_fail;
    public GameObject canvas_ingameStats;
    public GameObject hilghitGoalImage;
    public AudioEvent victorySound;
    public string lvlName = "Level";

    string lvlKey = "Levels_complete_";
    public int level;

    void FakeAwake()
    {
        if (FAKE_TEST_LEVEL != 0) PlayerPrefs.SetInt(lvlKey, FAKE_TEST_LEVEL);
        level = PlayerPrefs.GetInt(lvlKey);
        txt.text = (level + 1).ToString();
    }

    void Start()
    {
        canvas_menu.SetActive(true);
        canvas_fail.SetActive(false);
        canvas_complete.SetActive(false);
        EventManager.Instance.On_PlayButton_Pressed += LevelStarted;
        EventManager.Instance.On_LevelComplete += LevelComplete;
        Events_Barber.Instance.On_BarberBusted += LevelFail;
        DisableHighlight();
    }

    bool lvlFail;

    void LevelFail()
    {
        if (lvlFail || lvlComplete) return;
        lvlFail = true;

      //  canvas_menu.SetActive(true);
        canvas_fail.SetActive(true);
        Invoke(nameof(DisableHighlight),1);
    }

    bool lvlComplete;

    void LevelComplete()
    {
        if (lvlFail || lvlComplete) return;
        lvlComplete = true;

        if (victorySound) AudioManager.Instance.PlaySimpleEvent(victorySound);

        // canvas_menu.SetActive(true);
         canvas_complete.SetActive(true);
        level++;
        PlayerPrefs.SetInt(lvlKey, level);
        Invoke(nameof(DisableHighlight),1);
    }


    public float hideLvlTxtDelay = 1f;

    void LevelStarted()
    {
        canvas_ingameStats.SetActive(true);
        canvas_menu.SetActive(false);
        Invoke(nameof(DisableCanvas), hideLvlTxtDelay);
        
    }

    void DisableCanvas()
    {
        hilghitGoalImage.SetActive(true);
    }
    void DisableHighlight()
    {
        hilghitGoalImage.SetActive(false);
    }
}
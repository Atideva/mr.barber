using TMPro;
using UnityEngine;

public class Monkey_UI_Counter : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public int count;
    public GameObject monkeyUI;
    void Start()
    {
        Events_Police.Instance.On_MonkeyThrowBanana += BananaTrhow;
    }

    void BananaTrhow()
    {
        count--;
        Setup(count);
    }

    void OnDisable()
    {
        Events_Police.Instance.On_MonkeyThrowBanana -= BananaTrhow;
    }

    public void DisableMonkeyUI()
    {
        monkeyUI.SetActive(false);
    }
    public void Setup(int _count)
    {
        monkeyUI.SetActive(true);
        txt.text = _count.ToString();
    }
}
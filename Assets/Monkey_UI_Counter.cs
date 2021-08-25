 
using TMPro;
using UnityEngine;

public class Monkey_UI_Counter : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public int count;

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
 



    public void Setup(int _count)
    {
        txt.text = _count.ToString();
    }
}

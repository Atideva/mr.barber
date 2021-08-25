using TMPro;
using UnityEngine;

public class Police_UI_counter : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public int count;
 
    void Start()
    {
        Events_Police.Instance.On_PoliceFlee += PoliceFlee;
    }
    void OnDisable()
    {
        Events_Police.Instance.On_PoliceFlee -= PoliceFlee;
    }
    void PoliceFlee(Police obj)
    {
        count--;
        Setup(count);
    }

 

    public void Setup(int _count)
    {
        txt.text = _count.ToString();
    }
}

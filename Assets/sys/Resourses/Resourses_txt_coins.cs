using System.Collections;
using TMPro;
using UnityEngine;

public class Resourses_txt_coins : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public string  this_key = "Barber_coins_total";
 
    float freq=0.1f;
    void Start() => StartCoroutine(refreshTxt());

    IEnumerator refreshTxt()
    {
        while (true)
        {
            int coins = PlayerPrefs.GetInt(this_key);
            txt.text = coins.ToString();
            yield return new WaitForSeconds(freq);
        }
    }
  
}

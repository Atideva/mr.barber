using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrivacyPolicy_accept : MonoBehaviour
{

    public LoadLevel_using_SM loadLevel;
    public Transform shipImage;
    public TextMeshProUGUI txt;
    public TextFlashing txtFlash;
    public float time;
    public float dist;
    float y;
    float x;
    public Color clr;
    string key = "PrivacyPolicyAccepted";
 
    public void Accepted()
    {
        x = shipImage.localPosition.x;
        y = shipImage.localPosition.y;
        StartCoroutine(moveShip());
        //txt.enabled = false;
        txtFlash.enabled = false;
        txt.color = clr;

        PlayerPrefs.SetInt(key, 1);
    }

    IEnumerator moveShip()
    {
        float speed = dist / time;

        while (time > 0)
        {
            time -= Time.deltaTime;
            x += speed * Time.deltaTime;
            shipImage.localPosition = new Vector2(x, y);
            yield return null;
        }

        loadLevel.Load();
    }

}

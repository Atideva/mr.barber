using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFlashing : MonoBehaviour
{

    public TextMeshProUGUI txt;
    public float flashTime;
    Color clr;
    void Start()
    {
        clr = txt.color;
        n = -1;
        a = 1;
    }

    float a;
    float n;

    void Update()
    {
        if (a <= 0) n = 1;
        if (a >= 1) n = -1;

        a += n * Time.deltaTime / flashTime;
        clr.a = a;
        txt.color = clr;
    }

}

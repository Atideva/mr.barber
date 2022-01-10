 
using UnityEngine;
using UnityEngine.UI;

public class VibrateToogle_button : MonoBehaviour
{
    public Image frame;
    public Image img;
    bool toogle;
    string key = "VibroAllowed";
    string firstTime = "firstTime";
    void Start()
    {
        bool first = !PlayerPrefs.HasKey(firstTime + key);
        if (first) PlayerPrefs.SetInt(firstTime + key, 1);
        toogle = first || PlayerPrefs.GetInt(key) == 1;
        Vibro(toogle);
    }
    public void Toggle()
    {
        toogle = !toogle;
        Vibro(toogle);
    }
    bool dontAtStart=true;
    void Vibro(bool act)
    {
        AndroidSettings.Instance.vibrate = act;
        PlayerPrefs.SetInt(key, act ? 1 : 0);
        ChangeFrame(toogle);
        if (act)
        {
            if (dontAtStart)
            {
                dontAtStart = false;
                return;
            }
            AndroidSettings.Instance.VibrateCreate(Vector2.zero, false, false);
        }
    }
    void ChangeFrame(bool act)
    {
        //float widht = act ? 200 : 100;
        //float height = frame.rectTransform.rect.height;
        //frame.rectTransform.sizeDelta = new Vector2(widht, height);

        //ColorToogle(frame, act);
        ColorToogle(img, act);

    }
    void ColorToogle(Image img, bool act)
    {
        Color clr = img.color;
        clr.a = act ? 1 : 0.35f;
        img.color = clr;
    }
}

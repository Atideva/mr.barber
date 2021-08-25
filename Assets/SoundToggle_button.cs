using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundToggle_button : MonoBehaviour
{
    public Image frame;
    public Image img;
    public enum SoundType : int
    {
        SFX = 0,
        Music = 1
    }
    string soundKey()
    {
        string key;
        switch (soundType)
        {
            case SoundType.SFX: key = "VolumeSFX"; break;
            case SoundType.Music: key = "VolumeMusic"; break;
            default: key = ""; break;
        }
        return key;
    }
    public SoundType soundType;
    string key;
    public AudioMixerGroup mixer;
    string firstTime = "firstTime";
    void Start()
    {
        key = soundKey();
        bool first = !PlayerPrefs.HasKey(firstTime + key);
        if (first) PlayerPrefs.SetInt(firstTime + key, 1);
        toogle = first || PlayerPrefs.GetFloat(key) == 1;
        ToggleVolume(toogle);
    }

    bool toogle;
    public void Toggle()
    {
        toogle = !toogle;
        ToggleVolume(toogle);
    }
    void ToggleVolume(bool act)
    {
        mixer.audioMixer.SetFloat(key, act ? 0 : -80f);
        PlayerPrefs.SetFloat(key, act ? 1 : 0);
        ChangeFrame(act);
    }
    void ChangeFrame(bool act)
    {
        float widht = act ? 200 : 100;
        float height = frame.rectTransform.rect.height;
        frame.rectTransform.sizeDelta = new Vector2(widht, height);

        ColorToogle(frame, act);
        ColorToogle(img, act);
 
    }
    void ColorToogle(Image img,bool act)
    {
        Color clr = img.color;
        clr.a = act ? 1 : 0.5f;
        img.color = clr;
    }
}

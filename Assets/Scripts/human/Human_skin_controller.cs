using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Human_skin_controller : MonoBehaviour
{
    public bool mixGender;
    [Header("Style")]
    public Sprite hairStyle;
    public Sprite mouthStyle;

    [Header("Settings")]
    public SpriteRenderer hair;
    public SpriteRenderer mouth;
    public Data_hairs_human hairData;
    public Data_mouths_human mouthData;
    public ParticleSystem cutVX;
    [HideInInspector] public Color myNewColor;
    public List<Hair_pile> piles = new List<Hair_pile>();
    void Start()
    {
        int male= Random.Range(0, 100);
        if (male<70)
        {
            int r = Random.Range(0, hairData.hairs.Count);
            hairStyle = hairData.hairs[r];
            int a = Random.Range(0, mouthData.mouth.Count);
            mouthStyle = mouthData.mouth[a];
        }
        else
        {
            int r = Random.Range(0, hairData.hairs_womans.Count);
            hairStyle = hairData.hairs_womans[r];
            int a = Random.Range(0, mouthData.mouth_womans.Count);
            mouthStyle = mouthData.mouth_womans[a];
        }


        hair.sprite = hairStyle;
        mouth.sprite = mouthStyle;


        int rc = Random.Range(0, 100);
        myNewColor  = rc < 45 ? brown : rc < 85 ? Color.black : Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        hair.color = myNewColor;
        var main = cutVX.main;
        main.startColor = myNewColor;
        myNewColor.a = 0.70f;
        foreach (var item in piles) if(item)item.SetColor(myNewColor);

        string hairName = hairStyle.name;
        string namePrefix = "";
        if (hairName.Length >= 5) namePrefix = hairName.Substring(hairName.Length - 5);
        if (namePrefix == prefix)
        {
            hair.color = Color.white;
            hair.sortingOrder = 5;
        }
        else
        {
            hair.sortingOrder = (Random.value>0.5f) ? 0 : 5;
        }
    }
    public Color brown;
    string prefix = "color";


#if UNITY_EDITOR
    float t;
    bool change;
    void Update()
    {

        if (Application.isEditor && !Application.isPlaying)
        {

            bool male = (Random.value > 0.5f);
            if (male)
            {
                int r = Random.Range(0, hairData.hairs.Count);
                hairStyle = hairData.hairs[r];
                int a = Random.Range(0, mouthData.mouth.Count);
                mouthStyle = mouthData.mouth[a];
            }
            else
            {
                int r = Random.Range(0, hairData.hairs_womans.Count);
                hairStyle = hairData.hairs_womans[r];
                int a = Random.Range(0, mouthData.mouth_womans.Count);
                mouthStyle = mouthData.mouth_womans[a];
            }

            int rc = Random.Range(0, 100);
            Color calor = rc < 45 ? brown : rc < 85 ? Color.black : Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            hair.color = calor;
            var main = cutVX.main;
            main.startColor = calor;

            hair.sprite = hairStyle;
            mouth.sprite = mouthStyle;

            string hairName = hairStyle.name;
            string namePrefix = "";
            if (hairName.Length >= 5)  namePrefix = hairName.Substring(hairName.Length - 5);
            if (namePrefix == prefix)
            {
                hair.color = Color.white;
                hair.sortingOrder = 5;
            }
            else
            {
                t += 1f;
                if (t >= 1)
                {
                    t = 0;
                    change = !change;
                    hair.sortingOrder = change ? 0 : 5;
                }
            }
        }
    }
#endif
}

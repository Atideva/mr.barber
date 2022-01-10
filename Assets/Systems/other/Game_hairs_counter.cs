using TMPro;
using UnityEngine;

public class Game_hairs_counter : MonoBehaviour
{

    public TextMeshProUGUI txt;
    public int hairMin;
    public int hairPerLvl;
    int hairNEED, hairCLIPPED=0;
    void Start()
    {
        hairNEED = hairMin + Game_level.Instance.level * hairPerLvl;

        int policeHairs = Spawn.Instance.PoliceOnly_Count;
        if (policeHairs != 0) hairNEED = policeHairs;

        Events_Barber.Instance.On_HairClipped += HairClipped;
        RefreshTxt();
    }
 
   
    void HairClipped(Vector2 obj, bool isPolice, bool PoliceHair)
    {
        if (isPolice && !PoliceHair) return;
        if (isPolice && !Spawn.Instance.PoliceOnly) return;

        hairCLIPPED++;
        RefreshTxt();
        if (hairCLIPPED >= hairNEED)
        {
            txt.color = Color.green;
            EventManager.Instance.LevelComplete();
            Events_Barber.Instance.On_HairClipped -= HairClipped;
        }
    }

    void RefreshTxt()
    {
        txt.text = hairCLIPPED + "/" + hairNEED;
    }
}

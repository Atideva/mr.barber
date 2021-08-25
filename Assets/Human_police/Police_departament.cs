using System.Collections.Generic;
using UnityEngine;

public class Police_departament : MonoBehaviour
{
    Dictionary<Police, int> activPolice = new Dictionary<Police, int>();

    void Start()
    {
        Events_Police.Instance.On_CrimeCommited += CrimeCommited;
        Events_Police.Instance.On_PoliceFlee += PoliceFlee;
    }

    void PoliceFlee(Police police)
    {
        if (activPolice.ContainsKey(police))
        {
            activPolice.Remove(police);
        }
        if (activPolice.Count == 0) Events_Police.Instance.No_On_Duties();
    }

    void CrimeCommited(Police police, int wanted_LVL)
    {
        if (activPolice.Count == 0) Events_Police.Instance.At_Duty();

        if (activPolice.ContainsKey(police))
        {
            activPolice[police] = wanted_LVL;
        }
        else
        {
            activPolice.Add(police, wanted_LVL);
        }
    }


}

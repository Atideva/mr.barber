using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mouth human (list)", menuName = "Data/Mouth human (list)")]
public class Data_mouths_human : ScriptableObject
{
    public List<Sprite> mouth = new List<Sprite>();
    public List<Sprite> mouth_womans = new List<Sprite>();
}

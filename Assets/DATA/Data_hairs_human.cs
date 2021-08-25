using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public struct Dicestats
//{
//    [Header("Level")]
//    public float damage;
//    public float fireRate;
//}

[CreateAssetMenu(fileName = "Hairs human (list)", menuName = "Data/Hairs human (list)")]
public class Data_hairs_human : ScriptableObject
{
    public List<Sprite> hairs = new List<Sprite>();
    public List<Sprite> hairs_womans = new List<Sprite>();




}

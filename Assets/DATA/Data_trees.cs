using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trees (list)", menuName = "Data/Trees (list)")]
public class Data_trees : ScriptableObject
{
    public List<Sprite> trees = new List<Sprite>();
}

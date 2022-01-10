using UnityEngine;

public class Spawn : MonoBehaviour
{
    #region Singleton
    //-------------------------------------------------------------
    public static Spawn Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else gameObject.SetActive(false);
    }
    //-------------------------------------------------------------
    #endregion

    [Header("PoliceLevelOnly")]
    public int policeOnlyLevelEvery = 5;
    public int policeOnlyHairNeedPerLvl = 1;
    [Header("SpawnMonkeyTrees")]
    public int monkeyTreesEvery = 3;
    int lvl => Game_level.Instance.level + 1;

    public bool PoliceOnly => lvl < policeOnlyLevelEvery ? false : lvl % policeOnlyLevelEvery == 0;
    public int PoliceOnly_Count => !PoliceOnly ? 0 : (lvl / policeOnlyLevelEvery) * policeOnlyHairNeedPerLvl;
    public bool MonkeyTree => lvl < monkeyTreesEvery ? false : lvl % monkeyTreesEvery == 0;
    public int MonkeyTree_Count => !MonkeyTree ? 0 : (lvl / monkeyTreesEvery);

}

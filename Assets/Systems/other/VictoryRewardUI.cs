using TMPro;
using UnityEngine;

public class VictoryRewardUI : MonoBehaviour
{

    public TextMeshProUGUI txt;
    void Start()
    {
        EventManager.Instance.On_LevelReward += LevelReward;
    }

    void LevelReward(int gems)
    {
        txt.text = gems.ToString();
    }
}

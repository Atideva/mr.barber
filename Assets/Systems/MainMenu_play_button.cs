using UnityEngine;

public class MainMenu_play_button : MonoBehaviour
{

    public void OnClick()
    {
        EventManager.Instance.PlayButton_Pressed();
    }


}

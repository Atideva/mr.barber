using UnityEngine;

public class MainMenu_play_button : MonoBehaviour
{

    public void OnClick()
    {
        Events_Main.Instance.PlayButton_Pressed();
    }


}

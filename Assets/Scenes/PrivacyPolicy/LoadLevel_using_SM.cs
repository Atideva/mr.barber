using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel_using_SM : MonoBehaviour
{

    public string level;
    public void Load()
    {
        SceneManager.LoadScene(level);
    }

}

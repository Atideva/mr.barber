using UnityEngine;
using UnityEngine.SceneManagement;

public class PrivacyPolicyLoad : MonoBehaviour
{
 
    public string level = "PrivacyPolicy";
    string key = "PrivacyPolicyAccepted";

    void Start()
    {
        if (!PlayerPrefs.HasKey(key)) Load();
    }

    void Load()
    {
        SceneManager.LoadScene(level);
    }
}

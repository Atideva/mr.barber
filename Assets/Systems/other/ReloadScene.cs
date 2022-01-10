using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public float delay;
    private bool onceTime;
    public void Reload ()
    {
        if (onceTime) return;
        onceTime = true;
        Invoke(nameof(ReloadInvoked),delay);
    }

    void ReloadInvoked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

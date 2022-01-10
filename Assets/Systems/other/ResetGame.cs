using UnityEngine;

[ExecuteInEditMode]
public class ResetGame : MonoBehaviour
{
    public bool resetCheck1;
    public bool resetCheck2;
    public bool resetCheck3;
    void Update()
    {
        if(resetCheck1 && resetCheck2 && resetCheck3)
        {
            PlayerPrefs.DeleteAll();
            Debug.LogError("RESET");
            resetCheck1 = false;
            resetCheck2 = false;
            resetCheck3 = false;
        }
    }
}

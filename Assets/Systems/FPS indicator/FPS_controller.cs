using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPS_controller : MonoBehaviour
{
    float frequency = 1.0f;
    string fps;
    public float fpsGoodEnough = 24;
    public bool showFpsTxt;
    public GameObject txtCanvas;
    public TextMeshProUGUI txt;
    public List<ParticleSystem> particlesToDisable = new List<ParticleSystem>();
    public List<GameObject> objectsToDisable = new List<GameObject>();
    public List<GameObject> objectsToEnable = new List<GameObject>();
    My_GameManager GM;

    void Start()
    {
        if (!Application.isEditor)
        {
            txtCanvas.SetActive(false);
            return;
        }

        GM = My_GameManager.Instance;
        if (showFpsTxt) txtCanvas.SetActive(true);
        else txtCanvas.SetActive(false);
        StartCoroutine(FPS());
        Invoke("enableChecker", 5f);
    }
    bool checkForFps;
    void enableChecker()
    {
        checkForFps = true;
    }
    IEnumerator FPS()
    {
        for (; ; )
        {
            // Capture frame-per-second
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;

            // Display it
            float realFps = Mathf.RoundToInt(frameCount / timeSpan);

            if (showFpsTxt)
            {
                fps = string.Format("fps: {0}", realFps);
                txt.text = fps;
            }

            if (checkForFps)
            {
                if (realFps < fpsGoodEnough && isActive) particlesChangeState(false);
                if (realFps > fpsGoodEnough && !isActive) particlesChangeState(true);
            }
        }
    }
    bool isActive=true;
    void particlesChangeState(bool act)
    {
        if (GM) GM.FpsDropedLow = !act;
        isActive = act;

        foreach (var item in particlesToDisable)
        {
            if (item)
            {
                if (item.gameObject.activeSelf)
                {
                    var emision = item.emission;
                    emision.enabled = act;
                }
            }
        }

        foreach (var item in objectsToDisable)
        {
            if (item)
                item.SetActive(act);
        }

        foreach (var item in objectsToEnable)
        {
            if (item)
                item.SetActive(!act);
        }
    }
}

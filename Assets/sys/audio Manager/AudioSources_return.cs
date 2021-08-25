using System.Collections;
using UnityEngine;

public class AudioSources_return : MonoBehaviour
{
    AudioSource src;
    AudioSources_Pool pool;
    void Start()
    {
        src = GetComponent<AudioSource>();
        pool = AudioSources_Pool.Instance;
    }
    void OnEnable() => StartCoroutine(Kostil());
    IEnumerator Kostil()
    {
        yield return new WaitForEndOfFrame();
        AudioClip clp = src.clip;
        float delay;
        if (clp == null)
            delay = 1f;
        else
            delay = src.clip.length / src.pitch;
        yield return new WaitForSeconds(delay * Time.timeScale);
        gameObject.SetActive(false);
        pool.ReturnToPool(src);
    }
 
}

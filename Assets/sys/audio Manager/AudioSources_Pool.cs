using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSources_Pool : MonoBehaviour
{
    #region Init
    [SerializeField] AudioMixerGroup sfxMixerGroup;
    void Start()
    {
        //InitPreloaded();
#if (UNITY_EDITOR)
        StartEd();
#endif
    }

    //-------------------------------------------------------------
    [SerializeField] GameObject prefab = null;
    //-----------------------------------------------------------------------------------------------------
    public Queue<AudioSource> my_Queue = new Queue<AudioSource>();
    public Dictionary<AudioSource, Transform> my_transforms = new Dictionary<AudioSource, Transform>();
    public Dictionary<AudioSource, AudioSources_return> my_scripts = new Dictionary<AudioSource, AudioSources_return>();
    //-----------------------------------------------------------------------------------------------------
    public static AudioSources_Pool Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; Debug.Log("<color=yellow>AUDIO SOURCES pool</color> инициализирован", gameObject);
        }
        else
        {
            Debug.Log("<color=red>ERROR</color> нельзя использовать <color=yellow>gun_bullet_Pool</color> скрипт больше одного раза на сцене", gameObject);
            Destroy(gameObject);
            //gameObject.SetActive(false); 
        }

        //Transform par = transform.parent;
        //if (par) transform.parent = null;
        //DontDestroyOnLoad(gameObject);
    }
    //-------------------------------------------------------------
    #if (UNITY_EDITOR)
    int n = 0;
    string s;
    void StartEd() => s = name;
    #endif
    //-------------------------------------------------------------
    #endregion

    public void ReturnToPool(AudioSource src)
    {
        my_Queue.Enqueue(src);
        src.gameObject.SetActive(false);
    }

    public AudioSource GetSource()
    {
        if (my_Queue.Count > 0)
        {
            AudioSource src = my_Queue.Dequeue();
            if (!src) return null;
            src.gameObject.SetActive(true);
            return src;
        }
        else
            return CreateSource();
    }
    AudioSource CreateSource()
    {
        if (!gameObject) return null;
        if (!gameObject.activeSelf) return null;

        GameObject src = Instantiate(prefab);         //create new

        AudioSource source = src.GetComponent<AudioSource>();
        source.outputAudioMixerGroup = sfxMixerGroup;

        var t = src.transform;
        my_transforms.Add(source, t);    //add to dictionary for cash transform

        var script = src.GetComponent<AudioSources_return>();
        my_scripts.Add(source, script);


        #region EditorShiet
#if (UNITY_EDITOR)
        //-------------------------------------------------------------
        // make bul child of pool gameobject, for editor "clean" vision
        src.transform.parent = transform;

        //Write number of pooled object to the name of Pool (n).
        n++;
        gameObject.name = s + " (" + n.ToString() + ")";
        //-------------------------------------------------------------
#endif
        #endregion

        return source;
    }

    void InitPreloaded()
    {
        int i = 0;
        foreach (var src in gameObject.GetComponentsInChildren<AudioSources_return>())

        {
            var source = src.gameObject.GetComponent<AudioSource>();
            source.outputAudioMixerGroup = sfxMixerGroup;

            //add to dictionary for cashs
            my_transforms.Add(source, src.transform);  
            my_scripts.Add(source, src.GetComponent<AudioSources_return>());

#if (UNITY_EDITOR)
            //Write number of pooled object to the name of Pool (n).
            i++;
            Debug.Log("IM PRELOADED  " + i, src);
            n++;
            gameObject.name = s + " (" + n.ToString() + ")";
#endif
            ReturnToPool(source);
        }
    }
}

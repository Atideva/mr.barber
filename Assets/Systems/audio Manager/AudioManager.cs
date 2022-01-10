using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Instance
    private static AudioManager instance;
    public static AudioManager Instance;

    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = FindObjectOfType<AudioManager>();
    //            if (instance == null)
    //            {
    //                instance = new GameObject("Spawned AudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
    //                Debug.Log("soso");
    //            }
    //        }

    //        return instance;
    //    }
    //    set
    //    {
    //        instance = value;
    //    }
    //}
    #endregion

    #region Fields
   float hearDistance=50f;
    public float HearDistance() => hearDistance;
    [SerializeField] AudioMixerGroup musicMixerGroup;
    [SerializeField] AudioMixerGroup sfxMixerGroup;

     AudioSource musicSource;
     AudioSource musicSource2;
     AudioSource sfxSource;
    // Multiple musics
     bool firstMusicSourceIsActive;
    [SerializeField] float sameSFXfreq=0.1f;
  public  List<AudioClip> frequencyList = new List<AudioClip>();
    void OnLevelWasLoaded(int level)
    {
        if (pool == null)
            pool = AudioSources_Pool.Instance;
    }
    //void On() => pool = AudioSources_Pool.Instance;
    AudioSources_Pool pool;
    [SerializeField] bool moveSourceToSoundPosition;

    #endregion
    void Start()
    {
        if (pool == null)
            pool = AudioSources_Pool.Instance;
    }
    void Awake()
    {
        Transform par = transform.parent;
        if (par) transform.parent = null;
        DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource2 = gameObject.AddComponent<AudioSource>();
       //  sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        musicSource2.loop = true;

        musicSource.outputAudioMixerGroup = musicMixerGroup;
        musicSource2.outputAudioMixerGroup = musicMixerGroup;
       //  sfxSource.outputAudioMixerGroup = sfxMixerGroup;

        if (pool == null)
            pool = AudioSources_Pool.Instance;

        if (Instance == null)
        {
            Instance = this;
            Debug.Log("<color=yellow>AM</color> инициализирован", gameObject);
        }
        else
        {
            Debug.Log("<color=red>ERROR</color> нельзя использовать <color=yellow>AM</color> скрипт больше одного раза на сцене", gameObject);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip musicClip,float vol)
    {
        musicSource.clip = musicClip;
        musicSource.volume = vol;
        musicSource.Play();
    }
    public void PlayMusicWithFade(AudioClip musicClip, float vol,float transitionTime )
    {
        // Determine which source is active
        AudioSource activeSource = (firstMusicSourceIsActive) ? musicSource : musicSource2;

        StartCoroutine(UpdateMusicWithFade(activeSource, musicClip, vol, transitionTime));
    }
    public void PlayMusicWithCrossFade(AudioClip musicClip, float vol,float transitionTime  )
    {
        // Determine which source is active
        AudioSource activeSource = (firstMusicSourceIsActive) ? musicSource : musicSource2;
        AudioSource newSource = (firstMusicSourceIsActive) ? musicSource2 : musicSource;

        // Swap the source
        firstMusicSourceIsActive = !firstMusicSourceIsActive;

        // Set the fields of the audio source, then start the coroutine to crossfade
        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, musicClip,vol, transitionTime));
    }

    public void StopMusic() => StartCoroutine(StopMusicFade());
    IEnumerator StopMusicFade()
    {
        AudioSource activeSource = (firstMusicSourceIsActive) ? musicSource : musicSource2;
        float musicVolume = activeSource.volume;
        float t= musicVolume;
        // Fade out
        while (t > 0)
        {
            t -= Time.deltaTime * 1f;
            activeSource.volume = t * musicVolume;
            yield return null;
        }
        activeSource.Stop();

    }

    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip music,float musicVolume, float transitionTime)
    {
        // Make sure the source is active and playing
        if (activeSource.isPlaying)
        {
            float musicVolumeCurent = activeSource.volume;
            float a = musicVolumeCurent;
            while (a > 0)
            {
                a -= Time.deltaTime;
                activeSource.volume = a * musicVolumeCurent;
                yield return null;
            }
            activeSource.Stop();
        }
 
        // Gotta restart after changing the clip
        activeSource.clip = music;
        activeSource.Play();
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / transitionTime; 
            if (t > 1) t = 1f;
            activeSource.volume = musicVolume * t;
           yield return null;
        }
 
    }
    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, AudioClip music,float musicVolume, float transitionTime)
    {
        // Make sure the source is active and playing
        if (!original.isPlaying)
            original.Play();

        newSource.Stop();
        newSource.clip = music;
        newSource.Play();

        float t = 0.0f;

        for (t = 0.0f; t <= transitionTime; t += Time.deltaTime)
        {
            original.volume = (musicVolume - ((t / transitionTime) * musicVolume));
            newSource.volume = (t / transitionTime) * musicVolume;
            yield return null;
        }

        // Make sure we don't end up with a weird float value
        original.volume = 0;
        newSource.volume = musicVolume;

        original.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (ClipIsBlocked(clip)) return;
        sfxSource = pool.GetSource();
        sfxSource.clip = clip;
        sfxSource.Play();
    }
    public void PlaySFX(AudioClip clip, float volume)
    {
        if (ClipIsBlocked(clip)) return;
        sfxSource = pool.GetSource();
        sfxSource.clip = clip;
        sfxSource.volume = volume;
        sfxSource.Play();
    }
    public void PlaySimpleEvent(AudioEvent even)
    {
        even.Play(pool.GetSource());
    }
    public void PlaySimpleEvent(AudioEvent even,Vector2 from,Vector2 to)
    {
        even.Play(pool.GetSource(), from,to, hearDistance);
    }
 
    public void PlaySimpleEvent(AudioEvent even,float delay)
    {
        StartCoroutine(SimpleEventDelayed(even, delay));
    }
    IEnumerator SimpleEventDelayed(AudioEvent even,float delay)
    {
        yield return new WaitForSeconds(delay);
        even.Play(pool.GetSource());
    }
    public bool ClipIsBlocked(AudioClip clip)
    {
        if (frequencyList.Contains(clip))
            return true;
        else
        {
            StartCoroutine(FreqListJob(clip,sameSFXfreq));
            return false;
        }
    }
    public bool ClipIsBlocked(AudioClip clip,float sfxFreq)
    {
        if (frequencyList.Contains(clip))
            return true;
        else
        {
            //if (sameSFXfreq != 0)
            //    StartCoroutine(FreqListJob(clip, sameSFXfreq));
            //else
                StartCoroutine(FreqListJob(clip, sfxFreq));
            return false;
        }
    }
    IEnumerator FreqListJob(AudioClip _clip,float freq)
    {
        frequencyList.Add(_clip);
        yield return new WaitForSeconds(freq);
        frequencyList.Remove(_clip);
    }

    //public void SetMusicVolume(float volume)
    //{
    //    musicVolume = musicSource.volume = volume;
    //    musicSource2.volume = volume;
    //}
    //public void SetSFXVolume(float volume)
    //{
    //    sfxSource.volume = volume;
    //}
}
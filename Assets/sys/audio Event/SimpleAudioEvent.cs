using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Audio Events/Audio")]
public class SimpleAudioEvent : AudioEvent
{

    public AudioClip[] clips;
    public RangedFloat volume;
    [MinMaxRange(0, 3)]
    public RangedFloat pitch;
    [SerializeField] float sfxFreq = 0f;
    AudioSources_Pool pool;
 

    int i;
    public override void Play(AudioSource source)
    {
        if (SoundBlocked()) return;
        PlaySound(source, 1f);
    }
    public override void Play(AudioSource source,Vector2 from,Vector2 to,float hearDistance)
    {
        if (SoundBlocked()) return;

        if ((from - to).sqrMagnitude <= hearDistance * hearDistance)
        {
            float vol = 1f; // THERE SOUHLD BE some params for calculate 3D VOLUME from DIStANCE
            PlaySound(source, vol);
        }
    }
    public override void PlayOneShot(AudioSource source)
    {
        if (SoundBlocked()) return;
        PlaySoundOneShot(source, 1f);
    }
    bool SoundBlocked()
    {
        if (clips.Length == 0) return true;
        i = Random.Range(0, clips.Length);
        if (AudioManager.Instance.ClipIsBlocked(clips[i], sfxFreq))
            return true;
        else
            return false;
    }
    void PlaySound(AudioSource source,float volumed)
    {
        source.volume = Random.Range(volume.minValue, volume.maxValue);
        source.volume *= volumed;
        source.pitch = Random.Range(pitch.minValue, pitch.maxValue);
        source.clip = clips[i];
        source.Play();
    }
    void PlaySoundOneShot(AudioSource source, float volumed)
    {
        source.volume = Random.Range(volume.minValue, volume.maxValue);
        source.volume *= volumed;
        source.pitch = Random.Range(pitch.minValue, pitch.maxValue);
        source.PlayOneShot(clips[i]);
    }
    public override void Test(AudioSource source)
    {
        int i = Random.Range(0, clips.Length);

        source.volume = Random.Range(volume.minValue, volume.maxValue);
        source.pitch = Random.Range(pitch.minValue, pitch.maxValue);
        source.PlayOneShot(clips[i]);
    }
 
}
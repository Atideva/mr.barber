using UnityEngine;
public abstract class AudioEvent : ScriptableObject
{
    public abstract void Play(AudioSource source);
    public abstract void Play(AudioSource source,Vector2 from,Vector2 to,float hearDIstance);
    public abstract void PlayOneShot(AudioSource source);
    public abstract void Test(AudioSource source);
}
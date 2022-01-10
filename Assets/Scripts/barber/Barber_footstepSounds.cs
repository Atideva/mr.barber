using UnityEngine;

public class Barber_footstepSounds : MonoBehaviour
{
    readonly float rate = 0.6f;
    public AudioSource src;
    public iMotor2D_human motor;

    void Start() => EventManager.Instance.On_PlayButton_Pressed += Play;
    void Play() => src.gameObject.SetActive(true);
    void FixedUpdate() => src.pitch = motor.currentSpeed * rate;
}

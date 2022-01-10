using UnityEngine;

public class Barber_MOBILE_MOVE : MonoBehaviour
{
    public Barber barber;
    joysticks joy;
    void Start() => joy = joysticks.Instance;

#if UNITY_STANDALONE || UNITY_EDITOR
#else
    void Update()
    {
        if (joy.LeftJoystick_Is_Working)
            barber.MoveDirection(joy.LeftJoystick_Direction);
    }
#endif


    //void ArcadeDifficuly(float spd, float block, bool noReload)
    //{
    //    Speed *= (1 + spd);
    //    motor.SetMoveSpeed(Speed);
    //}
    //void HelpMe(float spd, float block)
    //{
    //    float newSpeed = Speed * (1 + spd);
    //    motor.SetMoveSpeed(newSpeed);
    //}
}

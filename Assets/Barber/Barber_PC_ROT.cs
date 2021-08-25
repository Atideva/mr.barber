using System.Collections.Generic;
using UnityEngine;

public class Barber_PC_ROT : MonoBehaviour
{
    [SerializeField] List<Transform> RotatePartsRightJoystick;
    joysticks joy;
    void Start()
    {
        joy = joysticks.Instance;
    }
    public float MyAngle;
    [SerializeField] Camera cam;
    void FixedUpdate()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        //>>>-----------Rotor-----------<<<    
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = (mousePos - (Vector2)transform.position).normalized;
        MyAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
     //   foreach (var t in RotatePartsRightJoystick) t.up = lookDir;
#else
        if (!joy) return;
        if (joy.LeftJoystick_Is_Working)
        {
            foreach (var t in RotatePartsRightJoystick)
                t.up = joy.LeftJoystick_Direction;
        }
#endif

    }
}

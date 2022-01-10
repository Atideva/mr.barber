using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human_foot_colider : MonoBehaviour
{
    public Transform graphics;
    public iMotor2D_human motor;
    public AudioEvent slipped;
    public AudioEvent smashed;
    public AudioEvent monkeyLaugh;
    public GameObject cotnrollers;
 
    public float slipTime = 1f;
    public float fallTime = 0.3f;
    bool slip;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (slip) return;
        if (collision.CompareTag("BananaPeel")) Slipped();
    }

    void Slipped()
    {
        if (slipped) AudioManager.Instance.PlaySimpleEvent(slipped);
        StartCoroutine(SlippAnim());
        StartCoroutine(EngineBroken());
    }

    IEnumerator EngineBroken()
    {
        slip = true;
        cotnrollers.SetActive(false);
        motor.ENGINE_BROKEN = true;
        yield return new WaitForSeconds(slipTime);
        motor.ENGINE_BROKEN = false;
        cotnrollers.SetActive(true);
        graphics.localEulerAngles = new Vector3(0, 0, 0);
        slip = false;
    }


    IEnumerator SlippAnim()
    {
        bool curve = true;
        bool kacheli = true;
        float timer = 0;
        float angleTotal = 90;
        Vector2 dir = motor.MoveDirection;
        float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float rightDir = (-90 < ang && ang <= 90) ? -1 : 1;
        float angleStep = angleTotal / fallTime;
        float angle = 0;
        float curveAngle = 0;
        float t = 0.5f;
        float i = 6;

        graphics.localEulerAngles = new Vector3(0, 0, angle);
        while (timer <= fallTime)
        {
            timer += Time.deltaTime;

            angle += rightDir * (angleStep * Time.deltaTime);

            float p = Mathf.Abs(angle / angleTotal);

            if (kacheli)
                curveAngle += rightDir * (angleStep * Time.deltaTime) * (1 + (t - p) * ((i - 1) / (t - 1)));
            else
                curveAngle += rightDir * (angleStep * Time.deltaTime) * p * 2;



            if (curve)
                graphics.localEulerAngles = new Vector3(0, 0, curveAngle);
            else
                graphics.localEulerAngles = new Vector3(0, 0, angle);
            yield return null;
        }
        angle = rightDir * 90;
        graphics.localEulerAngles = new Vector3(0, 0, angle);

        if (smashed) AudioManager.Instance.PlaySimpleEvent(smashed);
        yield return new WaitForSeconds(0.1f);
        if (monkeyLaugh) AudioManager.Instance.PlaySimpleEvent(monkeyLaugh);


    }


}

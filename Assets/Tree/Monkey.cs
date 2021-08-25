using System;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    public Transform from;
    public float activateDistance;
    public float bananaFlyTime = 1f;
    Transform barber;
    public Banana banan;
    My_GameManager manager;
    public TransformKacheli tailKach;
    void Start()
    {
        chill = true;
        manager = My_GameManager.Instance;
        barber = manager.barber.transform;
        Events_Police.Instance.On_At_Duty += ActiavteMonkey;
        Events_Police.Instance.On_No_On_Duties += ChillMonkey;
    }

    void ChillMonkey()
    {
        banan.RotateBanana(false);
        chill = true;
    }

    void ActiavteMonkey()
    {
        banan.RotateBanana(true);
        chill = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(from.position, activateDistance);
    }
    bool throwed, chill;
    void FixedUpdate()
    {
        if (chill) return;
        if (throwed) return;
        Vector2 target = barber.position;
        float dist = Vector2.Distance(from.position, target);
        if (dist <= activateDistance)
        {
            throwed = true;
            Events_Police.Instance.On_At_Duty -= ActiavteMonkey;
            Events_Police.Instance.On_No_On_Duties -= ChillMonkey;
            Events_Police.Instance.MonkeyThrowBanana();

            float barberSpeed = manager.BarberCurrentSpeed;
            //if (barberSpeed < 2) barberSpeed = 2;
            Vector2 offset = barberSpeed * manager.BarberMoveDirection * bananaFlyTime/3f;
            Vector2 predict = target + offset+Vector2.down;
            float newDist = Vector2.Distance(predict, banan.transform.position);
            float speed = newDist / bananaFlyTime;
            banan.TrhowBanana(predict, speed);
     

            Invoke("Tail", 1f);
        }
    }
    void Tail()
    {
        tailKach.enabled = false;
            tailKach.transform.localEulerAngles = new Vector3(0, 0, 0);
            tailKach.enabled = true;

        enabled = false;
    }

}

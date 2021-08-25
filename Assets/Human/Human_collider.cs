using UnityEngine;

public class Human_collider : MonoBehaviour
{
    public Human human;
    bool clipped;
    bool iamPolice;
    int policeLifes;
    bool supsect;
 
    public void CrimeSuspected(bool arg) => supsect = arg;
    void Start()
    {
        iamPolice = human.iamPolice;
        policeLifes = human.policeLifes;
        Events_Barber.Instance.On_BarberBusted += DisableCollider;
        Events_Main.Instance.On_LevelComplete += DisableCollider;
    }

    void DisableCollider() => enabled = false;


    bool busted;
    void OnTriggerEnter2D(Collider2D collision) => Trigger(collision);
    void OnTriggerStay2D(Collider2D collision) => Trigger(collision);

    void Trigger(Collider2D collision)
    {
        if (collision.CompareTag(Tags.HairClipper))
        {
            if (clipped) return;
            human.Clipper();
            clipped = true;

            if (iamPolice && policeLifes > 1)
                Invoke(nameof(IAmReady), 1f);
        }

        if (!clipped && iamPolice && supsect && policeLifes > 0)
        {
            if (collision.CompareTag(Tags.Baber))
            {
                if (!busted)
                {
                    busted = true;
                    Events_Barber.Instance.BarberBusted();
                }
            }
        }
    }
    void IAmReady()
    {
        clipped = false;
        if (human.policeFlee) policeLifes--;
    }

}

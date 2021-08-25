using UnityEngine;

public class Barber_clipper_sound : MonoBehaviour
{

    public AudioEvent hairCut;
    public AudioEvent comment;
    public float commnetDelay = 1f;
    public int commnetChanche = 100;
    void Start()
    {
        Events_Barber.Instance.On_HairClipped += Clipped;
    }

    void Clipped(Vector2 pos, bool isPolice, bool PoliceHair)
    {
        if (hairCut) AudioManager.Instance.PlaySimpleEvent(hairCut);

        int chance = Random.Range(0, 101);
        if (chance < commnetChanche)
            Invoke("Comment", commnetDelay);
    }

    void Comment()
    {
        if (comment) AudioManager.Instance.PlaySimpleEvent(comment);
    }
}

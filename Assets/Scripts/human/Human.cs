using System.Collections;
using UnityEngine;

public class Human : MonoBehaviour
{
    [Header("Move")]
    public float movSpeed;
    [Range(1, 5)] public float shockSpdMultiplier = 3;
    public ParticleSystem footVX;
    public GameObject cloudVX;
    public iMotor2D_human engine;
    public Human_clip_me clip;
    public Human_drop_coins dropCoins;
    public Human_Anim_shocked shockAnim;
    public Human_Anim_foots animFoots;
    public Human_Anim_hands animHands;
    public AudioEvent shockSound;
    [Header("This is police")]
    public bool iamPolice;
    public bool policeFlee;
    public int policeLifes = 3;
    public Human_behaviour behaviour;
    public Police_crime_commited police_job;
    public Police_anim police_anim;
    public Police police;
    public Color police_footStepClr;
    [Header("ShockTime")]
    public float shockTime = 0.5f;
    public float shockTimeDecrease = 0.1f;
    public float shockTimeMin = 0.1f;


    void Start()
    {
        engine.SetMoveSpeed(movSpeed);
        if (iamPolice)
        {
            var main = footVX.main;
            main.startColor = police_footStepClr;
            clip.PoliceHideHair();
        }
        EventManager.Instance.On_LevelComplete += StopAllHumans;
    }

    void StopAllHumans()
    {
        StartCoroutine(Stop());
    }

    IEnumerator Stop()
    {
        while (true)
        {
            engine.StopInstant();
            yield return null;
        }
    }
    public void MoveDirection(Vector2 dir)
    {
        if (iStopp) return;
        engine.SetMoveDirection(dir);
    }
    public void StopMe(bool stop, float speedINC)
    {
        iStopp = stop;
        if (iStopp) engine.StopInstant();
        else engine.SetMoveSpeed(movSpeed * speedINC);
    }
    public void Clipper()
    {
        clip.ClipMe(iamPolice);
        if (!iamPolice) dropCoins.DropCoins();
        if (iamPolice)
        {
            policeLifes--;
            if (!policeFlee && policeLifes == 0) policeLifes = 1;
            if (policeLifes == 0)
            {
                police_job.JobEnd();
                police_anim.DropItems();
                behaviour.FollowTarget(null);
                Events_Police.Instance.PoliceFlee(police);
            }
        }

        StartCoroutine(ImShocked());
    }

    bool iStopp;

    IEnumerator ImShocked()
    {
        StopMe(true, 0);
        float wait = shockTime;
        shockTime -= shockTimeDecrease;
        if (shockTime <= shockTimeMin) shockTime = shockTimeMin;
        yield return new WaitForSeconds(wait);
        shockAnim.Shock(iamPolice, policeLifes);
        animHands.Shock();
        //yield return new WaitForSeconds(0.1f);
        if (shockSound && (!iamPolice || policeFlee || policeLifes != 1)) AudioManager.Instance.PlaySimpleEvent(shockSound);
        //yield return new WaitForSeconds(stopTime);
        animFoots.Shock();
        StopMe(false, shockSpdMultiplier);
        FootVX();
    }

    void FootVX()
    {
        var main = footVX.main;
        main.simulationSpeed *= shockSpdMultiplier;
        //footVX.gameObject.SetActive(false);
        var emi = footVX.emission;
        emi.enabled = false;
        cloudVX.SetActive(true);
    }
}

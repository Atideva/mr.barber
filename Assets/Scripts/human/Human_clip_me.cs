using System.Collections.Generic;
using UnityEngine;

public class Human_clip_me : MonoBehaviour
{
    public SpriteRenderer myHair;
    public float cutDelay = 0.3f;
    public GameObject cutVX;
    bool hairCliped;
    int policeStage;
    public bool spawnPiles;
    public GameObject pilePrefab;
    public int pileMax;
    public Transform headPos;
    public Transform bodyCollider;
    [Header("Police settings")]
    public SpriteRenderer hat;
    public SpriteRenderer pants;
    public GameObject pantsVX;
    public Human human;
    public List<GameObject> piles = new List<GameObject>();

    public void PoliceHideHair() => myHair.enabled = false;

    public void ClipMe(bool iamPolice)
    {
        if (!hairCliped)
        {
            hairCliped = true;

            if (!iamPolice) Invoke(nameof(Cut), cutDelay);
            else PoliceStageCut();

            if (!iamPolice || (human.policeFlee && policeStage == 3)) Events_Barber.Instance.HumanFlee(bodyCollider);
            var hairOff = policeStage == 2;
            Events_Barber.Instance.HairClipped(transform.position, iamPolice, hairOff);
        }
    }

    void Cut()
    {
        myHair.sprite = null;
        cutVX.SetActive(true);
        if (spawnPiles) EnablePiles();
    }

    void EnablePiles()
    {
        var r = Random.Range(piles.Count / 2, piles.Count);
        for (var i = 0; i < r; i++)
        {
            if (piles[i]) piles[i].SetActive(true);
            piles[i].transform.parent = null;
            piles[i].transform.position = PosOffset(headPos.position);
        }
    }

    Vector2 PosOffset(Vector2 pos)
    {
        var o = 1.5f;
        var x = Random.Range(-o, o);
        var y = Random.Range(-o, o);
        var offset = new Vector2(x, y);
        return pos + offset;
    }

    public void PoliceStageCut()
    {
        policeStage++;
        if (policeStage == 1)
            Invoke(nameof(PoliceStage1), cutDelay);
        else
        {
            if (policeStage == 2)
                Invoke(nameof(PoliceStage2), cutDelay);
            else
                Invoke(nameof(PoliceStage3), cutDelay);
        }
    }

    void PoliceStage1()
    {
        hairCliped = false;
        hat.enabled = false;
        myHair.enabled = true;
    }

    void PoliceStage2()
    {
        hairCliped = false;
        Cut();
    }

    void PoliceStage3()
    {
        if (!human.policeFlee) return;
        hairCliped = false;
        pants.enabled = false;
        if (pantsVX) pantsVX.SetActive(true);
    }
}
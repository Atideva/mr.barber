 using UnityEngine;

public class Police_crime_commited : MonoBehaviour
{
    public Police police;
    public float commitedRange = 3f;
    public float rangePerLvl_PLUS = 0.2f;
    int lvl;
    public float policeOnlyRangeMultiplier = 0.2f;
    public void JobEnd() => Events_Barber.Instance.On_HairClipped -= CrimeCheck;
    public bool starWarning;
    public SpriteRenderer star;
    public Color starWarningColor;
    Color starNormalColor;
    Transform barber;
    void Start()
    {
        Events_Barber.Instance.On_HairClipped += CrimeCheck;
        lvl = Game_level.Instance.level;
        commitedRange += lvl * rangePerLvl_PLUS;
        if (Spawn.Instance.PoliceOnly) commitedRange *= (1 + policeOnlyRangeMultiplier);
        if (starWarning)
        {
            barber = My_GameManager.Instance.barber.transform;
            starNormalColor = star.color;
        }
    }
    Color clrLAST;
    void FixedUpdate()
    {
        if (!starWarning) return;
        if (wanted) return;
        float dist = Vector2.Distance(transform.position, barber.position);

        Color clrNEW = dist <= commitedRange ? starWarningColor : starNormalColor;
        if (clrLAST != clrNEW)
        {
            clrLAST = clrNEW;
            star.color = clrNEW;
        }
    }
    void CrimeCheck(Vector2 pos, bool isPolice, bool PoliceHair)
    {
        float dist = Vector2.Distance(transform.position, pos);
        if (dist <= commitedRange)
        {
            wanted = true;
            police.CrimeCommited();
        }
    }
    bool wanted;
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, commitedRange);
    }
}

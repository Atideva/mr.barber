using UnityEngine;

public class Human_Anim_shocked : MonoBehaviour
{
    public SpriteRenderer eyeLeft;
    public SpriteRenderer eyeRight;
    public SpriteRenderer mouth;

    public GameObject star;
    public Sprite eyeShocked;
    public Sprite mouthShocked;
    Sprite normalMouth,normalEye;
    [Header("Close mouth")]
    public bool closeMouth;
    public float closeMouthTime = 3f;
    public bool hideMouth;
   
    void Start()
    {
        normalMouth = mouth.sprite;
        normalEye = eyeLeft.sprite;
        
    }
    public void Shock(bool iamPolice, int policeLifes)
    {
        if (!iamPolice) star.SetActive(false);
        if (iamPolice && policeLifes == 0) star.SetActive(false);

        mouth.sprite = mouthShocked;
        if (!iamPolice) eyeLeft.sprite = eyeShocked;
        if (!iamPolice) eyeRight.sprite = eyeShocked;
        if (closeMouth) Invoke("CloseMouth", closeMouthTime);
    }
    public void PoliceEye()
    {
        eyeLeft.sprite = eyeShocked;
        eyeRight.sprite = eyeShocked;
    }
    void CloseMouth()
    {
        mouth.sprite = hideMouth ? null : normalMouth;
    }
}

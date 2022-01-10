using UnityEngine;

public class Spawn_police : MonoBehaviour
{
    public int countMin;
    public GameObject prefab;
    float widht, height;
    public float policePerLvl;
    public Police_UI_counter policeUI;

    void Start()
    {
        widht = World_size.Instance.width;
        height = World_size.Instance.height;
        Invoke(nameof(DebugInstantiate), 0.05f);
    }
    void DebugInstantiate()
    {
        int lvl = Game_level.Instance.level;
        int r = countMin + (int)(lvl * policePerLvl);

        if (Spawn.Instance.PoliceOnly)
            r = Spawn.Instance.PoliceOnly_Count;

        policeUI.Setup(r);
        for (int i = 0; i < r; i++)
        {
            GameObject ojb = Instantiate(prefab, RandomPos(), Quaternion.identity);
            if (Spawn.Instance.PoliceOnly) ojb.GetComponent<Human>().policeFlee = true;
        }

    }

    Vector2 RandomPos()
    {
        return new Vector2(Random.Range(-widht, widht), Random.Range(-height, height));
    }
}

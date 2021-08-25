using UnityEngine;

public class Spawn_humans : MonoBehaviour
{
    public int countMin;
    public int countMax;
    public GameObject human;
    float widht, height;
    public int incPerLevel;

    void Start()
    {
        widht = World_size.Instance.width;
        height = World_size.Instance.height;

        Invoke("DebugInstantiate", 0.05f);
    }
    void DebugInstantiate()
    {
        int lvl = Game_level.Instance.level;

        // POLICE ONLY LEVELS
        if (Spawn.Instance.PoliceOnly) return;

        int a = Random.Range(countMin, countMax + 1);
        int r = a + lvl * incPerLevel;

        for (int i = 0; i < r; i++)
        {
            Instantiate(human, RandomPos(), Quaternion.identity);
        }

    }

    Vector2 RandomPos()
    {
        return new Vector2(Random.Range(-widht, widht), Random.Range(-height, height));
    }

}

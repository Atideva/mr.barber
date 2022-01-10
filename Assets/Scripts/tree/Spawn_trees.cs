using UnityEngine;

public class Spawn_trees : MonoBehaviour
{
    public int treesMin;
    public int incPerLevel;
    public GameObject prefab;
    float widht, height;
    public Monkey_UI_Counter monkeysUI;

    void Start()
    {
        int monkeyLvl = Spawn.Instance.MonkeyTree_Count;
        
        if (monkeyLvl == 0)
        {
            monkeysUI.DisableMonkeyUI();
            return;
        }

        int r = treesMin + monkeyLvl * incPerLevel;
        widht = World_size.Instance.width;
        height = World_size.Instance.height;

        monkeysUI.Setup(r);
 
        for (int i = 0; i < r; i++)
        {
            Instantiate(prefab, RandomPos(), Quaternion.identity);
        }
    }


    Vector2 RandomPos()
    {
        float fix = 0.7f;
        return new Vector2(Random.Range(-widht* fix, widht* fix), Random.Range(-height* fix, height* fix));
    }
}

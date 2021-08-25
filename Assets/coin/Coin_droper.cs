using System.Collections;
using UnityEngine;

public class Coin_droper : MonoBehaviour
{
    public float dropRange;
    public Object_pool pool;
    void Start()
    {
        Events_Resourses.Instance.On_CoinDrop += CreateCoins;
    }

    void CreateCoins(Vector2 pos, int count)
    {
        StartCoroutine(create(pos, count));
    }
    IEnumerator create(Vector2 pos, int count)
    {
        int c = 0;
        while(c< count)
        {
            c++;
            Vector2 createPos = RandomPos(pos);
            pool.Create(createPos, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);

        }
    }
   Vector2 RandomPos(Vector2 pos)
    {
        Vector2 rand = new Vector2(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange));
        Vector2 res = pos + rand;
        return res;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Object_pool : pool
{
    #region Init

    public GameObject prefab = null;
    pool myPool;
    void Awake() => myPool = GetComponent<pool>();


    //-----------------------------------------------------------------------------------------------------
    public Queue<GameObject> queue = new Queue<GameObject>();
    public Dictionary<GameObject, Transform> trans = new Dictionary<GameObject, Transform>();
    //-----------------------------------------------------------------------------------------------------


    #region EditorShiet
#if (UNITY_EDITOR)
    int n = 0;
    string s;
#endif
    void Start()
    {
#if (UNITY_EDITOR)
        s = name;
#endif
    }
    #endregion
    //-------------------------------------------------------------
    #endregion

    public override void ReturnToPool(GameObject obj)
    {
        queue.Enqueue(obj);
        obj.SetActive(false);
    }

    public override GameObject Create(Vector2 pos, Quaternion rot)
    {
        if (queue.Count > 0)
        {
            GameObject obj = queue.Dequeue();
            trans[obj].position = pos;
            obj.SetActive(true);

            return obj;
        }
        else
        {
            GameObject enemy = Instantiate(prefab, pos, rot);         //create new bullet

            var ret = enemy.GetComponent<Object_return>();
            ret.SetPool(myPool);

            var t = enemy.transform;
            trans.Add(enemy, t);    //add to dictionary for cash transform


            #region EditorShiet
#if (UNITY_EDITOR)
            //-------------------------------------------------------------
            // make bul child of pool gameobject, for editor "clean" vision
            enemy.transform.parent = transform;

            //Write number of pooled object to the name of Pool (n).
            n++;
            gameObject.name = s + " (" + n.ToString() + ")";
            //-------------------------------------------------------------
#endif
            #endregion

            return enemy;
        }
    }

}

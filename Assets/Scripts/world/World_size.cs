 
using UnityEngine;

[ExecuteInEditMode]
public class World_size : MonoBehaviour
{

    #region Singleton
    //-------------------------------------------------------------
    public static World_size Instance;
    void Awake()
    {
        FakeAwake();
        if (Instance == null) Instance = this;
        else gameObject.SetActive(false);
    }
    //-------------------------------------------------------------
    #endregion

    public float widthDefault;
    public float heightDefault;
    public float width;
    public float height;
    public float incPerLvl = 0.02f;
    string lvlKey = "Levels_complete_";
    public GameObject frameDebugPrefab;
    Transform frame;

    void FakeAwake()
    {
        int level = PlayerPrefs.GetInt(lvlKey);
        width = widthDefault * (1 + incPerLvl * level);
        height = heightDefault * (1 + incPerLvl * level);
    }

    void Start()
    {
#if UNITY_EDITOR
        var oldLines = GameObject.FindGameObjectsWithTag("DebugFrame");
        foreach (var item in oldLines) DestroyImmediate(item);
#endif
        frame = Instantiate(frameDebugPrefab).transform;
        frame.position = Vector2.zero;
        frame.parent = transform;
    }


#if UNITY_EDITOR
    void Update()
    {
        frame.localScale = new Vector2(width, height);
    }
#endif

    void OnDrawGizmos()
    {
        Gizmos.color=Color.green;
        Vector2 horizontal = new Vector2(width, 0);
        Vector2 vertical = new Vector2(0, height);
        Vector2 center = (Vector2)transform.position;
        Vector2 one = center - horizontal + vertical;
        Vector2 two = center + horizontal + vertical;
        Vector2 three = center + horizontal - vertical;
        Vector2 four = center - horizontal - vertical;

        Gizmos.DrawLine(one, two);
        Gizmos.DrawLine(two, three);
        Gizmos.DrawLine(three, four);
        Gizmos.DrawLine(four, one);
    }
}

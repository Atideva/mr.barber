
using UnityEngine;

public class Tree : MonoBehaviour
{
    public Data_trees data;
    public SpriteRenderer spriteRend;
    public float xMin, xMax, yMin, yMax;
    public float multMin,multMax;
    void Start()
    {
        int r = Random.Range(0, data.trees.Count);
        spriteRend.sprite = data.trees[r];
        float x = Random.Range(xMin, xMax);
        float y = Random.Range(yMin, yMax);
        float mult = Random.Range(multMin, multMax);
 
        x *= mult;
        y *= mult;
        transform.localScale = new Vector2(x, y);
    }
}
    

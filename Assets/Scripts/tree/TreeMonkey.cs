 
using UnityEngine;

public class TreeMonkey : MonoBehaviour
{
    public SpriteRenderer tree;
    public bool flipX;
 
    void Start()
    {
        if (flipX) tree.flipX = Random.value > 0.5f ? true : false;
    }

  
}

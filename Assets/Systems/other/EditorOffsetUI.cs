using UnityEngine;

public class EditorOffsetUI : MonoBehaviour
{
    
    void Start()
    {
        var rect = GetComponent<RectTransform>();
        rect.localPosition = Vector3.zero;
    }

    
}

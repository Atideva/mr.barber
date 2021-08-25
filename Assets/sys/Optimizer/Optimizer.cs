using System.Collections.Generic;
using UnityEngine;

public class Optimizer : MonoBehaviour
{
    public Transform t;
    My_GameManager gm;
    public List<GameObject> objects = new List<GameObject>();
    public List<ParticleSystem> particles = new List<ParticleSystem>();
    void Awake()
    {
        if (!t) t = transform;
        t.localPosition = Vector2.zero;
    }

    void Start() => gm = My_GameManager.Instance;
    bool enabler = true;
    void FixedUpdate()
    {
        if (gm.amIoutSideOfcamera(t.position))
        {
            if (enabler) OjbectEnable(false);
        }
        else
        {
            if (!enabler) OjbectEnable(true);
        }
    }
    void OjbectEnable(bool act)
    {
        enabler = act;
        foreach (var item in objects)
        {
            if (item) item.SetActive(act);
        }  
        foreach (var item in particles)
        {
            if (item)
            {
                var emission = item.emission;
                emission.enabled = act;
            }
        }
    }
}
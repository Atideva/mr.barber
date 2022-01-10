using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barber_coll_autoShave : MonoBehaviour
{
    public Barber_clipper_autoShave autoShave;
    readonly string _tag = "ClipperObject";
    public float shaveTime = 0.7f;

    public List<Transform> targets = new List<Transform>();
    public List<Transform> shaved = new List<Transform>();
    void Start() => Events_Barber.Instance.On_HumanFlee += HumanFlee;
    void HumanFlee(Transform flee)
    {
        shaved.Add(flee);
        StartCoroutine(letMeFinishMyJob(flee));
    }
    IEnumerator letMeFinishMyJob(Transform target)
    {
        yield return new WaitForSeconds(shaveTime);
        RemoveCheck(target);
    }

    void OnTriggerEnter2D(Collider2D collision) => Triger(collision, true);
    void OnTriggerStay2D(Collider2D collision) => Triger(collision, true);
    void OnTriggerExit2D(Collider2D collision) => Triger(collision, false);


    void Triger(Collider2D collision, bool enter)
    {
        if (shaved.Contains(collision.transform)) return;
        if (collision.CompareTag(_tag))
        {
            if (enter)
                AddCheck(collision.transform);
            else
                RemoveCheck(collision.transform);
        }
    }

    void FixedUpdate()
    {
        if (targets.Count == 0) ResetClosest();
        else
        {
            foreach (var item in targets)
            {
                float dist = Vector2.Distance(transform.position, item.position);
                if (dist < closestDist)
                {
                    Closest(item, dist);
                }
            }
        }
    }

    float closestDist;
    Transform targetClosest;
    void AddCheck(Transform check)
    {
        if (!targets.Contains(check)) targets.Add(check);
    }
    void RemoveCheck(Transform check)
    {
        if (targets.Contains(check))
        {
            targets.Remove(check);
            if (check == targetClosest) ResetClosest();
        }
    }
    void ResetClosest() => Closest(null, 1000);
    void Closest(Transform targ, float dist)
    {
        targetClosest = targ;
        closestDist = dist;
        autoShave.SetTarget(targ);
    }


}

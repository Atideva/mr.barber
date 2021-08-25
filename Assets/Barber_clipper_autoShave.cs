using UnityEngine;

public class Barber_clipper_autoShave : MonoBehaviour
{
    public Transform handClipper;
    public Transform target;
    public GameObject pcRot;
    public Transform handIn;
    void Start()
    {
        pcRot.SetActive(false);
    }
    public void SetTarget(Transform _target)
    {
        target = _target;
        float z = _target == null ? 90 : 0;
        handIn.localEulerAngles = new Vector3(0, 0, z);
        if (z == 90) handClipper.localEulerAngles = new Vector3(0, 0, -90);
    }

    public float ang;
    void FixedUpdate()
    {
        if (target == null) return;

        Vector2 dir = target.position - transform.position;
        handClipper.up = dir;
    }


#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (target == null) return;
        Gizmos.DrawLine(target.position, transform.position);
    }
#endif
}

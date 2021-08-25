using UnityEngine;

public class TransformRotation : MonoBehaviour
{
    Transform t;
    float x, y, z;
    [SerializeField] float speed;
    float curSpeed;
    public bool startRandomAngle;

    public void ChangeSpeed(float spdMltp) => curSpeed = speed * spdMltp;
    public void ResetSpeed() => curSpeed = speed;
    public void ChangeDirection() => dir *= -1f;
    float dir = 1f;

    void Awake()
    {
        t = transform;
        curSpeed = speed;
    }
    void OnEnable()
    {
        x = t.rotation.eulerAngles.x;
        y = t.rotation.eulerAngles.y;
        z = startRandomAngle ? Random.Range(0, 360) : t.rotation.eulerAngles.z;
        t.rotation = Quaternion.Euler(x, y, z);
    }
    void FixedUpdate()
    {
        z += dir * curSpeed * Time.fixedDeltaTime;
        t.rotation = Quaternion.Euler(x, y, z);
    }

}

using UnityEngine;

public class Human_behaviour : MonoBehaviour
{
    public Human human;
    float widht, height;
    Vector2 destanation;
    [Header("Police smart")]
    bool smartBehaviour;
    public int fromLvl=15;
    [SerializeField] bool refreshingOffset;
    float refreshTimer;
    [SerializeField] float refreshRate;
    [SerializeField] float minDist;

    void Start()
    {
        widht = World_size.Instance.width;
        height = World_size.Instance.height;
        int lvl = Game_level.Instance.level;
        smartBehaviour = lvl >= fromLvl;
    }

    public void FollowTarget(Transform _target)
    {
        followTarget = _target != null;
        target = _target;
        if (followTarget && smartBehaviour) RandomOffset();
    }
 
    //SMART BEHAVIOUR
    float xOffset, yOffset;
    void RandomOffset()
    {
        xOffset = Random.Range(2f, minDist);
        if (Random.value > 0.5f) xOffset *= -1f;

        yOffset = Random.Range(2f, minDist);
        if (Random.value > 0.5f) yOffset *= -1f;
    }
    Vector2 Outside_Target_Position => new Vector2(target.position.x + xOffset, target.position.y + yOffset);
    //SMART BEHAVIOUR
    
    Transform target;
    bool followTarget;
    void Update()
    {
        if (followTarget)
        {
            Vector2 dir;
            Vector2 move;
            float dist = Vector2.Distance(target.position, transform.position);
            move = smartBehaviour && (dist > minDist) ? Outside_Target_Position : (Vector2)target.position;
            dir = move - (Vector2)transform.position;
            human.MoveDirection(dir);

            if (refreshingOffset)
            {
                refreshTimer += Time.deltaTime;
                if (refreshTimer > refreshRate)
                {
                    refreshTimer = 0;
                    RandomOffset();
                }
            }
        }
        else
        {
            if (destanation == Vector2.zero)
            {
                destanation = RandomPos();
            }
            else
            {
                float dist = Vector2.Distance(destanation, transform.position);
                if (dist <= 1f) destanation = RandomPos();

                Vector2 dir = destanation - (Vector2)transform.position;
                human.MoveDirection(dir);
            }
        }
    }

    Vector2 RandomPos()
    {
        float fix = 0.9f;
        return new Vector2(Random.Range(-widht* fix, widht* fix),Random.Range(-height* fix, height* fix));
    }

}

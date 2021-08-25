using System;
using System.Collections.Generic;
using UnityEngine;

public class iMotor2D_human : MonoBehaviour, iMotor2D
{
    [SerializeField] float moveSpeed;

    [SerializeField] float timeToTakeMaxSpeed;
    public bool ENGINE_BROKEN;
    [SerializeField] bool AlwaysLookForward;
    [SerializeField] Transform AlwaysLookForwardTransform;
    [SerializeField] List<Transform> RotatePartsRightJoystick;

    public event Action OnEngineStart = delegate { };
    public event Action OnEngineStop = delegate { };

    Vector2 moveDirection;
    public Vector2 MoveDirection
    {
        get { return moveDirection; }
        protected set { }
    }

    public float currentSpeed = 0f;
    float timeStep;
    public Transform t;
    float moveSpeedBoost;
    bool IsMoving;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        protected set { }
    }

    Color JetColor;
    void Start()
    {
       if(!t) t = GetComponent<Transform>();
        if (JetVX) { JetRender = JetVX.GetComponent<SpriteRenderer>(); JetColor = JetRender.color; }
    }

    [SerializeField] GameObject JetVX;
    SpriteRenderer JetRender;

    float overHeatBoost = 0f;
    public void SetMoveSpeed(float speed) => moveSpeed = speed;
    public void SetMoveSpeedAddBoost(float value) => moveSpeedBoost += value;
    public void SetOverHeatBoost(float value) => overHeatBoost = value;
    public void SetMoveDirection(Vector2 direction)
    {
        IsMoving = true;
        direction.Normalize();
        moveDirection = direction;
    }
    public void LookThisWay(Vector2 lookDirection)
    {
        foreach (var t in RotatePartsRightJoystick)
            t.up = lookDirection;
    }
    public void StopMoving() => SetMoveDirection(Vector2.zero);

    public void StopInstant() => currentSpeed = 0;

    bool EngineIsWorking;
    void FixedUpdate()
    {
        float spd = moveSpeed * (1 + moveSpeedBoost + overHeatBoost);
        if (spd < 0f) spd = 0f;
        if (IsMoving)
        {
            IsMoving = false;
            if (timeToTakeMaxSpeed == 0)
                currentSpeed = spd;
            else
            {
                if (currentSpeed == 0)
                {
                    EngineIsWorking = true;
                    OnEngineStart();
                }
                currentSpeed += spd / timeToTakeMaxSpeed * Time.fixedDeltaTime;
            }
        }
        else
        {

            if (timeToTakeMaxSpeed == 0)
                currentSpeed = 0f;
            else
            {
                float broken = ENGINE_BROKEN ? 0.2f : 1f;
                currentSpeed -= spd * broken / timeToTakeMaxSpeed * Time.fixedDeltaTime;
                if (currentSpeed <= 5f && EngineIsWorking)
                {
                    EngineIsWorking = false;    //TO reduce ACTIONS PER FRAME CALLED !!!!!!!!!!!!!!!!!!!!!
                    OnEngineStop();
                }
            }

        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0f, spd);

        if (currentSpeed > 0)
            t.Translate(moveDirection * currentSpeed * Time.fixedDeltaTime, Space.World);

        if (AlwaysLookForward)
            AlwaysLookForwardTransform.up = moveDirection;

        //JET VX
        if (JetVX)
        {

            if (moveSpeedBoost >= 0.5f)
            {
                if (!JetActivated)
                {
                    JetVX.SetActive(true);
                    JetActivated = true;
                    jetAlpha = 0f;
                }
                else
                {
                    if (jetAlpha < 1f)
                    {
                        jetAlpha += Time.deltaTime;
                        JetColor.a = jetAlpha;
                        JetRender.color = JetColor;
                    }
                }
            }
            else
            {
                if (JetActivated)
                {
                    if (jetAlpha > 0f)
                    {
                        jetAlpha -= Time.deltaTime;
                        JetColor.a = jetAlpha;
                        JetRender.color = JetColor;
                    }
                    else
                    {
                        JetActivated = false;
                        JetVX.SetActive(false);
                    }
                }
            }
        }
    }

    bool JetActivated;
    float jetAlpha;
}

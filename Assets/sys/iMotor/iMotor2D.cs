using UnityEngine;

public interface iMotor2D
{
    void SetMoveSpeed(float speed);

    void SetMoveDirection(Vector2 direction);

    void LookThisWay(Vector2 lookDirection);

    void StopMoving();
}

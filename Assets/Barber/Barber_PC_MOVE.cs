using UnityEngine;

public class Barber_PC_MOVE : MonoBehaviour
{
#if UNITY_STANDALONE || UNITY_EDITOR
    public Barber barber;
    public Vector2 mov;
    public float ang;
    public float curRotSpeed = 300f;
    public iMotor2D_human engine;

    public bool SmoothRotation = true;
    void Update()
    {
        //>>>-----------Motor-----------<<<         
        mov.x = Input.GetAxisRaw("Horizontal");
        mov.y = Input.GetAxisRaw("Vertical");
        if (mov != Vector2.zero)
        {
            Vector2 newMovDir;
            if (SmoothRotation)
            {
                ang = Mathf.Atan2(mov.y, mov.x) * Mathf.Rad2Deg;

                if (ang < 0) ang += 360f;
                float angMIN = ang;
                float angMAX = ang + 180f;
                if (angMAX > 360f) angMAX -= 360f;

                Vector2 movDir = engine.MoveDirection;
                float z = Mathf.Atan2(movDir.y, movDir.x) * Mathf.Rad2Deg;
                float CLOCKWISE = 0;

                float check = z;
                if (z >= 360f) check -= 360f;
                if (z < 0f) check += 360f;

                if (ang <= 180f)
                    CLOCKWISE = (angMIN < check && check < angMAX) ? -1 : 1;
                else
                    CLOCKWISE = (angMAX < check && check < angMIN) ? 1 : -1;

                float aprox = Mathf.Abs(ang - check);
                float step = curRotSpeed * Time.deltaTime;
                if (step <= aprox)
                    z += CLOCKWISE * step;
                else
                    z = ang;

                float x = Mathf.Cos(z * Mathf.Deg2Rad);
                float y = Mathf.Sin(z * Mathf.Deg2Rad);
                newMovDir = new Vector2(x, y);
            }
            else
                newMovDir = mov;

            barber.MoveDirection(newMovDir);
        }
    }
#endif

}

using UnityEngine;

public class My_GameManager : MonoBehaviour
{
    #region Singleton
    public static My_GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("<color=yellow>Joystick</color> инициализирован", gameObject);
        }
        else
            Debug.Log("<color=red>ERROR</color> нельзя использовать <color=yellow>joysticks</color> скрипт больше одного раза на сцене", gameObject);
    }
    #endregion   

    public bool FpsDropedLow;
    public GameObject barber;
    public Camera PlayerCamera;

    [Header("Magnet")]
    public bool magnet;
    public float magnetSpeed;
    public float magnetDelay;
     iMotor2D_human barberEngine;

    Transform playerCameraTransform;
    float xMax, xMin, yMax, yMin;
    void Start()
    {
        playerCameraTransform = PlayerCamera.transform;
        barberEngine = barber.GetComponent<Barber>().engine;
    }

    public float BarberCurrentSpeed => barberEngine.currentSpeed;
    public Vector2 BarberMoveDirection => barberEngine.MoveDirection;

    public bool amIoutSideOfcamera(Vector2 myPos)
    {
        if (playerCameraTransform)
        {
            if (myPos.x > xMax || myPos.x < xMin || myPos.y > yMax || myPos.y < yMin)
                return true;
            else
                return false;
        }
        else
            return false;
    }
    void FixedUpdate()
    {
        if (playerCameraTransform && PlayerCamera)
        {
            Vector2 camPos = playerCameraTransform.position;
            Vector2 screenBounds = PlayerCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            ////cam screen width & height
            float camWidth = screenBounds.x - camPos.x;
            float camHeight = screenBounds.y - camPos.y;

            float xSize = camWidth * 1.2f;
            float ySize = camHeight * 1.2f;

            xMax = camPos.x + xSize;
            xMin = camPos.x - xSize;
            yMax = camPos.y + ySize;
            yMin = camPos.y - ySize;
        }
    }
}

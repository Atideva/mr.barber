using UnityEngine;
using System.Collections;
public class joysticks : MonoBehaviour
{
    #region Инициализация
    [Header("Джойстики")]
    public Transform joystick;
    public Transform joystick_center;
    public Transform joystick_2;
    public Transform joystick_center_2;
    [Header("Corners")]
    [Range(0,1)]  public float leftWidth;
    [Range(0, 1)] public float leftHeight;
    [Range(0, 1)] public float rightWidth;
    [Range(0, 1)] public float rightHeight;

    /* Ручная передача данных
     * может сэкономить ресурсы, передавая данные только тогда, когда джойстик активен. В противном случае скрипты вынуждены постояяно запрашивать инфу у джойстика.
    [Header("Скрипты управления")]
    public player_movement mov_script;
    public player_rotation rot_script;
    public player_shoot shoot_script;
    */
        
    int freeID = -1; //должен отличатся от всех возможных touch.ID, быть отрицательным или больше чем возможное количество касаний на экране телефона, к примеру 999
    int leftID, rightID;
    public int LeftID { get => leftID;  protected set { } }
    public int RightID { get => rightID;  protected set { } }



    [SerializeField] float size = 25; // Joystick size

    Vector2 leftLoc, rightLoc;
    Vector2 leftReset, rightReset;

    [HideInInspector] public Vector2 LeftJoystick_Direction, RightJoystick_Direction;
    [HideInInspector] public bool LeftJoystick_Is_Working, RightJoystick_Is_Working;

    #endregion
         
    #region Singleton
    public static joysticks Instance;

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

    void Start()
    {
        leftID = freeID;
        rightID = freeID;
        leftReset = joystick.position;
        rightReset = joystick_2.position;

#if UNITY_STANDALONE
        gameObject.SetActive(false);
#endif
    }

    #region Работа с тачами пальцев
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (leftID == freeID || rightID == freeID)
            {
                SearchNewJoystick();
            }
            if(leftID != freeID || rightID != freeID)
            {
                JoystickOperations();
            }
        }
    }
#endregion

    #region Активация нового джойстика
    //--------------------------new joystick-----------------------------------
    /// <summary>
    /// Поиск нового джойстика
    /// </summary>
    void SearchNewJoystick()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
           
            float x = t.position.x;
            float y = t.position.y;
            if (t.phase == TouchPhase.Began)
            {
                //Left joystick AREA
                if (x < Screen.width * leftWidth &&
                     y < Screen.height * leftHeight && joystick.gameObject.activeSelf)
                {
                    CreateNewJoystick(x, y, t.fingerId, true);
                }
                //Right joystick AREA
                else if (x > Screen.width * rightWidth &&
                         y < Screen.height * rightHeight && joystick_2.gameObject.activeSelf)
                {
                    CreateNewJoystick(x, y, t.fingerId, false);
                }
            }

            ++i;
        }
    }
    
    /// <summary>
    /// Создание нового джойстика
    /// </summary>
    void CreateNewJoystick(float x, float y, int fingerID, bool thisIsLeft)
    {
        if (thisIsLeft)
        {
            LeftJoystick_Is_Working = true;
            leftID = fingerID;
            leftLoc = new Vector2(x, y); //NEW TOUCH LOCATION
            joystick.position = new Vector2(x, y);
            joystick_center.position = new Vector2(x, y);           
        }
        else
        {
            RightJoystick_Is_Working = true;
            rightID = fingerID;
            rightLoc = new Vector2(x, y); //NEW TOUCH LOCATION
            joystick_2.position = new Vector2(x, y);
            joystick_center_2.position = new Vector2(x, y);           
        }

    }

#endregion

    #region Работа с уже активированным джойстиком
    //--------------------------Существует joystick-----------------------------------
    /// <summary>
    /// Операции с существующим джойстиком
    /// </summary>
    void JoystickOperations()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);

            //Джойстик работает, пальчик двигается
            if (t.phase == TouchPhase.Moved ||
                t.phase == TouchPhase.Stationary)
            {
                if (t.fingerId == leftID)
                {
                    if (joystick.gameObject.activeSelf)
                        CalculateForTouch(t, true);
                    else
                        ResetJoystick(true);
                }
                else if (t.fingerId == rightID)
                {
                    if (joystick_2.gameObject.activeSelf)
                        CalculateForTouch(t,false);
                    else
                        ResetJoystick(false);
                }
            }

            // Джойстик отменен, пальчик перестал тереть телефон
            else if (t.phase == TouchPhase.Ended ||
                     t.phase == TouchPhase.Canceled)
            {
                if (t.fingerId == leftID)
                {
                    ResetJoystick(true);
                }
                else if (t.fingerId == rightID)
                {
                    ResetJoystick(false);
                }
            }

            ++i;
        }
    }
       
    /// <summary>
    /// Сброс джойстиков
    /// </summary>
    void ResetJoystick(bool thisIsLeft)
    {
        float x, y;
        Transform j, jc;

        if (thisIsLeft)
        {
            x = leftReset.x;
            y = leftReset.y;
            
            j = joystick;
            jc = joystick_center;

            leftID = freeID;
            LeftJoystickSTOP();
        }
        else
        {
            x = rightReset.x;
            y = rightReset.y;
            
            j = joystick_2;
            jc = joystick_center_2;

            rightID = freeID;
            RightJoystickSTOP();

        }

        j.position = new Vector2(x, y);
        jc.position = new Vector2(x, y);


    }

    /// <summary>
    /// Расчет и передача данных
    /// </summary>
    void CalculateForTouch(Touch t, bool thisIsLeft)
    {
        Vector2 loc;
        Transform j;
        if (thisIsLeft)
        {
            loc = leftLoc;
            j = joystick_center;
        }
        else
        {
            loc = rightLoc;
            j = joystick_center_2;
        }

        Vector2 newLoc = t.position;
        Vector2 offset = newLoc - loc;
        offset = Vector2.ClampMagnitude(offset, size);
        Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);

        j.position = new Vector2(loc.x + offset.x, loc.y + offset.y);

        //Передача данных
        if (thisIsLeft)
        {
            LeftJoystickActions(direction);
        }
        else
        {
            RightJoystickActions(direction);
        }
    }

#endregion

    #region LEFT
    //----------------------------L E F T---------------------------------

    void LeftJoystickActions(Vector2 direction)
    {
      //  mov_script.Move(direction);
        LeftJoystick_Direction = direction;
    }
    void LeftJoystickSTOP()
    {
        //mov_script.MoveStop();
        LeftJoystick_Is_Working = false;
    }

#endregion
    
    #region RIGHT
    //----------------------------R I G H T---------------------------------

    void RightJoystickActions(Vector2 direction)
    {
        //rot_script.Rotate(direction);
       // shoot_script.Shoot();
        RightJoystick_Direction = direction;
    }    
    void RightJoystickSTOP()
    {
       // rot_script.RotateStop();
       // shoot_script.ShootStop();
        RightJoystick_Is_Working = false;
    }
    //-------------------------------------------------------------
#endregion
}

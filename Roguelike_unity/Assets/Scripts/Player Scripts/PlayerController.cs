using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerController : MonoBehaviour
{

    KeyCode Left = KeyCode.A;
    KeyCode Right = KeyCode.D;
    KeyCode Down = KeyCode.S;
    KeyCode Up = KeyCode.W;

    Transform Player;
    Vector2 movement;
    SpriteRenderer MySpr;
    Animator myAnim;
    PlayerStatus MyStatus;

    Vector2 mouseLast;

    public GameObject GamepadCH;
    public Transform AttackHandler;
    float PosUpdate = 20;
    Rigidbody2D MyRB;

    // Start is called before the first frame update
    void Start()
    {
        GameInfo.Player = this.transform;
        myAnim = GetComponent<Animator>();
        Player = this.transform;
        MySpr = GetComponent<SpriteRenderer>();
        GamepadCH.SetActive(false);
        MyStatus = GetComponent<PlayerStatus>();
        MyRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MyStatus.Alive)
        {
            GetInput();
            DoMovement();

            if (PosUpdate >= 0)
            {
                PosUpdate -= 60 * Time.deltaTime;
                if (PosUpdate < 0)
                {
                    GameInfo.PlayerPos = transform.position;
                    PosUpdate = 5;
                }
            }
        }
        else
        {
            movement = Vector2.zero;
            
        }
    }

    void DoMovement()
    {
         MyRB.velocity = (Vector3)Vector2.ClampMagnitude(movement, 3);

        if (Cursor.visible)
        {
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
                MySpr.flipX = false;
            else
                MySpr.flipX = true;
        }
        else
        {
            if (GamepadCH.transform.position.x > transform.position.x)
                MySpr.flipX = false;
            else
                MySpr.flipX = true;

        }

        if (movement != Vector2.zero)
            myAnim.SetBool("Running", true);
        else
            myAnim.SetBool("Running", false);
    }

    void GetInput()
    {
        CheckKeyboard();
        //CheckGamepad();
    }

    void CheckGamepad()
    {
        if (Gamepad.current.leftStick.ReadValue() != Vector2.zero)
        {
            movement = Gamepad.current.leftStick.ReadValue();
        }
        if (Gamepad.current.rightStick.ReadValue() != Vector2.zero)
        {

            AttackHandler.localEulerAngles = new Vector3(0f, 0, 90 + Mathf.Atan2(-Gamepad.current.rightStick.ReadValue().x, Gamepad.current.rightStick.ReadValue().y) * 180 / Mathf.PI);
            GamepadCH.transform.eulerAngles = Vector3.zero;
            if (Cursor.visible)
                Cursor.visible = false;
            if (!GamepadCH.activeSelf)
            {
                GamepadCH.SetActive(true);
            }
        }
        else if (!Cursor.visible && (Vector2)Input.mousePosition - mouseLast!=Vector2.zero)
        {
            Cursor.visible = true;
            GamepadCH.SetActive(false);
        }
        mouseLast = Input.mousePosition;
    }
    void CheckKeyboard()
    {
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            movement.x = -3;
        else if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            movement.x = 3;
        else
            movement.x = 0;

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            movement.y = 3;
        else if (!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
            movement.y = -3;
        else
            movement.y = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    Vector2 DirVec;
    GameObject scanObject;

    public GameManager manager;
    public float Speed;
    float h;
    float v;
    bool isHorizonMove;

    //Mobile Key Var
    int up_Value;
    int down_Value;
    int left_Value;
    int right_Value;
    bool up_down;
    bool down_down;
    bool left_down;
    bool right_down;
    bool up_up;
    bool down_up;
    bool left_up;
    bool right_up;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        //Move Value
        h = Input.GetAxisRaw("Horizontal") + right_Value + left_Value;
        v = Input.GetAxisRaw("Vertical") + up_Value + down_Value;

        //Button Down & Up Check
        bool hDown = Input.GetButtonDown("Horizontal") || right_down || left_down;
        bool vDown = Input.GetButtonDown("Vertical") || up_down || down_down;
        bool hUp = Input.GetButtonUp("Horizontal") || right_up || left_up;
        bool vUp = Input.GetButtonUp("Vertical") || up_up || down_up;

        //Check Horizontal Move
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0;

        //Animation
        if(anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("isChange", false);

        //Direction Vector
        if (vDown && v == 1)
            DirVec = Vector2.up;
        else if (vDown && v == -1)
            DirVec = Vector2.down;
        else if (hDown && h == -1)
            DirVec = Vector2.left;
        else if (hDown && h == 1)
            DirVec = Vector2.right;

        //Scan
        if (Input.GetButtonDown("Jump") && (scanObject != null))
            manager.Action(scanObject);


        //Mobile Var Init
        up_down = false;
        down_down = false;
        left_down = false;
        right_down = false;
        up_up = false;
        down_up = false;
        left_up = false;
        right_up = false;
    }
    private void FixedUpdate()
    {
        //isAction Freeze
        Speed = manager.isAction ? 0 : 10;

        //Move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        scanObject = collision.gameObject;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        scanObject = null;
    }

    public void ButtonDown(string type)
    {
        switch(type)
        {
            case "U":
                up_Value = 1;
                up_down = true;
                break;
            case "D":
                down_Value = -1;
                down_down = true;
                break;
            case "L":
                left_Value = -1;
                left_down = true;
                break;
            case "R":
                right_Value = 1;
                right_down = true;
                break;
            case "A":
                if (scanObject != null)
                    manager.Action(scanObject);
                break;
            case "C":
                manager.SubMenuActive();
                break;
        }
    }

    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "U":
                up_Value = 0;
                up_up = true;
                break;
            case "D":
                down_Value = 0;
                down_up = true;
                break;
            case "L":
                left_Value = 0;
                left_up = true;
                break;
            case "R":
                right_Value = 0;
                right_up = true;
                break;
        }
    }
}

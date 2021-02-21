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


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        //Move Value
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //Button Down & Up Check
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

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
}

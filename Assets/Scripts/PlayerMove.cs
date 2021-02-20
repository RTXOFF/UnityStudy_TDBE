using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;

    public float Speed;
    float h;
    float v;
    bool isHorizonMove;
    Vector2 DirVec;
    GameObject scanObject;

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
            Debug.Log("This is " + scanObject.name);

    }
    private void FixedUpdate()
    {
        //Move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;

        //Ray
        Debug.DrawRay(rigid.position, DirVec * 0.7f, Color.green);
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, DirVec, 0.7f, LayerMask.GetMask("Object"));
        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
            scanObject = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;

    public float Speed;
    float h;
    float v;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(h, v) * Speed;
    }
}

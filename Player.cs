using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    Animator anim;
    Vector3 inputVec;


    float playerSpeed = 6; 



    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        transform.position += inputVec * Time.fixedDeltaTime * playerSpeed;
        anim.SetFloat("Walk", inputVec.magnitude);

        if (inputVec.x != 0)
            sprite.flipX = (inputVec.x < 1) ? true : false;
            
    }
}
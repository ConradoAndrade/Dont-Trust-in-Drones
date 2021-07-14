using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class plasyercontroller : MonoBehaviour
{
    public Rigidbody2D theRB;

    public float moveSpeed, jumpForce;

    public Transform groundPoint;

    public LayerMask whatIsGround;

    public bool isGrounded;

    public Animator anim;

    public InputMaster controls;

    private float inputX;


    void Start()
    {
        
    }
    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;

    }

    void Update()
    {
        theRB.velocity = new Vector2(inputX * moveSpeed, theRB.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);


        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);

        if (theRB.velocity.x > 0f)
        {
            transform.localScale = Vector3.one;
        }else if(theRB.velocity.x < 0f)
        {

            transform.localScale = new Vector3(-1, 1f, 1f);
        }


    }

    void Shoot()
    {

    }



}

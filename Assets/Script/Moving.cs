using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigidbody;
    private Animator animator;
    private float vertical,horizontal;
    private float standX,standY;
    private Vector3 offset;
    void Start()
    {
        offset=Camera.main.transform.position-transform.position;
        rigidbody=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
    }
    void Update()
    {
        vertical=Input.GetAxisRaw("Vertical");
        horizontal=Input.GetAxisRaw("Horizontal");
        Vector2 input=new Vector2(horizontal,vertical).normalized;
    //    rigidbody.velocity=input*speed;
        if(input!=Vector2.zero)
        {
            transform.rotation=Quaternion.LookRotation(input);
            rigidbody.velocity=input;
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            transform.Translate(Vector2.up*Time.deltaTime*speed);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.Translate(Vector2.down*Time.deltaTime*speed);
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(Vector2.left*Time.deltaTime*speed);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(Vector2.right*Time.deltaTime*speed);
        }
        if(input!=Vector2.zero)
        {
            animator.SetBool("Moving",true);
            standX=vertical;
            standY=horizontal;
        }
        else
        {
            animator.SetBool("Moving",false);
        }
        animator.SetFloat("InputX",standX);
        animator.SetFloat("InputY",standY);
        Camera.main.transform.position=transform.position+offset;
    }
}
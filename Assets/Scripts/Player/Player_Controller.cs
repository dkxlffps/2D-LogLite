using System;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float maxSpeed = 8f;
    public float jumpPower = 12f;

    public float rayCastLength = 1.35f;
    public Boolean isGround;

    Rigidbody2D rigidId ;
    SpriteRenderer spriteId;

    Animator aniController;

    Mouse_Controller mouseDash;

    public Boolean isCommand;

    void Awake() {
        rigidId = GetComponent<Rigidbody2D>();
        spriteId = GetComponent<SpriteRenderer>();
        aniController = GetComponent<Animator>();
        mouseDash = GetComponent<Mouse_Controller>();
    }

    void Update()
    {   
         
        RaycastHit2D hit = Physics2D.Raycast(rigidId.position, Vector3.down , rayCastLength, LayerMask.GetMask("Field"));
        if (hit.collider != null)
        {
            isGround = true;
            mouseDash.dahsCountReset();
        }
        else
            isGround = false;

        aniController.SetBool("isGround", isGround);



        if (isCommand)
            return;

        // Move Action

        //Jump
        if (Input.GetButtonDown("Jump"))
        { 
            if (isGround)
            {
              aniController.SetBool("isGround", false);
              rigidId.velocity = Vector2.zero;
              rigidId.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            rigidId.velocity = new Vector2(rigidId.velocity.normalized.x * 0.5f, rigidId.velocity.y);
        }
    }

    void FixedUpdate()
    {   

        
        //Move
        float h = Input.GetAxisRaw("Horizontal");
        rigidId.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (h != 0)
            spriteId.flipX = h == -1;

        //Max Spped
        if (rigidId.velocity.x > maxSpeed)
            rigidId.velocity = new Vector2(maxSpeed, rigidId.velocity.y);
        else if (rigidId.velocity.x < maxSpeed * (-1))
            rigidId.velocity = new Vector2(maxSpeed * (-1), rigidId.velocity.y);

    }
}

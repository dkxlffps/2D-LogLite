using System;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float jumpPower = 5f;

    public Boolean isGround;

    Rigidbody2D rigidId ;
    SpriteRenderer spriteId;

    Vector3 mousePosition;
    Camera mainCamera;

    public float rayCastLength = 1.35f;

    void Awake() {
        rigidId = GetComponent<Rigidbody2D>();
        spriteId = GetComponent<SpriteRenderer>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidId.position, Vector3.down , rayCastLength, LayerMask.GetMask("Field"));
        if (hit.collider != null)
            isGround = true;
        else
            isGround = false;

        if(Input.GetMouseButtonDown(1))
        {
            mousePosition = Input.mousePosition;
            mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        }

        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                rigidId.velocity = Vector2.zero;
              rigidId.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
        }


        if (Input.GetButtonUp("Horizontal"))
        {
            rigidId.velocity = new Vector2(rigidId.velocity.normalized.x * 0.5f, rigidId.velocity.y);
        }

        if (Input.GetButtonDown("Horizontal"))
            spriteId.flipX = Input.GetAxisRaw("Horizontal") == -1;
    }

    void FixedUpdate()
    {   
        //Move
        float h = Input.GetAxisRaw("Horizontal");
        rigidId.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //Max Spped
        if (rigidId.velocity.x > maxSpeed)
            rigidId.velocity = new Vector2(maxSpeed, rigidId.velocity.y);
        else if (rigidId.velocity.x < maxSpeed * (-1))
            rigidId.velocity = new Vector2(maxSpeed * (-1), rigidId.velocity.y);

    }
}

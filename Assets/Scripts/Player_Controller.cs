using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player_Controller : MonoBehaviour
{
    public float maxSpeed = 5f;
    Rigidbody2D rigidId ;
    SpriteRenderer spriteId;

    void Awake() {
        rigidId = GetComponent<Rigidbody2D>();
        spriteId = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Input.GetButtonUp("Horizontal"))
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

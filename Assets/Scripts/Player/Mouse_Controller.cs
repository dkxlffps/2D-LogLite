using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mouse_Controller : MonoBehaviour
{
    Rigidbody2D rigId;
    Transform mouseTransform;
    SpriteRenderer sprite;
    Camera mainCamera;

    Vector3 mousePosition;
    Vector3 transPosition;

    Vector2 dashPosition;


    //Dash
    public float dashSpeed = 30f;
    public int dashCount = 2;
    private int currDashCount;

    public float dashTime = 0.15f;
    private float currDashTime;

    private Boolean dashing;

    Animator animator;


    private void Awake()
    {
        currDashCount = dashCount;
        currDashTime = dashTime;
        dashing = false;
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rigId = GetComponent<Rigidbody2D>();
        mouseTransform = GameObject.Find("Mouse Aim").GetComponent<Transform>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transPosition.z = 0;
        mousePosition = transPosition;
        mouseTransform.position = mousePosition;

        if (Input.GetMouseButtonDown(1))
        {
            if(currDashCount > 0)
            {   
                rigId.velocity = Vector2.zero;
                dashPosition = new Vector2(mousePosition.x, mousePosition.y) - new Vector2(rigId.position.x, rigId.position.y);
                dashing = true;
                currDashCount--;
                currDashTime = dashTime;
                CreateDashEffectObject();
                sprite.flipX = dashPosition.x < 0;
            }
        }

        if (dashing)
        {
            Dash();
        }
    }

    private void Dash()
    {
        if(currDashTime > 0)
        {
            currDashTime -= Time.deltaTime;
            rigId.velocity = dashPosition.normalized * dashSpeed;
        }
        else
        {
            rigId.velocity = Vector2.zero;
            dashing = false;
        }
        animator.SetBool("isDash", dashing);
    }

    public void dahsCountReset()
    {
        currDashCount = dashCount;
    }

    private void CreateDashEffectObject()
    {
        /*GameObject dashEffect = Instantiate(Resources.Load("../../Prefab/Dash Effect.prefab"), rigId.position, new Quaternion(rigId.position);*/
    }
}

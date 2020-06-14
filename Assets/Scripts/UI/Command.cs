using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Command : MonoBehaviour
{
    private Boolean isCommand;
    private string command;
    Text textComponent;

    Player_Controller player_controller;
    

    private void Awake()
    {
        this.isCommand = false;
        player_controller.isCommand = this.isCommand;
        textComponent = GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            this.isCommand = !this.isCommand;
            player_controller.isCommand = this.isCommand;
        }

        if (this.isCommand)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

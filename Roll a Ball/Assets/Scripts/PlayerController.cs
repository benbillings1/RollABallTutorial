﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    public AudioClip pickUpSound;
    public bool isGrounded;
    public float jumpPower = 3.0f;

    private Rigidbody rb;
    private int count;
    private int nrOfAllowedDJumps = 1;
    private int dJumpCounter = 0;
    private Vector3 moveDirection = Vector3.zero;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText ();
        winText.text = "";
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText ();
            GetComponent<AudioSource>().PlayOneShot(pickUpSound); 
        }
    }

    void SetCountText ()
    {
        countText.text = "Count: " + count.ToString ();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }
            
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            return;
        }

        isGrounded = true;
    }

    private void Update()
    {
        if(Input.GetButton("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isGrounded = false;

        }
    }

    
}






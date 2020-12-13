using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Vector3 Movement;

    public float PlayerSpeed;

    public float JumpHeight;

    public bool isJumpPressed;

    public Rigidbody RB;

    public Animator Anim;

    private bool Grounded;

    void Start() {
        PlayerSpeed = 10.0f;
        JumpHeight = 5.0f;
        Movement = new Vector3(0.0f, 0.0f, 0.0f);
        RB = this.GetComponent<Rigidbody>();
    }

    void Update() {
        isJumpPressed = Input.GetButtonDown("Jump");
        Movement.x = Input.GetAxisRaw("Horizontal");

        // Debug.Log(Movement.x);

        Anim.SetBool("Grounded", Grounded);
        Anim.SetFloat("Speed", Movement.x);
    }

    void FixedUpdate() {
        Jump();
        RB.MovePosition(RB.position + Movement * PlayerSpeed * Time.deltaTime);

        Grounded = Physics.OverlapSphere(transform.position, 0.3f, 1)[0];


    }

    void Jump() {
         // if (isJumpPressed && ground) {
         //     // the cube is going to move upwards in 10 units per second
         //     RB.velocity = new Vector3(0, 5, 0);
         //     ground = false;
         // }
    }
}

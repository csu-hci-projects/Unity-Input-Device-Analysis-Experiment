using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public CharacterController characterController;
    public float speed;
    public float rotationSpeed;
    public float gravity = -9.81f;
    public Vector3 velocity;
    public float jump;
    public float rise;
    private float canJump = 0f;
    private bool using_gamepad = false;
    private Vector3 walk;

    [SerializeField]
    private PlayerInputControls controls;


    private void performJump() {
        if (Time.time > canJump) {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
            canJump = Time.time + 1.5f;
        }
    }

    private void Start() {
        if (UI_Manager.using_gamepad) {
            controls = new PlayerInputControls();

            controls.Gameplay.Jump.performed += context => performJump();
            controls.Gameplay.Walk.performed += context => walk = context.ReadValue<Vector2>();
            controls.Gameplay.Walk.canceled += context => walk = Vector3.zero;
            using_gamepad = true;
            controls.Gameplay.Enable();
    }


    }
    private void Update()
    {   
        float x, z;
        if (using_gamepad) {
            x = walk.x;
            z = walk.y;
        } else {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
        }
        Vector3 move = transform.forward * z;
        if (characterController.isGrounded && velocity.y < 0) {
            velocity.y = 0;
        }
        if (Input.GetButtonDown("Jump") && Time.time > canJump && !using_gamepad) {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
            canJump = Time.time + 1.5f;
        }
        // if (Input.GetButtonDown("Vertical")) {
        //     if (Input.GetButtonDown("Jump") && Time.time > canJump) {
        //         velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        //         canJump = Time.time + 1.5f;
        //     }
        // }
        // if (Input.GetButtonUp("Vertical")) {
        //     if (Input.GetButtonDown("Jump") && Time.time > canJump) {
        //         velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        //         canJump = Time.time + 1.5f;
        //     }
        // }
        transform.Rotate(0.0f, x * rotationSpeed * Time.deltaTime, 0.0f);
        characterController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}

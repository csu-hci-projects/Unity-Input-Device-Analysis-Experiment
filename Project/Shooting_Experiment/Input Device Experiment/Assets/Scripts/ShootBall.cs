using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using System;
 
public class ShootBall : MonoBehaviour
{
   public Camera mainCam;
   public GameObject projectile;
   public float launchVelocity = 700f;
   private Vector3 worldPosition;
   private Ray ray; 
   private bool using_gamepad = false;
   [SerializeField]
   private PlayerInputControls controls;

   private void Start() {
       if (UI_Manager.using_gamepad) {
           using_gamepad = true;
           controls = new PlayerInputControls();
           controls.Gameplay.Shoot.performed += ctx => performShoot();
           controls.Gameplay.Enable();
       }

   }

   private void performShoot() {
       GameObject ball = Instantiate(projectile, transform.position,  
                                                transform.rotation);
        Rigidbody p_clone = ball.GetComponent<Rigidbody>();
        p_clone.AddForce(transform.forward*launchVelocity);
   }

   private void Update()
   {
        ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        if(Physics.Raycast(ray, out hitData, Mathf.Infinity)) {
            worldPosition = hitData.point;
            transform.LookAt(worldPosition);
            float rot;
            if (transform.eulerAngles.x <= 180f) {
                rot = transform.eulerAngles.x;
            } else {
                rot = transform.eulerAngles.x - 360f;
            }
        }
        if (Input.GetButtonDown("Fire1") && !using_gamepad) {
            performShoot();
        }
   }
}

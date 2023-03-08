using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.Users;
//Wei Lun Tsai

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float maxForce;


    private Player player;
    private PlayerInput playerInput;
   

    private Vector2 moveInput = Vector2.zero;
    public bool interactInput = false;
    public bool placeAnimalInput = false;

    private void Start()
    {
        player = GetComponent<Player>();
        playerInput = GetComponent<PlayerInput>();


    }

    private void FixedUpdate()
    {
        Movement();
    }
    
    private void Movement()
    {
        if (player.InPanel) return;

        Vector3 currentVel = rb.velocity;
        Vector3 targetVel = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        targetVel *= moveSpeed;

        //align direction
        targetVel = transform.TransformDirection(targetVel);

        //Calculate forces
        Vector3 velocityChange = targetVel - currentVel;
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        //limit force
        velocityChange = Vector3.ClampMagnitude(velocityChange, maxForce);

        velocityChange.y = 0;

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        interactInput = context.action.IsPressed();
    }
    public void OnPlaceAnimal(InputAction.CallbackContext context)
    {
        placeAnimalInput = context.action.triggered;
    }

}

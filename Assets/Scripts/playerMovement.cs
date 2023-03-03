using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.Users;
//Wei Lun Tsai

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector3 dir;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float maxForce;

    [SerializeField] private BackpackManager backpack;

    private Player player;

    private Vector2 moveInput = Vector2.zero;
    public  bool interactInput = false;
    private bool placeAnimalInput = false;

    private void Start()
    {
        player = GetComponent<Player>();
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

    private void OnTriggerStay(Collider other)
    {
        //if player trigger with animal and press E, get that animal and add it to the bag
        if (other.gameObject.tag.Equals("Animal") && Input.GetKey(KeyCode.E))
        {
            AnimalInstance animal = other.gameObject.GetComponent<AnimalInstance>();
            backpack.AddItem(animal);
        }
    }
}

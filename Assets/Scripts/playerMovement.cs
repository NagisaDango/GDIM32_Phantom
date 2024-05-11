using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
    [SerializeField] private Animator anim ;
    private Vector3 playerMoveRange_UL;
    private Vector3 playerMoveRange_BR;


    private Player player;
    private PlayerInput playerInput;
   

    private Vector2 moveInput = Vector2.zero;
    public bool interactInput = false;
    public bool placeAnimalInput = false;

    private void Start()
    {
        player = GetComponent<Player>();
        playerInput = GetComponent<PlayerInput>();
        playerMoveRange_UL = GameObject.Find("PlayerMoveRange_UL").transform.position;
        playerMoveRange_BR = GameObject.Find("PlayerMoveRange_BR").transform.position;


    }

    private void FixedUpdate()
    {
        Movement();
    }
    
    private void Movement()
    {
        if (player.InPanel) return;

        Vector3 currentVel = rb.velocity;

        Vector3 lookPos = transform.position + new Vector3(moveInput.x, 0, moveInput.y);
        transform.LookAt(lookPos);

        Vector3 targetVel = new Vector3(moveInput.x, 0, moveInput.y).normalized;


        targetVel *= moveSpeed;

        if (targetVel != Vector3.zero) anim.SetBool("Running", true);
        else anim.SetBool("Running", false);

        targetVel.y = rb.velocity.y;

        Vector3 nextPos = transform.position + targetVel * Time.fixedDeltaTime;
        // Debug.Log(transform.position + " , " + nextPos );
        if (nextPos.x < playerMoveRange_UL.x || nextPos.z > playerMoveRange_UL.z ||
            nextPos.x > playerMoveRange_BR.x || nextPos.z < playerMoveRange_BR.z)
        {
            Debug.Log("at boundary");
            rb.velocity = Vector3.zero;
            return;
        }
            

        rb.velocity = targetVel;

        

        #region Old
        /*
        
        //Debug.Log("Input vector " + targetVel);

        //transform.rotation = Quaternion.Euler(0,moveInput.x,0);
        
        targetVel *= moveSpeed;

        
        Debug.DrawRay(transform.position, targetVel);
        transform.LookAt(targetVel);

        //align direction
        //targetVel = transform.TransformDirection(targetVel);

        //Calculate forces
        Vector3 velocityChange = targetVel - currentVel;
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        //limit force
        velocityChange = Vector3.ClampMagnitude(velocityChange, maxForce);

        velocityChange.y = 0;

        rb.AddRelativeForce(velocityChange, ForceMode.VelocityChange);

        Debug.Log("force being added" + velocityChange);
        //transform.LookAt(velocityChange);
        //rb.AddTorque(velocityChange);
        */
        #endregion

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

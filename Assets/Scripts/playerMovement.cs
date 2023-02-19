using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Wei Lun Tsai

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector3 dir;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float maxForce;

    [SerializeField] private BackpackManager backpack;

    


    private void FixedUpdate()
    {
        Movement();
    }


    private void Movement()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        //Method one using Translate():
        //dir = new Vector3(hInput, 0, vInput); 
        //transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);

        //Method two using AddForce
        //find targer velocity
        Vector3 currentVel = rb.velocity;
        Vector3 targetVel = new Vector3(hInput, 0, vInput).normalized;
        targetVel *= moveSpeed;

        //align direction
        targetVel = transform.TransformDirection(targetVel);

        //Calculate forces
        Vector3 velocityChange = targetVel - currentVel;
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        //limit force
        velocityChange = Vector3.ClampMagnitude(velocityChange, maxForce);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);


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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector3 dir;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform tf;

    [SerializeField] private ItemDecorator decorator;
    [SerializeField] private IDecoratorManager decoratorManager;

    [SerializeField] private BackpackManager backpack;


    private void FixedUpdate()
    {
        Movement();
    }
    //movement of the player
    private void Movement()
    {
        float hInput = Input.GetAxisRaw("Horizontal");

        float vInput = Input.GetAxisRaw("Vertical");

        dir = this.transform.right * hInput + this.transform.forward * vInput;
        rb.AddForce(dir.normalized * moveSpeed, ForceMode.Force);
    }
    private void OnTriggerStay(Collider other)
    {
        //if player trigger with animal and press E, get that animal and add it to the bag
        if (other.gameObject.tag.Equals("Animal")&&Input.GetKey(KeyCode.E))
        {
            AnimalInstance animal = other.gameObject.GetComponent<AnimalInstance>();
            backpack.AddItem(animal);
            if (backpack.addable)
            {
                other.gameObject.SetActive(false);
            }
        }
    }

}

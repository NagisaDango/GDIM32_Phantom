using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector3 dir;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform tf;
    //[SerializeField] Animator animator;
    private Vector3 direction;

    [SerializeField] private ItemDecorator decorator;
    [SerializeField] private IDecoratorManager decoratorManager;

    [SerializeField] private BackpackManager backpack;

    [SerializeField] public bool inFarm;

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        float hInput = Input.GetAxisRaw("Horizontal");

        float vInput = Input.GetAxisRaw("Vertical");

        dir = this.transform.right * hInput + this.transform.forward * vInput;
        rb.AddForce(dir.normalized * moveSpeed, ForceMode.Force);
    }

    private void OnTriggerStay(Collider other)
    {
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Farm"))
        {
            inFarm=true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        inFarm = false;
    }
}

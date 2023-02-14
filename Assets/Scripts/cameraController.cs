using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private float xRot;
    private float yRot;
    [SerializeField] private float xSen;
    [SerializeField] private float ySen;

    private void Update()
    {
        follow();
    }

    private void follow()
    {
        this.transform.position = target.transform.position;

        float mosueX=Input.GetAxisRaw("Mouse X")*Time.deltaTime*xSen;
        float mosueY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySen;

        target.transform.Rotate(Vector3.up * mosueX);
        xRot -= mosueY;
        xRot = Mathf.Clamp(xRot, -80, 80);
        yRot += mosueX;

        this.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        target.transform.rotation=Quaternion.Euler(0, yRot, 0);
    }

}

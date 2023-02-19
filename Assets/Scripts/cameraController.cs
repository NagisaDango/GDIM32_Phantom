using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Wei Lun Tsai
public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject targetPlayer;
    private float xRot;
    private float yRot;
    [SerializeField] private float xSen;
    [SerializeField] private float ySen;
    [SerializeField] private float xThreshold = 80f;


    private void LateUpdate()
    {
        follow();
    }

    private void follow()
    {
        //follow the target
        this.transform.position = targetPlayer.transform.position;

        //if viewing a panel, freeze the camera
        if (targetPlayer.GetComponent<Player>().InPanel) return;

        //change the camera rotation according to the player mouse
        float mosueX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSen;
        float mosueY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySen;

        targetPlayer.transform.Rotate(Vector3.up * mosueX);

        //set threshold for camera angel
        xRot -= mosueY;
        xRot = Mathf.Clamp(xRot, -xThreshold, xThreshold);
        yRot += mosueX;


        this.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
    }

}

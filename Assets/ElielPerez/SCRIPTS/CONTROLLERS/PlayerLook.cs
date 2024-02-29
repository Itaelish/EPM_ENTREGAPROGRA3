using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private float movY;
    private float movX;

    public float mouseSenseY;
    public float mouseSenseX;

    private Transform player;

    private float rotY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = transform.parent;
    }

    void Update()
    {
        //Camera
        movX = Input.GetAxis("Mouse X") * mouseSenseX * Time.deltaTime;
        movY = Input.GetAxis("Mouse Y") * mouseSenseX * Time.deltaTime;

        rotY -= movY;
        rotY = Mathf.Clamp(rotY, -90f, 90f);

        player.Rotate(0, movX, 0);
        transform.localRotation = Quaternion.Euler(rotY, 0, 0);

        //Movement

    }
}
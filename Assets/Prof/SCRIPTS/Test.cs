using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Test : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float runMultiplier = 2f;
    private Rigidbody rb;

    private PlayerInput input;
    [SerializeField] private InputSystem inputSystem;

    Vector2 moveDirection = Vector2.zero;
    Vector2 rotateDirection = Vector2.zero;

    private Action Movement;
    private Action Rotation;
    private Action Jump;
    private Action Run;
    private Action Shoot;
    private Action Reload;

    private void OnValidate()
    {

        switch (inputSystem)
        {

            case InputSystem.OldInputSystem:
                {

                    Movement = OldInputSystemMovement;
                    Rotation = OldInputSystemRotation;
                    Jump = OldInputSystemJump;
                    Run = OldInputSystemRun;
                    Shoot = OldInputSystemShoot;
                    Reload = OldInputSystemReload;
                    break;

                }

            case InputSystem.NewInputSystem:
                {

                    Movement = NewInputSystemMovement;
                    Rotation = NewInputSystemRotation;
                    Jump = NewInputSystemJump;
                    Run = NewInputSystemRun;
                    Shoot = NewInputSystemShoot;
                    Reload = NewInputSystemReload;
                    break;

                }

        }



    }
    private void Start()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotation();
        Jump();
        Run();
        Shoot();
        Reload();
    }


    #region New Input System

    private void NewInputSystemShoot()
    {
        if (input.actions["Shoot"].triggered)
        {
            
            Debug.Log("Disparando en el nuevo input");
        }
    }

    private void NewInputSystemReload()
    {
        if (input.actions["Recargar"].triggered)
        {
            // Código adicional para recargar
            Debug.Log("Recargando en el nuevo input.");
        }
    }
    private void NewInputSystemRun()
    {
        if (input.actions["Run"].triggered)
        {
            // Código adicional cuando la acción "Run" se activa
            float speedMultiplier = runMultiplier;

            // Debug para verificar si se está corriendo
            Debug.Log("Estoy corriendo Nuevo sistema");

            // Resto del código de correr...
            transform.position += transform.rotation * new Vector3(0, 0, NewMoveDirection()) * (movementSpeed * speedMultiplier * Time.deltaTime);
        }
    }
    private void NewInputSystemJump()
    {
        if (input.actions["Jump"].triggered)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void NewInputSystemMovement()
    {
        transform.position += this.transform.rotation * new Vector3(x: 0, y: 0, z: NewMoveDirection()) * (movementSpeed * Time.deltaTime);
    }

    private void NewInputSystemRotation()

    {
        transform.Rotate(new Vector3(0, NewInputSystemRotationDirection(), 0) * (rotationSpeed * Time.deltaTime));
    }

    private float NewMoveDirection()
    {
        return input.actions["Move"].ReadValue<Vector2>().y;
    }

    private float NewInputSystemRotationDirection ()
    {
        return input.actions["Move"].ReadValue<Vector2>().x;
    }

    #endregion

    #region Old System Input
    private void OldInputSystemShoot()
    {
        if (InputHandler.ShootInput())
        {
            // Código adicional para disparar
            Debug.Log("Disparando en el antiguo input");
        }
    }

    private void OldInputSystemReload()
    {
        if (InputHandler.ReloadInput())
        {
            // Código adicional para recargar
            Debug.Log("Recargando en el antiguo input");
        }
    }
    private void OldInputSystemRun()
    {
        if (InputHandler.RunInput())
        {
            float speedMultiplier = runMultiplier;

            // Debug para verificar si se está corriendo
            Debug.Log("Estoy corriendo Old input.");

        
            transform.position += transform.rotation * new Vector3(0, 0, OldSystemMovementDirection().y) * (movementSpeed * speedMultiplier * Time.deltaTime);
        }
    }

    private void OldInputSystemJump()
    {
        if (InputHandler.JumpInput()) 
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    private void OldInputSystemRotation()
    {
        transform.Rotate(new Vector3(0, OldSystemRotationDirection().x, 0) * (rotationSpeed * Time.deltaTime));
    }
    //void OldInputMovement()
    //{
    //    transform.position += this.transform.rotation * new Vector3(x: 0, y: 0, z: OldMoveDirection().y) * (movementSpeed * Time.deltaTime);
    //}
 

    private void OldInputSystemMovement()
    {
        transform.position += this.transform.rotation * new Vector3(0, 0, OldSystemMovementDirection().y) * (movementSpeed * Time.deltaTime);
    }


    private Vector2 OldSystemMovementDirection()

    {
        if (InputHandler.WalkForwardInput())
        {

            moveDirection += Vector2.up;

        }

        if (InputHandler.WalkBackwardInput())
        {

            moveDirection += Vector2.down;

        }
        if (!InputHandler.WalkForwardInput() & !InputHandler.WalkBackwardInput())
        {
            moveDirection = Vector2.zero;
        }

        return moveDirection.normalized;
        
    }

    Vector2 OldSystemRotationDirection()
    {

        if (InputHandler.RotateRightInput())
        {

            rotateDirection += Vector2.right;

        }
        if (InputHandler.RotateLeftInput())
        {

            rotateDirection += Vector2.left;

        }
        if (!InputHandler.RotateRightInput() && !InputHandler.RotateLeftInput())
        {

            rotateDirection = Vector2.zero;

        }

        return rotateDirection.normalized;

    }
    #endregion
}
public enum InputSystem
{
    OldInputSystem, NewInputSystem

}
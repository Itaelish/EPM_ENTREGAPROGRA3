using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private  int items = 0;
    private float playerSpeed = 7f;
    private float jumpHeight = 4f;
    private float gravityValue = -9.8f;

    private bool hasJumped = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            hasJumped = false; // Permite saltar nuevamente cuando está en el suelo
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * Time.deltaTime * playerSpeed);

        // Detectar el salto en Update y realizar el salto en FixedUpdate
        if (Input.GetButtonDown("Jump") && isGrounded && !hasJumped)
        {
            hasJumped = true; // Evita saltar múltiples veces en el aire
        }
    }

    void FixedUpdate()
    {
        PerformJump();
    }

    void PerformJump()
    {
        if (hasJumped)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravityValue);
            hasJumped = false;
        }

        playerVelocity.y += gravityValue * Time.fixedDeltaTime;
        controller.Move(playerVelocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            items += 1;
            Destroy(other.gameObject);
        }
    }
}

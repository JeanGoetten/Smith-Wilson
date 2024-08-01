using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;          // Velocidade de movimenta��o
    public float jumpForce = 5f;          // Força do pulo
    //public float mouseSensitivity = 100f; // Sensibilidade do mouse para rotação da camera
    //public Transform cameraTransform;     // Transform da c�mera

    private Rigidbody rb;
    //private float xRotation = 0f;
    private bool isGrounded;

    //float mouseX;
    //float mouseY;

    private void Awake()
    {
        //mouseX = 20f;
        //mouseY = 0f;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // Trava o cursor no meio da tela
    }

    void Update()
    {
        Move();
        //RotateCamera();

        //cameraTransform.localPosition = new Vector3(0f, 11f, -10f);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal"); // Pega o input das teclas A e D
        float moveZ = Input.GetAxis("Vertical");   // Pega o input das teclas W e S

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move = move.normalized; // Normaliza o vetor de movimento para evitar velocidade maior na diagonal

        rb.AddForce(move * moveSpeed, ForceMode.Acceleration);
    }


    //private void RotateCamera()
    //{
    //    mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    //    mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    //    xRotation -= mouseY;
    //    xRotation = Mathf.Clamp(xRotation, 20, 50);

    //    cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    //    transform.Rotate(Vector3.up * mouseX);

    //}

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
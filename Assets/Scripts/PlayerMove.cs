using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;          // Velocidade de movimenta��o
    public float jumpForce = 5f;          // Força do pulo
    public float mouseSensitivity = 100f; // Sensibilidade do mouse para rotação da camera
    public Transform cameraTransform;     // Transform da c�mera

    private Rigidbody rb;
    private float xRotation = 0f;
    private bool isGrounded;

    float mouseX;
    float mouseY;

    public float camMinInclination = 0;
    public float camMaxInclination = 0;

    private Quaternion targetRotation; // Rotação alvo do personagem

    private void Awake()
    {
        mouseX = 20f;
        mouseY = 0f;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // Trava o cursor no meio da tela

        targetRotation = transform.rotation; // Define a rotação inicial
    }

    void Update()
    {
        Move();
        //RotateCamera();

        cameraTransform.localPosition = new Vector3(0f, 1f, 0f);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // Captura as teclas A e D
        float moveVertical = Input.GetAxis("Vertical"); // Captura as teclas W e S

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Verifica se o Shift está sendo pressionado para aumentar a velocidade
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            movement *= 2;
        }

        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        // Ajusta a rotação com base na tecla pressionada
        if (Input.GetKey(KeyCode.W))
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            targetRotation = Quaternion.Euler(0, -90, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            targetRotation = Quaternion.Euler(0, 90, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // Verifica a direção atual e ajusta a rotação para S
            if (transform.rotation.eulerAngles.y == 90)
            {
                targetRotation = Quaternion.Euler(0, 170, 0);
            }
            else if (transform.rotation.eulerAngles.y == -90)
            {
                targetRotation = Quaternion.Euler(0, -170, 0);
            }
            else
            {
                targetRotation = Quaternion.Euler(0, 170, 0);
            }
        }

        // Aplica a rotação ao personagem
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
    }


    private void RotateCamera()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, camMinInclination, camMaxInclination);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

    }

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
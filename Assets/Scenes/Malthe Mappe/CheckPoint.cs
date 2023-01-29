using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public float walkSpeed = 2.0f;
    public float runSpeed = 6.0f;
    public float jumpForce = 8.0f;
    public float crouchSpeed = 1.0f;
    public float rotationSpeed = 3.0f;


    private bool isCrouching = false;
    private bool isJumping = false;
    private bool isRunning = false;
    public Transform cameraTransform;


    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0, moveZ);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (Input.GetKey(KeyCode.C))
        {
            if (isCrouching == false)
            {
                isCrouching = true;
            }
            else
            {
                isCrouching = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }

        if (isRunning)
        {
            move = move.normalized * runSpeed;
        }
        else if (isCrouching)
        {
            move = move.normalized * crouchSpeed;
        }
        else
        {
            move = move.normalized * walkSpeed;
        }

        rb.MovePosition(transform.position + move * Time.deltaTime);


        float yRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        float xRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
        Vector3 direction = gameObject.transform.forward;

        transform.Rotate(0, yRotation, 0, Space.Self);

        cameraTransform.Rotate(-xRotation, 0, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}

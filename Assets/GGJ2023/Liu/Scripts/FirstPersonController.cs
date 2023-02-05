using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float sensitivity = 2.0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private float rotX, rotY;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        rotX = transform.localEulerAngles.x;
        rotY = transform.localEulerAngles.y;
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetMouseButton(0))
        {
            rotX += Input.GetAxis("Mouse Y") * sensitivity;
            rotY += Input.GetAxis("Mouse X") * sensitivity;
            rotX = Mathf.Clamp(rotX, -90f, 90f);
            transform.localEulerAngles = new Vector3(-rotX, rotY, 0);
        }
    }
}

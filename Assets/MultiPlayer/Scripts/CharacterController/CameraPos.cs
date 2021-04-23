using UnityEngine;

public class CameraPos : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 5f;
    public float gravity = -1.62f;

    public float jumpHeight = 2;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public LayerMask solidMask;

    Vector3 velocity;
    public static bool isGrounded = false;
    public static bool isSolid = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isSolid = Physics.CheckSphere(groundCheck.position, groundDistance, solidMask);

        if (isSolid && velocity.y < 0)
        {
            velocity.y = -2f;
            speed = 5f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isSolid)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            speed = 2.5f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


    }
}

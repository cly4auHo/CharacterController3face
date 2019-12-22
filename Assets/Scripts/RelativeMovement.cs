using UnityEngine;

public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotSpeed = 15.0f;
    [SerializeField] private float moveSpeed = 6.0f;

    private float jumpSpeed = 15.0f;
    private float gravity = -9.8f;
    private float terminalVelocity = -10.0f;
    private float minFall = -1.5f;
    private float vertSpeed;
    private bool hitGround = false;
    private float pushForce = 3.0f;

    private CharacterController charController;
    private Rigidbody rb;
    private ControllerColliderHit contact;
    private Animator animator;

    private Vector3 movement;
    private Quaternion direction;
    private Quaternion tmp;
    private float horInput;
    private float vertInput;

    void Start()
    {
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        vertSpeed = minFall;
    }

    void Update()
    {
        movement = Vector3.zero;
        horInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");

        if (horInput != 0 || vertInput != 0)
        {
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);

            tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);

            movement = target.TransformDirection(movement);
            target.rotation = tmp;

            direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);

        }

        animator.SetFloat("Speed", movement.sqrMagnitude);

        hitGround = false; // raycast down to address steep slopes and dropoff edge
        RaycastHit hit;

        if (vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (charController.height + charController.radius) / 1.9f;
            hitGround = hit.distance <= check;  // to be sure check slightly beyond bottom of capsule
        }

        if (charController.isGrounded) // y movement: possibly jump impulse up, always accel down                                     
        {               // could _charController.isGrounded instead, but then cannot workaround dropoff edge
            if (Input.GetButtonDown("Jump"))
            {
                vertSpeed = jumpSpeed;
            }
            else
            {
                vertSpeed = minFall;
                animator.SetBool("Jumping", false);
            }
        }
        else
        {
            vertSpeed += gravity * 5 * Time.deltaTime;

            if (vertSpeed < terminalVelocity)
            {
                vertSpeed = terminalVelocity;
            }

            if (contact != null) // not right at level start
            {
                animator.SetBool("Jumping", true);
            }

            if (charController.isGrounded)  // workaround for standing on dropoff edge
            {
                if (Vector3.Dot(movement, contact.normal) < 0)
                {
                    movement = contact.normal * moveSpeed;
                }
                else
                {
                    movement += contact.normal * moveSpeed;
                }
            }
        }

        movement.y = vertSpeed;
        movement *= Time.deltaTime;

        charController.Move(movement);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contact = hit;

         rb = hit.collider.attachedRigidbody;
        if (rb != null && !rb.isKinematic)
        {
            rb.velocity = hit.moveDirection * pushForce;
        }
    }
}

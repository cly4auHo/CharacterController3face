using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotSpeed = 15.0f;
    [SerializeField] private float moveSpeed = 6.0f;
    [SerializeField] private float jumpSpeed = 15.0f;

    private float gravity = -9.8f;
    private float terminalVelocity = -10.0f;
    private float minFall = -1.5f;
    private float vertSpeed;

    private CharacterController charController;

    private Vector3 movement;
    private Quaternion direction;
    private Quaternion tmp;
    private float horInput;
    private float vertInput;

    void Start()
    {
        charController = GetComponent<CharacterController>();
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

            if (charController.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    vertSpeed = jumpSpeed;
                }
                else
                {
                    vertSpeed = minFall;
                }
            }
            else
            {
                vertSpeed += gravity * 5 * Time.deltaTime;

                if (vertSpeed < terminalVelocity)
                {
                    vertSpeed = terminalVelocity;
                }
            }

            movement.y = vertSpeed;
            movement *= Time.deltaTime;
            charController.Move(movement);
        }
    }
}

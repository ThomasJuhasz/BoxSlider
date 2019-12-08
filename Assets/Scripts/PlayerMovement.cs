using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

enum MoveDirection
{
    Right = 1,
    Left = -1,
    None = 0
}

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rigidBody;
    public Slider dashCooldown;
    public float speed = 100;
    public float sidewaysForce = 50;
    public float dashLength = 0;

    private bool isGrounded = true;
    MoveDirection moveDirection = MoveDirection.None;

    void Start()
    {
        dashCooldown.value = 1;
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x > Screen.width / 2)
            {
                moveDirection = MoveDirection.Right;
            }
            else
            {
                moveDirection = MoveDirection.Left;
            }

        } else
        {
            moveDirection = MoveDirection.None;
        }


        var horizontalMove = Input.GetAxisRaw("Horizontal");

        if (horizontalMove != 0)
        {
            moveDirection = (MoveDirection)horizontalMove;
        }

        if (Input.GetAxisRaw("Jump") > 0 & isGrounded)
        {
            rigidBody.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
            isGrounded = false;
        }

        if (Input.GetAxisRaw("Vertical") > 0 && dashCooldown.value >= 1)
        {
            rigidBody.AddForce(new Vector3(0, 0, 30), ForceMode.Impulse);
            dashCooldown.value = 0;
        }

        dashCooldown.value += Time.deltaTime;
    }

    void FixedUpdate()
    {
        rigidBody.AddRelativeForce(Vector3.forward * speed);
        rigidBody.AddForce((float)moveDirection * sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);

        if (rigidBody.position.y < -1)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}

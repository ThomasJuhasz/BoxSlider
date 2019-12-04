using UnityEngine;
using UnityEngine.EventSystems;
enum MoveDirection
{
    Right = 1,
    Left = -1,
    None = 0
}

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float speed = 100;
    public float sidewaysForce = 50;

    MoveDirection moveDirection = MoveDirection.None;

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

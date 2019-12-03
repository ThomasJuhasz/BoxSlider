using UnityEngine;
using UnityEngine.EventSystems;
enum MoveDirection { Right, Left }

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float forwardForce = 1f;
    public float sidewaysForce = 50f;

    float timeBetweenTaps = .01f;
    float tapTimer = 0;

    int _lane = 3;
    int lane {
        get {
            return _lane;
        }
        set {
            value = value > 5 ? 5 : value;
            value = value < 1 ? 1 : value;
            _lane = value;
        }
    }

    bool inMotion;

    void SetLane(MoveDirection moveDirection, int taps)
    {        
        if (moveDirection == MoveDirection.Right)
        {
            lane += taps;
        }
        else
        {
            lane -= taps;
        }

        tapTimer = 0;
        inMotion = true;        
    }

    void Update()
    {
        tapTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && tapTimer >= timeBetweenTaps)
        {
            if (Input.mousePosition.x > Screen.width / 2)
            {
                SetLane(MoveDirection.Right, 1);
            }
            else
            {
                SetLane(MoveDirection.Left, 1);
            }

        }
    }

    void FixedUpdate()
    {
        rigidBody.AddForce(0, 0, forwardForce * Time.deltaTime, ForceMode.Force);
        
        if (inMotion)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(lane * 3f, transform.position.y, transform.position.z), .1f);

            if (rigidBody.transform.position.x == lane * 3f)
            {
                inMotion = false;
            }
        }
        

        if (rigidBody.position.y < -1)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}

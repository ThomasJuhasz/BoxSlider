using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction
{
    Left,
    Right
}

public class ShiftingObstacle : MonoBehaviour
{
    public GameObject leftArrow;
    public GameObject rightArrow;
    Direction direction;

    float startPosition;

    void Start()
    {
        startPosition = transform.position.x;
        if(Random.Range(0, 2) > 0)
        {
            leftArrow.SetActive(false);
            direction = Direction.Right;
        } else
        {
            rightArrow.SetActive(false);
            direction = Direction.Left;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == Direction.Left)
        {
            if (transform.position.x > startPosition + (Vector3.left * 3).x)
            {
                transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.left * 3, Time.deltaTime);
            }
        } else
        {
            if (transform.position.x < startPosition + (Vector3.right * 3).x)
            {
                transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.right * 3, Time.deltaTime);
            }
        }
    }
}

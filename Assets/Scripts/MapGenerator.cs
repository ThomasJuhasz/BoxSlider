using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject obstacle;
    public Transform player;
    public int spacesBetweenObstacles = 15;

    int rowsOfBlocksToGenerateAtOnce = 5;
    int generationThreshold;
    List<GameObject> obstacles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        generationThreshold = spacesBetweenObstacles * 5;
        GenerateBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z > transform.position.z - generationThreshold)
        {
            GenerateBlocks();
        }
    }

    void GenerateBlocks()
    {
        for (int i = 0; i < rowsOfBlocksToGenerateAtOnce; i++)
        {
            var randomPositions = new List<int>();

            while (randomPositions.Count < 3)
            {
                var random = Random.Range(1, 15);

                while (randomPositions.Exists(x => x < random + 2 && x > random - 2))
                {
                    random = Random.Range(1, 15);
                }

                randomPositions.Add(random);
            }

            AddBlock(randomPositions[0], 0);
            AddBlock(randomPositions[1], 0);
            AddBlock(randomPositions[2], 0);

            transform.position += new Vector3(0, 0, spacesBetweenObstacles);
        }
    }

    void AddBlock(int x, int z)
    {
        var pos = transform.position + new Vector3(x, 0, z);

        obstacles.Add(Instantiate(obstacle, pos, Quaternion.identity));

        if (obstacles.Count > rowsOfBlocksToGenerateAtOnce * 3 * 2)
        {
            Destroy(obstacles[0]);
            obstacles.RemoveRange(0, 1);
        }
    }
}

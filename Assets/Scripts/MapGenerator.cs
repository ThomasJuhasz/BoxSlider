using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject obstacleContainer;
    public GameObject obstacle;
    public GameObject shiftingObstacle;
    public Transform player;
    public int spacesBetweenObstacles = 15;

    int rowsOfBlocksToGenerateAtOnce = 10;
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

    int RandomPosition()
    {
        return Random.Range(0, 5) * 3;
    }

    void GenerateBlocks()
    {
        for (int i = 0; i < rowsOfBlocksToGenerateAtOnce; i++)
        {
            var randomPositions = new List<int>();

            while (randomPositions.Count < 3)
            {
                var random = RandomPosition();

                while (randomPositions.Exists(x => x == random))
                {
                    random = RandomPosition();
                }

                randomPositions.Add(random);
            }

            randomPositions.ForEach(x => {
                if(Random.Range(0, 10) < 3)
                {
                    AddBlock(x, obstacle);
                    //  AddBlock(x, shiftingObstacle);
                } else
                {
                    AddBlock(x, obstacle);
                }
            });

            transform.position += new Vector3(0, 0, spacesBetweenObstacles);
        }
    }

    void AddBlock(int x, GameObject o)
    {
        var pos = transform.position + new Vector3(x, .5f, 0);
        var newBlock = Instantiate(o, pos, Quaternion.identity);
        newBlock.transform.parent = obstacleContainer.transform;

        obstacles.Add(newBlock);

        if (obstacles.Count > rowsOfBlocksToGenerateAtOnce * 3 * 2)
        {
            Destroy(obstacles[0]);
            obstacles.RemoveRange(0, 1);
        }
    }
}

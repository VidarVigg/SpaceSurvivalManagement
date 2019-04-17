using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectPowerManager : MonoBehaviour
{
    public int size = 21;
    public GameObject wallPrefab;
    public float spacing = 1.1f;
    public float height = 0.5f;
    GameObject[,] maze;
    public Transform parentPanel;
    public enum Direction
    {
        East,
        North,
        South,
        West,

    }
    public Direction direction;

    Vector2Int pos;
    Stack<Vector2Int> visited;
    public Transform walker;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {

        maze = new GameObject[size, size];

        for (int z = 0; z < size; z++)
        {
            for (int x = 0; x < size; x++)
            {
                var position = new Vector3(x * spacing, height, z * spacing) - halfboard;

                GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity);
                wall.transform.rotation = new Quaternion(0, 45, 0, 0);
                wall.transform.SetParent(parentPanel);
                maze[x, z] = wall;
            }
        }
        GenerateNewMaze();
    }

    Vector3 halfboard
    {
        get
        {

            return new Vector3(size * spacing / 2.0f, 0, size * spacing / 2.0f);
        }
    }

    void Update()
    {
        walker.position = new Vector3(pos.x * spacing, 1f, pos.y * spacing) - halfboard;

    }
    public void SetMaze(int x, int z, bool on)
    {
        GameObject wall = maze[x, z];
        wall.GetComponent<Renderer>().enabled = on;
        wall.GetComponent<Collider2D>().enabled = on;
    }
    public void GenerateNewMaze()
    {
        pos = new Vector2Int(1, 1);
        visited = new Stack<Vector2Int>();
        visited.Push(pos);
        StartCoroutine(TakeRandomStep());
    }
    void Dig()
    {
        SetMaze(pos.x, pos.y, false);
    }
    private IEnumerator TakeRandomStep()
    {
        while (true)
        {
            var goodDir = GoodDirections();

            if (goodDir.Count == 0)
            {
                if (visited.Count == 0)
                {
                    //player.GetComponent<Transform>().position = new Vector3(pos.x * spacing, 1f, pos.y * spacing) - halfboard;
                    //player.GetComponent<Transform>().rotation = Quaternion.identity;
                    break;
                }
                else
                {
                    pos = visited.Pop();
                }
            }
            else
            {
                Step(goodDir[Random.Range(0, goodDir.Count)]);
            }

            yield return new WaitForSeconds(0.05f);
        }
        yield break;
    }

    public void Step(Direction direction)
    {

        if (direction == Direction.East && pos.x < size - 3)
        {
            pos.x++;
            SetMaze(pos.x, pos.y, false);
            pos.x++;
            SetMaze(pos.x, pos.y, false);

        }
        else if (direction == Direction.North && pos.y < size - 3)
        {
            pos.y++;
            SetMaze(pos.x, pos.y, false);
            pos.y++;
            SetMaze(pos.x, pos.y, false);
        }
        else if (direction == Direction.West && pos.x > 2)
        {
            pos.x--;
            SetMaze(pos.x, pos.y, false);
            pos.x--;
            SetMaze(pos.x, pos.y, false);
        }
        else if (direction == Direction.South && pos.y > 2)
        {
            pos.y--;
            SetMaze(pos.x, pos.y, false);
            pos.y--;
            SetMaze(pos.x, pos.y, false);
        }

        visited.Push(pos);
    }
    List<Direction> GoodDirections()
    {
        var directions = new List<Direction>();
        if (pos.x < size - 3 && !Visited(new Vector2Int(pos.x + 2, pos.y)))
        {
            directions.Add(Direction.East);
        }
        if (pos.y < size - 3 && !Visited(new Vector2Int(pos.x, pos.y + 2)))
        {
            directions.Add(Direction.North);
        }
        if (pos.x > 2 && !Visited(new Vector2Int(pos.x - 2, pos.y)))
        {
            directions.Add(Direction.West);
        }
        if (pos.y > 2 && !Visited(new Vector2Int(pos.x, pos.y - 2)))
        {
            directions.Add(Direction.South);
        }
        return directions;
    }

    bool Visited(Vector2Int point)
    {
        return !maze[point.x, point.y].GetComponent<Renderer>().enabled;
    }

}

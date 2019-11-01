using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[ExecuteInEditMode]
public class MapManager : MonoBehaviour
{
    public GameObject tile;
    public int map_index;
    Map currentMap;
    public Node[,] map;

    private void Awake()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void Start()
    {
        currentMap = MapLoader.ReadMap(map_index);
        map = new Node[currentMap.col, currentMap.row];

        for (int i = 0; i < currentMap.col; i++)
        {
            for (int j = 0; j < currentMap.row; j++)
            {
                if (currentMap.mapData[i * currentMap.row + j] == 1)
                {
                    GameObject temp_tile = Instantiate(tile, new Vector3(i, 0, j), Quaternion.identity, this.transform);

                    map[i, j] = new Node(true,i,j,0,temp_tile.transform);
                }
            }
        }
    }


    public List<Node> GetNeighbour(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                int checkX = node.x - x;
                int checkY = node.y - y;
                if (checkX >= 0 && checkX < currentMap.col && checkY>=0 && checkY < currentMap.row)
                {
                    neighbours.Add(map[checkX,checkY]);
                }
            }
        }

        return neighbours;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapManager : MonoBehaviour
{
    public GameObject tile;
    public int map_index;
    Map currentMap;
    public Node[,,] map;

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
        map = new Node[currentMap.x_size, currentMap.y_size,currentMap.z_size];
        for (int z = 0; z < currentMap.z_size; z++)
        {
            for (int y = 0; y < currentMap.y_size; y++)
            {
                for (int x = 0; x < currentMap.x_size; x++)
                {
                    if (currentMap.mapData[x+y*currentMap.x_size+z*currentMap.x_size*currentMap.y_size] == 1)
                    {
                        GameObject temp_tile = Instantiate(tile, new Vector3(x, z, y), Quaternion.identity, this.transform);
                        map[x,y,z] = new Node(true, x, y, z, temp_tile.transform);
                    }

                    else if(currentMap.mapData[x + y * currentMap.x_size + z * currentMap.x_size * currentMap.y_size] == 0)
                    {
                        GameObject temp_tile = Instantiate(tile, new Vector3(x, z, y), Quaternion.identity, this.transform);
                        map[x, y, z] = new Node(false, x, y, z, temp_tile.transform);
                        temp_tile.GetComponent<Renderer>().material.SetColor("_Color", new Color(0, 0, 0, 0.5f));
                    }
                }
            }
        }
        //for (int z = 0; z < currentMap.z_size; z++)
        //{
        //    for (int y = 0; y < currentMap.y_size; y++)
        //    {
        //        for (int x = 0; x < currentMap.x_size; x++)
        //        {
        //            Debug.Log(currentMap.mapData[x + y * currentMap.y_size + z * currentMap.x_size * currentMap.y_size]);
        //        }
        //    }
        //}
    }


    public List<Node> GetNeighbour(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    if (x == 0 && y == 0 && z==0)
                        continue;

                    int checkX = node.x - x;
                    int checkY = node.y - y;
                    int checkZ = node.z - z;
                    if (checkX >= 0 && checkX < currentMap.x_size &&
                        checkY >= 0 && checkY < currentMap.y_size &&
                        checkZ >= 0 && checkZ < currentMap.z_size &&
                        map[checkX,checkY,checkZ].walkable)
                    {
                        neighbours.Add(map[checkX,checkY,checkZ]);
                    }
                }
            }
        }

        return neighbours;
    }
}

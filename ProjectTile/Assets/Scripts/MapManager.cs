using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapManager : MonoBehaviour
{
    public GameObject tile;
    public int map_index;
    Map currentMap;

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

        for (int i = 0; i < currentMap.col; i++)
        {
            for (int j = 0; j < currentMap.row; j++)
            {
                if (currentMap.mapData[i * currentMap.col + j] == 1)
                {
                    Instantiate(tile, new Vector3(i, 0, j), Quaternion.identity, this.transform);
                }
            }
        }
    }
}

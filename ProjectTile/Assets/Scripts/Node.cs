using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public int g_cost;
    public int h_cost;
    public Node parent;

    public int x;
    public int y;
    public int z;
    public Transform tile_transform;

    public Node(bool _walkable, int _x, int _y, int _z,Transform _tile)
    {
        walkable = _walkable;
        g_cost = 0;
        h_cost = 0;
        x = _x;
        y = _y;
        z = _z;
        tile_transform = _tile;
    }
    public int f_cost { get { return g_cost + h_cost; } }
}

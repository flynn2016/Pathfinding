using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public MapManager mapManager;
    public Vector3 startPos;
    public Vector3 endPos;
    public void Find()
    {
        FindPath(mapManager.map, startPos, endPos);
    }

    void FindPath(Node[,] map, Vector3 startPos, Vector3 endPos)
    {
        Node startNode = map[(int)startPos.x, (int)startPos.y];
        Node endNode = map[(int)endPos.x, (int)endPos.y];

        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();
        openList.Add(startNode);

        while(openList.Count > 0)
        {
            Node currentNode = openList[0];
            for (int i = 0; i < openList.Count; i++)
            {
                if(openList[i].f_cost < currentNode.f_cost ||(openList[i].f_cost == currentNode.f_cost&&openList[i].h_cost<currentNode.h_cost))
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if(currentNode.x == endNode.x&& currentNode.y == endNode.y&& currentNode.z == endNode.z)
            {
                RetracePath(startNode, endNode);
                return;
            }

            foreach (Node neighbour in mapManager.GetNeighbour(currentNode))
            {
                if (!neighbour.walkable || closedList.Contains(neighbour))
                {
                    continue;
                }

                int newMoveCost = currentNode.g_cost + GetDistance(currentNode,neighbour);

                if (newMoveCost < neighbour.g_cost || !openList.Contains(neighbour))
                {
                    neighbour.g_cost = newMoveCost;
                    neighbour.h_cost = GetDistance(neighbour,endNode);
                    neighbour.parent = currentNode;

                    if (!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);
                    }
                }
            
            }
        }
    }

    int GetDistance(Node a, Node b)
    {
        int distx = Mathf.Abs(a.x - b.x);
        int disty = Mathf.Abs(a.y - b.y);
        return 14 * Mathf.Min(distx, disty) + 10 * Mathf.Abs(distx - disty);
    }
    
    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        foreach (Node path_tile in path)
        {
            path_tile.tile_transform.GetComponent<Renderer>().material.SetColor("_Color",new Color(0,0,1,1));
        }
    }
}


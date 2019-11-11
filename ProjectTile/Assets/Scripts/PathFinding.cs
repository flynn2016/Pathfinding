using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public MapManager mapManager;
    public Vector3 startPos;
    public Vector3 endPos;
    List<Node> final_path;
    List<Node> search_path = new List<Node>();
    int time_index;   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Find();
        }

        if (search_path != null)
        {
            if (time_index == search_path.Count)
            {
                DrawPath();
                CancelInvoke("search_path");
            }
        }
    }

    public void Find()
    {
        FindPath(mapManager.map, startPos, endPos);
        InvokeRepeating("DrawSearchPath", 0f, 0.05f);
    }

    void DrawPath()
    {
        mapManager.map[(int)startPos.x, (int)startPos.y, (int)startPos.z]
           .tile_transform.GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 0, 0, 1));
        if (final_path != null)
        {
            foreach (Node path_tile in final_path)
            {
                path_tile.tile_transform.GetComponent<Renderer>().material.SetColor("_Color", new Color(1f, 0f, 0, 1));
            }
        }
    }

    void DrawSearchPath()
    {
        search_path[time_index].tile_transform.GetComponent<Renderer>().material.SetColor("_Color", new Color(0, 191 / 255f, 1, 0.5f));
        time_index++;
    }

    void FindPath(Node[,,] map, Vector3 _startPos, Vector3 _endPos)
    {
        Node startNode = map[(int)_startPos.x, (int)_startPos.y,(int)_startPos.z];
        Node endNode = map[(int)_endPos.x, (int)_endPos.y,(int)_endPos.z];
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
            search_path.Add(currentNode);
        }
    }

    int GetDistance(Node a, Node b)
    {
        int distx = Mathf.Abs(a.x - b.x);
        int disty = Mathf.Abs(a.y - b.y);
        int distz = Mathf.Abs(a.z - b.z);
        if (distx > disty && disty > distz)
        {
            return 17 * distz + 14 * (disty - distz) + 10 * (distx - disty);
        }
        else if(distx>distz && distz > disty)
        {
            return 17 * disty + 14 * (distz - disty) + 10 * (distx - distz);
        }

        else if (disty > distx && distx > distz)
        {
            return 17 * distz + 14 * (distx - distz) + 10 * (disty - distx);
        }

        else if (disty > distz && distz > distx)
        {
            return 17 * distx + 14 * (distz - distx) + 10 * (disty - distz);
        }

        else if (distz > distx && distx > disty)
        {
            return 17 * disty + 14 * (distx - disty) + 10 * (distz - distx);
        }
        else
        {
            return 17 * distx + 14 * (disty - distx) + 10 * (distz - disty);
        }
    }
    
    void RetracePath(Node startNode, Node endNode)
    {
        final_path = new List<Node>();
        Node currentNode = endNode;
        while (currentNode != startNode)
        {
            final_path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        final_path.Reverse();
    }
}


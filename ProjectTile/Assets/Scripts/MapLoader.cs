using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class MapLoader
{   
    public static Map ReadMap(int map_index)
    {
        int col;
        int row;
        int counter = 0;
        string temp;
        string path = "Assets/Maps/Map_"+map_index+".txt";
        StreamReader reader = new StreamReader(path);

        if(reader.Peek() == '/')
        {
            reader.ReadLine();
        }

        temp = reader.ReadLine();
        row = int.Parse(temp.Split()[0]);
        col = int.Parse(temp.Split()[1]);
        Map map = new Map(col, row);

        while ((temp = reader.ReadLine()) != null)
        {
            if (reader.Peek() != '/')
            {
                string[] temp_string = temp.Split();
                for (int i = 0; i < map.row; i++)
                {
                    map.mapData[counter * row + i] = int.Parse(temp_string[i]);
                }
            }
            counter++;
        }

        reader.Close();
        return map;
    }
}

public struct Map
{
	public int col;
	public int row;
	public int[] mapData;

    public Map(int _col, int _row)
    {
        col = _col;
        row = _row;
        mapData = new int[_col * _row];
    }
}



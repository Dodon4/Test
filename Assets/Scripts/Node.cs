using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public int id;
    public GameObject tile;
    public GameObject square;
    public bool isEmpty = true;
    public Node(GameObject _tile, int _id)
    {
        tile = _tile;
        id = _id;
    }
}

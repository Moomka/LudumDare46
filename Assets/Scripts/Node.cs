using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int gridX;
    public int gridY;
    public Vector3 worldPosition;
    public bool walkable;
    public Node parrent;
    public int costFromStart;
    public int costToEnd;
    public int costFull {
        get{ return costToEnd + costFromStart; }
    }
    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY){
        walkable = _walkable;
        gridX = _gridX;
        gridY = _gridY;
        worldPosition = _worldPos;
    }

}

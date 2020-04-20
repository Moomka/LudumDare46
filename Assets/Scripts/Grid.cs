using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    Vector2Int gridSize;

    LayerMask unwalkableMask;
    LayerMask walkableMask;

    [SerializeField]
    public Node [,] grid;

    void Awake(){
        CreateGrid();
    }

    void CreateGrid(){
        unwalkableMask = LayerMask.GetMask("Unwalkable");
        walkableMask = LayerMask.GetMask("Walkable");
        grid = new Node[gridSize.x, gridSize.y];
        
        for (int x=0; x<gridSize.x; x++)
            for (int y = 0; y < gridSize.y; y++){
                    grid[x,y] = new Node(Physics.CheckSphere(new Vector3(x, 0.5f, -y), .4f, walkableMask), new Vector3 (x, 0, -y), x, y);
                if (Physics.CheckSphere(new Vector3(x, 1f, -y), .4f, unwalkableMask))
                {
                    grid[x, y].walkable = false;
                }
        }
    }



    public List <Node> GetNeighbours(Node node){
        List <Node> neighbours = new List <Node>();
        for (int x = -1; x <= 1; x++)
            for (int y = -1; y <= 1; y++){
                if (x == 0 && y == 0) continue;
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if (checkX >= 0 && checkY >= 0 && checkX < gridSize.x && checkY < gridSize.y)
                    neighbours.Add(grid[checkX,checkY]);
            }
            return neighbours;
    }

    public int GetDistance(Node start, Node end){
        int xDelta = Mathf.Abs(end.gridX - start.gridX);
        int yDelta = Mathf.Abs(end.gridY - start.gridY);
        if (xDelta > yDelta) 
        return 14 * yDelta + 10 * (xDelta - yDelta);
        return 14 * xDelta + 10 * (yDelta - xDelta);
    }



    public Node GridPosToNode(Vector2Int pos){
        if (pos.x >= 0 && pos.x < gridSize.x && Mathf.Abs(pos.y) >= 0 && Mathf.Abs(pos.y) < gridSize.y)
            return grid[pos.x, Mathf.Abs(pos.y)];
            return null;
    }
    public Node GridPosToNode(int x, int y){
        if (x >= 0 && x < gridSize.x && -y >= 0 && -y < gridSize.y)
            return grid[x, -y];
        return null;
    }

    public Node WorldPosToNode(Vector3 pos){
        Vector2Int posInInt;
        posInInt = new Vector2Int(Mathf.RoundToInt(pos.x),Mathf.RoundToInt(Mathf.Abs(pos.z)));

        if (posInInt.x >= 0 && posInInt.x < gridSize.x && posInInt.y >= 0 && posInInt.y < gridSize.y)
            return grid[posInInt.x, posInInt.y];
        else return null;
    }
}

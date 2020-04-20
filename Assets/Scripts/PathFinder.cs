using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    Node startNode;
    Node endNode;

    Grid grid;


    void Awake(){
        grid = GetComponent<Grid>();
    }

    void AStar(Vector2Int startPos, Vector2Int targetPos){
        List <Node> openNodes = new List<Node>();
        HashSet <Node> closedNodes = new HashSet<Node>();
        startNode = grid.GridPosToNode(startPos);
        endNode = grid.GridPosToNode(targetPos);


        openNodes.Add(startNode);

        while (openNodes.Count > 0){
            Node currentNode = openNodes[0];

            foreach (Node node in openNodes)
                if (node.costFull < currentNode.costFull || (node.costFull == currentNode.costFull && node.costToEnd < currentNode.costToEnd)) currentNode = node;

            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

            if (currentNode == endNode) {
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode)){
                if (!neighbour.walkable || closedNodes.Contains(neighbour)) continue;
                
                int potentialCost = currentNode.costFromStart + grid.GetDistance(currentNode, neighbour);
                if (potentialCost < neighbour.costFromStart || !openNodes.Contains(neighbour)) {
                    neighbour.costFromStart = potentialCost;
                    neighbour.costToEnd = grid.GetDistance(neighbour, grid.GridPosToNode(targetPos));
                    neighbour.parrent = currentNode;
                    if (!openNodes.Contains(neighbour)) openNodes.Add(neighbour);
                } 
            }
        }
    }

    public List<Node> GetPath(Node startNode, Node endNode){
        if (endNode == null || endNode.walkable == false) return new List<Node>();
        AStar(new Vector2Int(startNode.gridX,startNode.gridY), new Vector2Int(endNode.gridX,endNode.gridY));
        List<Node> _path = new List<Node>();
        _path.Add(endNode);
        Node currentNode = endNode;
        int i = 0;
        while (currentNode != startNode) {
            _path.Add(currentNode.parrent);
            currentNode = currentNode.parrent;
            i++;
        }
        _path.Reverse();
        return _path;
    }
}



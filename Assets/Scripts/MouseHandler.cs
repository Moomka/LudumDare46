using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    GameObject mainController;
    public Camera mainCamera;
    public GameObject selectedObject;
    public GameObject objectUnderCursor;
    Ray ray;
    public LayerMask walkableMask;


    Movable player;
    Grid grid;
    RaycastHit hit;


    private void Awake()
    {
        mainController = GameObject.FindWithTag("MainController");
        walkableMask = LayerMask.GetMask("Walkable");
        player = GameObject.FindWithTag("Player").GetComponent<Movable>();
        grid = mainController.GetComponent<Grid>();
    }
    void Update(){
        ray = mainCamera.ScreenPointToRay(Input.mousePosition); 
        if (Physics.Raycast(ray, out hit, 100f, walkableMask)){
            objectUnderCursor = hit.transform.gameObject;
        }
        else objectUnderCursor = null;

        if (Input.GetMouseButtonDown(0))
        {
            if (objectUnderCursor == null) Debug.LogError("Wrong Way!");
            else if (objectUnderCursor.layer == 8)
                player.targetNode = grid.GridPosToNode(convertCursorToNodePos(hit.point));
        }
    }

    Vector2Int convertCursorToNodePos(Vector3 cursorPos)
    {
        if (hit.collider != null)
        {
            return new Vector2Int(Mathf.RoundToInt(hit.point.x), Mathf.RoundToInt(-hit.point.z));
        }
            
        return Vector2Int.zero;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    Timer timer;

    GameObject mainController;
    Animator animator;
    Grid grid;
    PathFinder pathfinder;
    MouseHandler mouseHandler;
    public GameObject targetSprite;
    public GameObject pathDot;
    public Node playerPos;

    public Node stayingNode;
    public Node targetNode;
    Vector2Int deltaMove = Vector2Int.zero;
    bool makeTurn;
    List<Node> path;
    List<GameObject> points = new List<GameObject>();
    public Vector2Int MobTarger;
    public int SearchRadius = 5;
    GameObject player;
    [SerializeField]
    public bool isPatrol = false;
    [SerializeField]
    public List<Vector2Int> patrolPoint;


    private void Awake()
    {
        mainController = GameObject.FindWithTag("MainController");
        animator = gameObject.GetComponent<Animator>();
        grid = mainController.GetComponent<Grid>();
        pathfinder = mainController.GetComponent<PathFinder>();
        mouseHandler = mainController.GetComponent<MouseHandler>();
        player = GameObject.FindWithTag("Player");
    }

    void Start()
    {
        timer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
        stayingNode = grid.WorldPosToNode(transform.position);
        targetNode = stayingNode;
        playerPos = grid.WorldPosToNode(player.transform.position);
        StartCoroutine("Turn");
        if (targetSprite)
        {
            points.Add(Instantiate(targetSprite));
            points[0].SetActive(false);
        }
    }

    void Update()
    {
        playerPos = grid.WorldPosToNode(player.transform.position);
        //for player
        if (gameObject.tag == "Player")
        {
            if (targetNode != null)
            {
                path = pathfinder.GetPath(stayingNode, targetNode);
                ShowTrace(path);
            }
            else targetNode = grid.WorldPosToNode(transform.position);

            if (makeTurn && path != null)
            {
                if (path.Count > 1)
                {

                    stayingNode = path[1];
                    deltaMove = new Vector2Int(path[1].gridX - path[0].gridX, path[1].gridY - path[0].gridY);
                    animator.SetInteger("X", deltaMove.x);
                    animator.SetInteger("Y", -deltaMove.y);
                    path.Remove(path[0]);
                    ShowTrace(path);
                }
            }

        }

        //for mobs
        if (gameObject.tag == "Mob")
        {
            path = pathfinder.GetPath(stayingNode, playerPos);
            if (path.Count < SearchRadius)
            {
                targetNode = playerPos;
                path.RemoveAt(path.Count - 1);
                ShowTrace(path);
            }
            else if (isPatrol)
            {
                
                if (stayingNode == grid.GridPosToNode(patrolPoint[1]))
                {
                    targetNode = grid.GridPosToNode(patrolPoint[0]);
                }
                else if (stayingNode == grid.GridPosToNode(patrolPoint[0]))
                {
                    targetNode = grid.GridPosToNode(patrolPoint[1]);
                }
                path = pathfinder.GetPath(stayingNode, targetNode);
            }
            else
            {
                targetNode = grid.GridPosToNode(MobTarger.x, MobTarger.y);
                path = pathfinder.GetPath(stayingNode, targetNode);
            }

            if (makeTurn && path != null)
            {
                if (path.Count > 1)
                {

                    stayingNode = path[1];
                    deltaMove = new Vector2Int(path[1].gridX - path[0].gridX, path[1].gridY - path[0].gridY);
                    animator.SetInteger("X", deltaMove.x);
                    animator.SetInteger("Y", -deltaMove.y);
                    path.Remove(path[0]);
                }
            }
         }

        makeTurn = false;
    }

    IEnumerator Turn()
    {
        while (true)
        {
            while (timer.pause)
                yield return null;

            if (targetNode != null)
                if (targetNode.gridX < stayingNode.gridX) gameObject.GetComponent<SpriteRenderer>().flipX = true;
                else gameObject.GetComponent<SpriteRenderer>().flipX = false;
            makeTurn = true;
            animator.Rebind();
            if (stayingNode != null)
                transform.parent.position = new Vector3(stayingNode.gridX, transform.position.y, -stayingNode.gridY);
            if (gameObject.tag == "Mob")
            {
                if (grid.GetDistance(stayingNode, playerPos) <= 14)
                {
                    deltaMove = new Vector2Int(playerPos.gridX - stayingNode.gridX, playerPos.gridY - stayingNode.gridY);
                    animator.SetInteger("AttackX", deltaMove.x);
                    animator.SetInteger("AttackY", -deltaMove.y);
                    player.SendMessage("TakeDamage", 1f);
                }
                else
                {
                    animator.SetInteger("AttackX", 0);
                    animator.SetInteger("AttackY", 0);
                }
            }
            yield return new WaitForSeconds(timer.BPM / timer.speed);
        }
    }

    void ShowTrace(List<Node> _path)
    {
        HideTrace();
        if (points.Count < _path.Count)
        {
            int needPoints = _path.Count - points.Count;
            for (int i = 1; i <= needPoints; i++)
            {
                points.Add(Instantiate(pathDot));
            }
        }
        for (int i = 2; i < path.Count; i++)
        {
            points[i].transform.position = new Vector3(path[i - 1].gridX, 0f, -path[i - 1].gridY);
            points[i].SetActive(true);
        }
        if (_path.Count > 1)
        {
            points[0].transform.position = new Vector3(path[path.Count - 1].gridX, 0f, -path[path.Count - 1].gridY);
            points[0].SetActive(true);
        }
    }

    public void HideTrace()
    {
        foreach (GameObject point in points)
        {
            point.SetActive(false);
        }
    }
}



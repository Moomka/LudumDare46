using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Trigger : MonoBehaviour
{
    GameObject mainController;
    Timer timer;
    Text textField;
    public List<GameObject> objectsToInterract;
    public enum TriggeredBy
    {
        Player = 0,
        Enemy = 1
    }
    public TriggeredBy trigger;
    string[] tags = new string [2] { "Player", "Mob" };

    public string cutScene;
    // Start is called before the first frame update
    void Awake()
    {
        if (transform.parent != null) 
            objectsToInterract = transform.parent.GetComponent<Trigger>().objectsToInterract;
        mainController = GameObject.FindWithTag("MainController");
        timer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
        textField = GameObject.FindWithTag("TextField").GetComponent<Text>();
        objectsToInterract[5].GetComponent<Animator>().speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tags[(int)trigger])
        {
            playCutScene(cutScene);
            gameObject.SetActive(false);
        }

    }


    void playCutScene (string name)
    {
        switch (name){
            case "HideField":
                textField.text = "";
                break;
            case "Intro":
                timer.pause = true;
                textField.text = "Wait, who is this? He are not like my brothers. I'd better hide. \n /Hide behind the crates. You can plan steps and give orders during a pause. \n Press space to (un)pause./";
                objectsToInterract[0].GetComponent<Movable>().MobTarger = new Vector2Int(152, -50);   //Start bandit
                objectsToInterract[1].SetActive(true);  //Show arrows
                objectsToInterract[2].SetActive(true);  //Show arrows
                objectsToInterract[3].GetComponent<Image>().enabled = true;  //show the Timer
                break;
            case "HideArrows":
                objectsToInterract[1].SetActive(false); //HideArrows
                objectsToInterract[2].SetActive(false); //HideArrows
                textField.text = "All creatures make a move every time the timer in the lower right corner of screen runs out. I tried to keep it to the beat :)";
                objectsToInterract[4].SetActive(false);
                break;
            case "HidePauseText":
                textField.text = "";
                break;
            case "DestroyEnemy":
                Destroy(objectsToInterract[0],1f);
                break;
            case "OMGBlood":
                textField.text = "Oh my glob, what happened here? Probably these people came to pick up IT. I have to get to IT before them. \n I know a short way.";
                objectsToInterract[6].SetActive(true);
                break;
            case "Seesam":
                textField.text = "It seems this place. Sisam, open up!";
                objectsToInterract[6].SetActive(false);
                objectsToInterract[5].GetComponent<Animator>().speed = 1;
                break;
            case "WrongWay":
                textField.text = "Not this way!";
                break;
            case "HideSisam":
                textField.text = "";
                break;
            case "PatrolAlert":
                textField.text = "Oh no. One of these guys is patrolling the cave. Need to go unnoticed! /Use pause if you need/";
                break;
            case "AlertHide":
                textField.text = "";
                break;
            case "End":
                textField.text = "Finally... I found IT";
                break;
            case "LoadNextScene":
                SceneManager.LoadScene("Intro", LoadSceneMode.Single);
                break;
            default:
                break;
        }
    }
}

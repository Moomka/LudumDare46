using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{
    float speed = .5f;
    public GameObject skipText;
    public List<Sprite> frames;
    public Text BackText1;
    public Text BackText2;
    public Text FrontText;
    string[] subtitles = new string[3]
    {
        "Hood Guy: I finally found IT! Now I can hide it in a safe place.",
        "Thing: Hey, have you even washed your hands? Viruses around!",
        "Thing: And to hell with you. Get me out of here soon, and I'll help you. \n Probably."
    };
    float dark = 0;
    public Image image;
    int frame = 0;
    bool creating = true;
    bool deleting = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (creating && !deleting)
        {
            dark = Mathf.Clamp(dark + Time.deltaTime * speed, 0, 1);
            image.color = new Color(dark, dark, dark);
            if (dark == 1)
            {
                skipText.SetActive(true);
                creating = false;
                BackText1.text = subtitles[frame];
                BackText2.text = subtitles[frame];
                FrontText.text = subtitles[frame];
            }
        }

        if (deleting)
        {
            skipText.SetActive(false);
            dark = Mathf.Clamp(dark - Time.deltaTime * speed, 0, 1);
            image.color = new Color(dark, dark, dark);
            if (dark == 0)
            {
                deleting = false;
                image.GetComponent<Image>().sprite = frames[frame];
                image.color = Color.black;
                creating = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !creating)
        {
            Debug.Log(frame);
            deleting = true;
            if (frame < frames.Count - 1) frame++;
            else { SceneManager.LoadScene("Level1", LoadSceneMode.Single); }

        }

        
    }
}

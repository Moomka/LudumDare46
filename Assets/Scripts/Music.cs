using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    bool musicIsPlaying = true;
    AudioSource source;
    public Sprite enabled;
    public Sprite disabled;
    public Button button;
    public Button exitButton;
    Timer timer;
    // Start is called before the first frame update
    void Awake()
    {
        timer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer.pause)
        {
            button.GetComponent<Image>().enabled = true;
            exitButton.GetComponent<Image>().enabled = true;
        }
        else
        {
            button.GetComponent<Image>().enabled = false;
            exitButton.GetComponent<Image>().enabled = false;
        }
    }

    public void Click()
    {
        if (musicIsPlaying)
        {
            source.Pause();
            button.GetComponent<Image>().sprite = disabled;
        }
        else
        {
            source.Play();
            button.GetComponent<Image>().sprite = enabled;
        }
        musicIsPlaying = !musicIsPlaying;
    }
    public void Exit()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float tempSpeed;
    public float speed = 1;
    public float BPM = 1.1428f; //STAYING ALIVE! 1.15384f
    public Animator timerAnimator;
    public GameObject pauseSprite;

    public bool pause = false;
    void Start()
    {
        timerAnimator = gameObject.GetComponent<Animator>();
        StartCoroutine("Turn");
        tempSpeed = speed;
    }


    IEnumerator Turn()
    {
        timerAnimator.speed = speed;
        while (true)
        {
            while (pause)
            {
                timerAnimator.enabled = false;
                yield return null;
                timerAnimator.enabled = true;
                
            }

            timerAnimator.Rebind();
            yield return new WaitForSeconds(BPM/speed);

        }
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
        }
        speed = pause ? 0 : 1;
        pauseSprite.SetActive(pause);
    }
}

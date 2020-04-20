using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;
    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 delta = new Vector3(player.transform.position.x - gameObject.transform.position.x, 0f, player.transform.position.z - gameObject.transform.position.z);
        gameObject.transform.position = (Vector3.Lerp(gameObject.transform.position, gameObject.transform.position + delta, Time.deltaTime));
    }
}

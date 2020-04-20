using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    Renderer renderer;
    public Shader dissolveShader;
    GameObject player;
    Grid grid;
    float intensity;

    bool playerIsNear, playerIsInside, PlayerFarAway;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.shader = dissolveShader;
        player = GameObject.FindWithTag("Player");
        grid = GameObject.FindWithTag("MainController").GetComponent<Grid>();
        intensity = 0;
    }

    private void FixedUpdate()
    {
        float distance = GetDistance(player.transform.position, gameObject.transform.position);
        if (gameObject.transform.position.z < player.transform.position.z && gameObject.transform.position.z > player.transform.position.z-2)
        {
            if (distance < 2)
            {
                intensity = Mathf.Clamp(intensity += Time.deltaTime, 0, 0.8f);
            }
            else if (distance < 3)
            {
                intensity = Mathf.Clamp(intensity += Time.deltaTime, 0, 0.5f);
            }
            else
            {
                intensity = Mathf.Clamp(intensity -= Time.deltaTime, 0, 1);
            }
        }
        else
        {
            intensity = Mathf.Clamp(intensity -= Time.deltaTime, 0, 1);
        }


        renderer.material.SetFloat("Vector1_62F4E3F1", intensity);
    }

    float GetDistance(Vector3 begin, Vector3 end)
    {
        float xDelta = Mathf.Abs(end.x - begin.x);
        float yDelta = Mathf.Abs(end.y - begin.y);
        if (xDelta > yDelta)
            return 1.4f * yDelta + 1f * (xDelta - yDelta);
        return 1.4f * xDelta + 1f * (yDelta - xDelta);
    }
}

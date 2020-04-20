using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public float HealthPoint = 10;
    public HPBar HealthBar;
    public GameObject grave;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame

    void TakeDamage(float damage)
    {
        HealthPoint -= damage;
        HealthBar.RefreshBar(damage);
        if (HealthPoint <= 0)
        {
            if (gameObject.GetComponent<Movable>() != null) gameObject.GetComponent<Movable>().HideTrace();
            grave.SetActive(true);
            grave.transform.parent = gameObject.transform.parent;
            gameObject.SetActive(false);
        }
    }
}

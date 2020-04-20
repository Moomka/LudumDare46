using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public List<GameObject> cells;
    float cellWidth = 0.08f;
    float maxHP;
    public float currentHP;
    public GameObject cell;
    public GameObject leftBorder;
    public GameObject rightBorder;
    public GameObject central;
    // Start is called before the first frame update
    void Awake()
    {
        maxHP = gameObject.GetComponentInParent<Damagable>().HealthPoint;
        currentHP = maxHP;
        cells.Add(cell);
        leftBorder.transform.localPosition = new Vector3(-cellWidth * (maxHP / 2) + cellWidth/2, 0f, 0f);
        rightBorder.transform.localPosition = new Vector3(cellWidth * (maxHP / 2) - cellWidth/2, 0f, 0f);
        central.transform.localScale = new Vector3(maxHP-1, 1f, 1f);
        for (int i = 1; i < maxHP; i++)
        {
            cells.Add(Instantiate(cell));
        }

        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].transform.parent = gameObject.transform;
            cells[i].transform.localPosition = new Vector3((i*cellWidth) - (maxHP/2 * cellWidth - cellWidth/2), 0f, 0f);
            cells[i].transform.rotation = cell.transform.rotation;
        }
    }

    public void RefreshBar(float damage)
    {
        currentHP -= damage;
        if (currentHP < maxHP)
        {
            for (int i = Mathf.FloorToInt(currentHP); i < maxHP; i++)
            {
                cells[i].SetActive(false);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryGrass : MonoBehaviour
{
    public bool onFire = false;
    public List<GameObject> neighbours;
    LayerMask fireableMask;
    LayerMask damagableObjects;
    Timer timer;
    ParticleSystem fire;
    int destroyDelay = 2;
    // Start is called before the first frame update
    void Start()
    {
        fire = gameObject.GetComponent<ParticleSystem>();
        fire.Stop();
        timer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
        fireableMask = LayerMask.GetMask("Fireable");

        Collider[] neighboursColliders = Physics.OverlapSphere(transform.position, 1f, fireableMask);
        int i = 0;
        while (i < neighboursColliders.Length)
        {
            neighbours.Add(neighboursColliders[i].gameObject);
            i++;
        }
        neighbours.Remove(gameObject);
        StartCoroutine("Turn");
    }

    IEnumerator Turn()
    {
        while (true)
        {
            while (timer.pause)
                yield return null;


            if (onFire)
            {
                fire.Play();

                Collider[] firingColliders = Physics.OverlapBox(transform.position, new Vector3(0.5f, 5f, 0.5f), Quaternion.identity);
                foreach(Collider collider in firingColliders)
                {
                    if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Mob"){
                        collider.gameObject.SendMessage("TakeDamage", 5f);
                    }
                }

                foreach (GameObject neighbour in neighbours)
                {
                    if (neighbour != null)
                        neighbour.SendMessage("SetFire", true);

                }
                if (destroyDelay <= 0) Destroy(gameObject);
                else destroyDelay--;
            }
            yield return new WaitForSeconds(timer.BPM / timer.speed);

        }
    }

    void SetFire(bool _onFire)
    {
        onFire = _onFire;
    }
}

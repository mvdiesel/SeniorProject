using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour
{
    bool canAttack = false;
    float timeLeft = 1;
    float range = 10f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        Attack(CheckForEnemies(this.transform.position, range));
    }
    private Collider[] CheckForEnemies(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Enemy") && canAttack == false)
            {
                canAttack = true;
                Debug.Log(hitColliders[0].tag);

            }
        }

        return hitColliders;
    }
    public void Attack(Collider[] target)
    {
        if (canAttack)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                //damage
                //  Debug.Log("vur  -    "+target.gameObject.GetComponent<Enemy>().health);
                for (int i = 0; i < target.Length; i++)
                {
                    if (target[i].gameObject.CompareTag("Enemy"))
                    {

                        if (target[i].gameObject.GetComponent<Enemy>().type == 0)
                        {//infantry
                            Debug.Log("hit infantry");
                            target[i].gameObject.GetComponent<Enemy>().InflictDamage(5);
                            break;
                        }
                        else if (target[i].gameObject.GetComponent<Enemy>().type == 1)
                        {//ranged
                            target[i].gameObject.GetComponent<Enemy>().InflictDamage(8);
                            break;
                        }
                        else if (target[i].gameObject.GetComponent<Enemy>().type == 2)
                        {//cavalry
                            Debug.Log("hit cavalry");
                            target[i].gameObject.GetComponent<Enemy>().InflictDamage(2);
                            break;
                        }
                    }
                }
                canAttack = false;
                timeLeft = 1;



            }
        }
    }
}

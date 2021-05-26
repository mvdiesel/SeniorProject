using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCavalry : MonoBehaviour
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
            if (hitCollider.gameObject.CompareTag("Unit") && canAttack == false)
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
                //  Debug.Log("vur  -    "+target.gameObject.GetComponent<Unit>().health);
                for (int i = 0; i < target.Length; i++)
                {
                    if (target[i].gameObject.CompareTag("Unit"))
                    {

                        if (target[i].gameObject.GetComponent<Unit>().type == 0)
                        {//infantry
                            
                            target[i].gameObject.GetComponent<Unit>().inflictDamage(2);
                            break;
                        }
                        else if (target[i].gameObject.GetComponent<Unit>().type == 1)
                        {//ranged
                            target[i].gameObject.GetComponent<Unit>().inflictDamage(8);
                            break;
                        }
                        else if (target[i].gameObject.GetComponent<Unit>().type == 2)
                        {//cavalry
                            
                            target[i].gameObject.GetComponent<Unit>().inflictDamage(5);
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

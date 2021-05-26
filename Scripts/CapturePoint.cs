using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : MonoBehaviour
{
    private float timer = 20;
    private float range = 100f;
    private bool takenByComputer = false;
    private bool takenByPlayer = false;
    private bool neutral = false;

    public void CheckForCollision(Vector3 center){
        Collider[] hitColliders = Physics.OverlapSphere(center, range);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Unit"))
            {
                takenByPlayer = true;
                
            }
            else if(hitCollider.gameObject.CompareTag("Enemy")){
                takenByComputer = true;
            }
        }
        if((takenByComputer&&takenByPlayer)||(!takenByComputer&&!takenByPlayer))
            neutral = true;
        else
            neutral = false;
        
        
    }
    void Start()
    {
        
    }
    public void Timer(){
        if(neutral){
            timer = 20;
        }
        else if(timer>-1){
            timer -= Time.deltaTime;
        }
    }
    // Update is called once per frame
    public void CheckForWinner(){
        
        if(timer<=0){
            if(takenByPlayer)
                Debug.Log("Player Won");
            else if(takenByComputer)
                Debug.Log("Computer Won");
        }
        //Debug.Log((int)timer);
    }
    void FixedUpdate()
    {
        CheckForCollision(transform.position);
        CheckForWinner();
        if(neutral){
            //Debug.Log("Neutral");
        }
        if(takenByComputer){
            //Debug.Log("Computer");
        }
        if(takenByPlayer){
            //Debug.Log("Player");
        }
        Timer();
        
    }
}

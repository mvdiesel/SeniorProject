using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationScript 
{
    public void CalculateLocations(List<GameObject> SelectedUnits, Vector3 location){
        int leader = (int)Mathf.Floor(SelectedUnits.Count/2);
        GameObject pos = new GameObject();
        pos.transform.position = location;

        SelectedUnits[leader].GetComponent<Unit>().setPosition(pos);
        for(int i = leader+1; i<SelectedUnits.Count;i++){
            SelectedUnits[i].GetComponent<Unit>().setTransform(SelectedUnits[i-1].gameObject.transform.Find("Navigation").transform.Find("Right").gameObject.transform);
        }
        for(int i = 0; i<leader;i++){
            SelectedUnits[i].GetComponent<Unit>().setTransform(SelectedUnits[i+1].gameObject.transform.Find("Navigation").transform.Find("Left").gameObject.transform);
        }
    }
    public void CalculateEnemyLocations(List<GameObject> SelectedUnits, Vector3 location){
        int leader = (int)Mathf.Floor(SelectedUnits.Count/2);
        GameObject pos = new GameObject();
        pos.transform.position = location;

        SelectedUnits[leader].GetComponent<Enemy>().setPosition(pos);
        for(int i = leader+1; i<SelectedUnits.Count;i++){
            SelectedUnits[i].GetComponent<Enemy>().setTransform(SelectedUnits[i-1].gameObject.transform.Find("Navigation").transform.Find("Right").gameObject.transform);
        }
        for(int i = 0; i<leader;i++){
            SelectedUnits[i].GetComponent<Enemy>().setTransform(SelectedUnits[i+1].gameObject.transform.Find("Navigation").transform.Find("Left").gameObject.transform);
        }
    }
    public void MoveFlockAtRest(List<List<GameObject>> flock, Vector3 location){
        
        

        int firstUnitLeader = (int)Mathf.Floor(flock[0].Count/2);
        int secondaryUnitLeader = (int)Mathf.Floor(flock[1].Count/2);
        int thirdUnitLeader = (int)Mathf.Floor(flock[2].Count/2);

        GameObject pos = new GameObject();
        pos.transform.position = location;
        flock[0][firstUnitLeader].GetComponent<Enemy>().setPosition(pos);
        flock[1][secondaryUnitLeader].GetComponent<Enemy>().setTransform(flock[0][firstUnitLeader].gameObject.transform.Find("Navigation").transform.Find("Back").gameObject.transform);
        flock[2][thirdUnitLeader].GetComponent<Enemy>().setTransform(flock[1][secondaryUnitLeader].gameObject.transform.Find("Navigation").transform.Find("Back").gameObject.transform);

    
        for(int i = 0; i<flock.Count;i++){
            int leader = (int)Mathf.Floor(flock[i].Count/2);
            for (int k = leader+1; k < flock[i].Count; k++)
            {
                flock[i][k].GetComponent<Enemy>().setTransform(flock[i][k-1].gameObject.transform.Find("Navigation").transform.Find("Right").gameObject.transform);     
            }
            for(int k = 0; k<leader;k++){
                flock[i][k].GetComponent<Enemy>().setTransform(flock[i][k+1].gameObject.transform.Find("Navigation").transform.Find("Left").gameObject.transform);
            }
        }

        /*for(int i = firstUnitLeader+1; i<flock[0].Count;i++){
            flock[0][i].GetComponent<Enemy>().setTransform(flock[0][i-1].gameObject.transform.Find("Navigation").transform.Find("Right").gameObject.transform);
        }
        for(int i = 0; i<firstUnitLeader;i++){
            flock[0][i].GetComponent<Enemy>().setTransform(flock[0][i+1].gameObject.transform.Find("Navigation").transform.Find("Left").gameObject.transform);
        }
        for(int i = secondaryUnitLeader+1; i<flock[2].Count;i++){
            flock[2][i].GetComponent<Enemy>().setTransform(flock[2][i-1].gameObject.transform.Find("Navigation").transform.Find("Right").gameObject.transform);
        }
        for(int i = 0; i<secondaryUnitLeader;i++){
            flock[2][i].GetComponent<Enemy>().setTransform(flock[2][i+1].gameObject.transform.Find("Navigation").transform.Find("Left").gameObject.transform);
        }
        for(int i = thirdUnitLeader+1; i<flock[1].Count;i++){
            flock[1][i].GetComponent<Enemy>().setTransform(flock[1][i-1].gameObject.transform.Find("Navigation").transform.Find("Right").gameObject.transform);
        }
        for(int i = 0; i<thirdUnitLeader;i++){
            flock[1][i].GetComponent<Enemy>().setTransform(flock[1][i+1].gameObject.transform.Find("Navigation").transform.Find("Left").gameObject.transform);
        }*/
    
    }
}
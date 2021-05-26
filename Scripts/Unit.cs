using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
public class Unit : MonoBehaviour {
    Rigidbody rb;
    public static int unitCount=0;
    public int unitId;
    List<Character> characters = new List<Character>();
    private Vector3 worldPosition;
    private GameObject positionObject;
    bool destinationSet = false;
    bool destroyedPositionObject = false;//destroy the position object given by A
    private AIDestinationSetter aiDestinationSetter;
    private string formation = "square";//default formation
    private int numberOfChilds=0;
    private int width = 0;
    public int health = 100;
    private int kill = 75;
    public int type;//0 - infantry, 1 - ranged, 2 - cavalry
    private Transform target;
    public Unit(){
        unitCount++;
        unitId = unitCount;
    }
   
    public void inflictDamage(int damage) {
        health -= damage; 
    }
    public void Destroy() {

        if (health <= kill) {
            Destroy(characters[numberOfChilds - 1].gameObject);
            characters.RemoveAt(numberOfChilds-1);
            numberOfChilds--;
            kill = kill - 25;
        }

    }
    public void DestroyUnit() {
        if (health<=0) {
            Destroy(gameObject);
        }

    }
    private void fillCharacterList() {
         
        foreach(Transform child in this.transform) {
            if(numberOfChilds == 4){break;}
            Character c = child.gameObject.GetComponent<Character>();
            characters.Add(c);
            numberOfChilds++;
        }
        
    }
    private void Start() {
        rb = this.gameObject.GetComponent<Rigidbody>();
        fillCharacterList();
        setWidth();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
    }
    public void setWidth()
    {
        if (formation == "line") {
            width = numberOfChilds; 
        }
        else if (formation == "square") {
            width = (int)Mathf.Ceil(Mathf.Sqrt(numberOfChilds)); 
        }
    }
    public int getWidth() {
        return width;
    }
    public void setPosition(GameObject go) {
        positionObject = go;
        aiDestinationSetter.target = positionObject.transform;
        destinationSet = true;
        destroyedPositionObject = false;
    }
    public void setTransform(Transform t){
        aiDestinationSetter.target = t;

    }
    public Transform getTarget() {
        return aiDestinationSetter.target;
    }
    public string getFormation() {
        return formation;
    }
    public void setFormation(string formation) {
        this.formation = formation;
    }
    public int getNumberOfChilds() {
        return numberOfChilds;
    }
    public Vector3 getPosition() {
        worldPosition = this.transform.position;
        return worldPosition;
    }
    private void Update() {
        destroyDestinationObject();
        Destroy();
        DestroyUnit();
        
    }
    public void destroyDestinationObject()
    {
        //if the given position object is set, and hasnt been destroyed
        //yet, and character has reached the target
        if (!destroyedPositionObject && destinationSet && Vector3.Distance(positionObject.transform.position, transform.position) > Vector3.kEpsilon)
        {
            Destroy(positionObject);
            destroyedPositionObject = true;
        }
    }

}
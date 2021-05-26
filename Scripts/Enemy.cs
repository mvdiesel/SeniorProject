using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    [SerializeField]
    public int flockId;
    bool destinationSet = false;
    bool destroyedPositionObject = false;//destroy the position object given by A
    private GameObject positionObject;
    int kill;
    int numberOfChilds;
    public MeshRenderer rd;
    public int type; //0-infantry 1 - ranged 2 - cavalry
    private Transform target;
    List<Character> characters = new List<Character>();
    private AIDestinationSetter aiDestinationSetter;
    private static int maxFlockSize = 0;


    public void DestroyUnit()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Flasher()
    {
        for (int i = 0; i < 5; i++)
        {
            rd.material.color = Color.red;
            yield return new WaitForSeconds(.1f);
            rd.material.color = Color.grey;
            yield return new WaitForSeconds(.1f);
        }
    }


    public int getMaxFlockSize(){
        return maxFlockSize;
    }
    public int getFlockId(){
        return flockId;
    }
    private void SetFlockSize(){
        if(flockId>maxFlockSize)
            maxFlockSize = flockId;
    }

    void Start()
    {
        SetFlockSize();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        health = 100;
        kill = health - 25;
        numberOfChilds = 0;
        fillCharacterList();
    
    }

    // Update is called once per frame
    void Update()
    {
        Destroy();
        DestroyUnit();
        destroyDestinationObject();
    }
    public void InflictDamage(int damage) {
        health -= damage;
        StartCoroutine(Flasher()); 
    
    }

    public void Destroy()
    {
        int unitCountTemp = numberOfChilds;
        if (health <= kill)
        {
            Destroy(characters[unitCountTemp - 1].gameObject);
            characters.RemoveAt(unitCountTemp - 1);
            unitCountTemp--;
            kill = kill - 25;
        }
        numberOfChilds = unitCountTemp;

    }
    private void fillCharacterList()
    {
        foreach (Transform child in this.transform)
        {
            if(numberOfChilds == 4){break;}
            Character c = child.gameObject.GetComponent<Character>();
            characters.Add(c);
            numberOfChilds++;
        }
    }
    public int getType(){
        return type;
    }
    public void setTransform(Transform t){
        aiDestinationSetter.target = t;

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
    public void setPosition(GameObject go) {
        positionObject = go;
        aiDestinationSetter.target = positionObject.transform;
        destinationSet = true;
        destroyedPositionObject = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationScript : MonoBehaviour
{
    int unitId;
    // Start is called before the first frame update
    Vector3 destination;
    void Start()
    {
        destination = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetDestination(Vector3 _destination){
        destination = _destination;
    }
    public int getUnitId(){
        return unitId;
    }
    public void setUnitId(int _unitId){
        unitId = _unitId;
    }
}

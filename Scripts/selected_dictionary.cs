using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selected_dictionary : MonoBehaviour
{
    public Dictionary<int, GameObject> selectedTable = new Dictionary<int, GameObject>();
    Unit u;
    UnitHandler uHandler;

    private void Start() {
        u=GetComponent<Unit>();
        uHandler = GetComponent<UnitHandler>();
    }
    //int size=0;
    public void addSelected(GameObject go)
    {
        int id = go.GetInstanceID();
        
        if (!(selectedTable.ContainsKey(id)))
        {   
            selectedTable.Add(id, go);
            uHandler.AddUnitSelected(go);
            go.AddComponent<selection_component>();
            Debug.Log("Added " + id + " to selected dict");
            //size++;
        }
    }
    /*public int GetSize(){
        return size;
    }*/
    public void deselect(int id)
    {
        Destroy(selectedTable[id].GetComponent<selection_component>());
        uHandler.RemoveUnitSelected(selectedTable[id]);
        selectedTable.Remove(id);
        //size--;
    }

    public void deselectAll()
    {
        foreach(KeyValuePair<int,GameObject> pair in selectedTable)
        {
            if(pair.Value != null)
            {
                Destroy(selectedTable[pair.Key].GetComponent<selection_component>());
            }
        }
        selectedTable.Clear();
        uHandler.ClearSelected();        //size=0;
    }

}
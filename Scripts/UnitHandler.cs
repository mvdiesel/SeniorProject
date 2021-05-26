using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UnitHandler : MonoBehaviour
{
    List<GameObject> selectedUnits = new List<GameObject>();
    List<List<List<GameObject>>> enemyUnits = new List<List<List<GameObject>>>();
    List<GameObject> allUnits = new List<GameObject>();
    private Vector3 worldPosition;

    private FormationScript fs;
    private Vector3 capturePosition;

    private bool [] states = new bool[3];

    /*public Vector3[,] getPositionGridSquare(int numberOfRows, Vector3 destination)
    {
        Vector3[,] map = new Vector3[(2 * numberOfRows) - 1, (2 * numberOfRows) - 1];
        int center = numberOfRows;
        Vector3 soldierPosition = new Vector3(destination.x - (numberOfRows * 2), 0, destination.z - (numberOfRows) * 2);//position of first soldier
        float xPosFirst = soldierPosition.x;
        float yPosFirst = soldierPosition.z;
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = new Vector3(xPosFirst + (i * 2), 0, yPosFirst + (j * 2));
            }

        }
        return map;
    }
    public int CalculateSelectedWidth()
    {
        int width = 0;
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            Unit u = selectedUnits[i].GetComponent<Unit>();
            if (u.getFormation() == "square")
            {
                width += (int)Mathf.Ceil(Mathf.Sqrt(u.getNumberOfChilds()));
            }
            else if (u.getFormation() == "line")
            {
                width += u.getNumberOfChilds();
            }
        }
        return width;
    }
    public int compareVectorsDiagonality(Vector3 playerP, Vector3 clickedP)
    {
        float tolerance = 8f;
        float dx = playerP.x - clickedP.x;
        float dz = playerP.z - clickedP.z;
        if (Mathf.Abs(dx) > tolerance && Mathf.Abs(dz) > tolerance)//çapraz 
        {
            if ((dx<0&&dz<0)||(dx>0&&dz>0)) { //sol alt sağ üst 
                return 1;
            }
            else if((dx > 0 && dz < 0) || (dx < 0 && dz > 0)) {//sağ alt sol üst 
                return 2;
            }
        }
        else if (Mathf.Abs(dx) > tolerance && Mathf.Abs(dz) < tolerance) {//aşağı yukarı
            return 3; 
        }
        else if (Mathf.Abs(dx) < tolerance && Mathf.Abs(dz) > tolerance) {// 
            return 4; 
        }
        return 0;
    }
    public void SetUnitPositions(Vector3 destination) {
        float x = 0f;
        float y = 0f;
        float radius = 1f;
        float leaderX = destination.x;
        float leaderY = destination.z;
        GameObject leaderGo = new GameObject();
        leaderGo.transform.position = destination;
        selectedUnits[0].GetComponent<Unit>().setPosition(leaderGo);
        for (int i=1; i<selectedUnits.Count;i++) {
            radius = selectedUnits[i].GetComponent<Unit>().getWidth()*1.50f;
            if (compareVectorsDiagonality(destination,selectedUnits[0].GetComponent<Unit>().transform.position)==1) {
                if (i % 2 == 0)
                {
                    x = leaderX - (i * radius);
                    y = leaderY + (i* radius);
                }
                else
                {
                    x = leaderX + (i * radius);
                    y = leaderY - (i * radius);
                }
            }
            else if(compareVectorsDiagonality(destination, selectedUnits[0].GetComponent<Unit>().transform.position) == 2) {
                if (i%2==0) {
                    x = leaderX + (i*radius);
                    y = leaderY + (i * radius);
                }
                else {
                    x = leaderX - (i * radius);
                    y = leaderY - (i * radius);
                }
            }
            else if (compareVectorsDiagonality(destination, selectedUnits[0].GetComponent<Unit>().transform.position) == 4) {
                if (i % 2 == 0)
                {
                    x = leaderX + (i * radius);
                    y = leaderY;
                }
                else
                {
                    x = leaderX - (i * radius);
                    y = leaderY;
                }
            }
            else if (compareVectorsDiagonality(destination, selectedUnits[0].GetComponent<Unit>().transform.position) == 3)
            {
                if (i % 2 == 0)
                {
                    x = leaderX ;
                    y = leaderY + (i * radius);
                }
                else
                {
                    x = leaderX ;
                    y = leaderY -(i * radius);
                }
            }
            GameObject go = new GameObject(); go.transform.position = new Vector3(x, 0, y);
            selectedUnits[i].GetComponent<Unit>().setPosition(go);
        }
    }
    public void CreateDestinationNode()
    {
        GameObject DestinationNode = new GameObject();
    }*/
    public void AddUnitGlobal(GameObject u)
    {
        allUnits.Add(u);
    }
    public void RemoveUnitGlobal(GameObject u)
    {
        allUnits.Remove(u);
    }
    public void AddUnitSelected(GameObject u)
    {
        selectedUnits.Add(u);
    }
    public void RemoveUnitSelected(GameObject u)
    {
        selectedUnits.Remove(u);
    }
    public void ClearSelected()
    {
        selectedUnits.Clear();
    }
    private void Move()
    {
        if (Input.GetMouseButtonUp(1)&&selectedUnits.Count!=0)
        {
            Plane plane = new Plane(Vector3.up, 0);
            float distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                worldPosition = ray.GetPoint(distance);
            }

            fs.CalculateLocations(selectedUnits,worldPosition);
            //SetUnitPositions(worldPosition);
        }
    }
    public void EnemyStates(){
        
    }
    public void LoadEnemyUnits(){
        
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        int flockSize = gos[0].GetComponent<Enemy>().getMaxFlockSize();
        
        for(int i = 0;i<flockSize;i++){
            List<List<GameObject>> flock = new List<List<GameObject>>();
            for(int k = 0; k<3;k++){
                flock.Add(new List<GameObject>());    
                for(int j = 0; j<gos.Length;j++){
                    if(gos[j].GetComponent<Enemy>().getType() == k&&gos[j].GetComponent<Enemy>().getFlockId()-1==i){
                        flock[k].Add(gos[j]);
                    }
                }
            }
            enemyUnits.Add(flock);
        }
    }
    void Start()
    {
        LoadEnemyUnits();
        capturePosition = GameObject.Find("Capture").gameObject.transform.position;
        fs = new FormationScript();
        Debug.Log(capturePosition);
        fs.MoveFlockAtRest(enemyUnits[0],capturePosition);
    }
    void Update()
    {
        Move();
    }


}
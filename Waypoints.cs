using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] points; //[]를 했으니 array가 됨

    void Awake() 
    {
        points = new Transform[transform.childCount]; //childCount는 waypoints의 children 개수를 셈
        //waypoints는 4개의 children이 있고 4개의 transform이 각각 points에 부여
        for (int i = 0; i < points.Length; i++)
        {
            points[i]=transform.GetChild(i); 
        }        
    }
    
    
}

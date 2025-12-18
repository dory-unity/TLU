using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    
    SpriteRenderer rend;
    private Transform GOtransform;
    private Enemy enemy;
    private Transform target; //Enemy가 이동해야하는 target
    private int wavepointIndex = 0;

    
    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0];
        rend = GetComponent<SpriteRenderer>();
        GOtransform = GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World); //시간 함수를 다룰 때 Time.deltaTime이 필요

        if (Vector3.Distance(transform.position, target.position) <= 0.05f) //적당히 찍으면 다음 포인트로
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {        
        wavepointIndex++;
        if (wavepointIndex > Waypoints.points.Length - 1) 
        {
            wavepointIndex = 0; //마지막 포인트까지 오면 다시 돌기
        }        
        target = Waypoints.points[wavepointIndex];

        if(rend != null) //sprite renderer가 있는 경우에 flip
        {
            if (wavepointIndex == 1)
            {                
                rend.flipX = true; //방향 바꾸기
                return;
            }
            if (wavepointIndex == 3)
            {
                rend.flipX = false; //방향 바꾸기
                return;
            }
        }
        else
        {
            if (wavepointIndex == 1)
            {
                Debug.Log("wavepointIndex == 1");
                GOtransform.localScale = new Vector3(GOtransform.localScale.x * -1, GOtransform.localScale.y, GOtransform.localScale.z);
                return;
            }
            if (wavepointIndex == 3)
            {
                GOtransform.localScale = new Vector3(GOtransform.localScale.x * -1, GOtransform.localScale.y, GOtransform.localScale.z);
                return;
            }            
        }

        
    }
}

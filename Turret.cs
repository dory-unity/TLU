using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{

    public Transform target; //공격할 타겟 설정
    private Enemy targetEnemy;
    private Animator anim;

    [Header("Attributes")]
    public float range = 2f;
    public float fireRate = 1f;
    private float slowAmount = 0.0f; //0~1.0f으로 설정. 0.3일 경우, enemy의 속도는 0.7이 됨
    public int sellAmount = 0;
    public string legend;

    private float fireCountdown;   
    private float criticprob;
    private float deleteprob;
    private float warexprob;



    Vector3 currentScale; //turret 좌우 방향전환에 사용

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";
    public GameObject bulletPrefab;
    public GameObject bulletCriticalPrefab;
    public Transform firePoint;
    public Sprite turretImage;
    
    
    void Awake() 
    {
        currentScale = transform.localScale;
    }
    
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.2f); //0.2초마다 repeat
        anim = GetComponent<Animator>();
        fireCountdown = 1f/fireRate;        
        slowAmount = Upgrade.slowAmount; //전사에 적용되는 slow amount
        criticprob = Upgrade.criticprob;
        deleteprob = Upgrade.deleteprob;
        warexprob = Upgrade.warexprob;
        //criticprob = ES3.Load<float>("criticprob",10f);
        //deleteprob = ES3.Load<float>("deleteprob",1.0f);
        //warexprob = ES3.Load<float>("warexprob",10f);
    }
    
    void UpdateTarget()//가장 가까운 target을 찾는 것
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); //GameObject의 array. Enemy 라는 tag가 있는 것들.
        float shortestDistance = Mathf.Infinity; //최초에 enemy가 없을 경우에는 Infinite distance니까 이렇게 설정
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) 
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();         
        }
        else
        { 
            target = null;
        }

        //target 위치에 따라 좌우 방향 전환
        
        if(target == null) //target 이 없으면 null
        return;

        Vector2 dir = target.position - transform.position;
        if(dir.x > 0)
        {   
            currentScale.x = Mathf.Abs(currentScale.x) * 1f; 
            transform.localScale = currentScale;
        }
        else if (dir.x <= 0)
        {                                    
            currentScale.x = Mathf.Abs(currentScale.x) * -1f; 
            transform.localScale = currentScale;
        }       
    }

    
    void Update()
    {
        if(target == null) //target 이 없으면 null
        {
            anim.SetBool("isShooting", false);
            return;
        }
        
        anim.SetBool("isShooting", true);
        fireCountdown -= Time.deltaTime;     

        if(fireCountdown <= 0f)
        {   
            Shoot();
            fireCountdown = 1f / fireRate;            
        }   
    }

    void Shoot()
    {        
        Vector2 dir = target.position - transform.position;
        // 화살 방향 표시
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion arrowAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        
        if(gameObject.CompareTag("Archer")) //궁사 크리티컬
        {            
            int dice = Random.Range(0,100);
            if (dice < criticprob) //critical 확률
            {
                GameObject bulletGO = (GameObject)Instantiate(bulletCriticalPrefab, firePoint.position, arrowAngle);        
                BulletCritical bullet = bulletGO.GetComponent<BulletCritical>();
                Debug.Log("critical arrow shot");
                
                if (bullet != null)
                {
                    bullet.Seek(target);
                }
            }
            else
            {
                GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, arrowAngle);        
                Bullet bullet = bulletGO.GetComponent<Bullet>();
                Debug.Log("critical arrow shot fail");
                if (bullet != null)
                {
                    bullet.Seek(target);
                }
            }
        }
        else if(gameObject.CompareTag("Wizard")) //마법사 궁극
        {            
            float dice = Random.Range(0,100);
            if (dice < deleteprob) //delete궁극 확률
            {
                GameObject bulletGO = (GameObject)Instantiate(bulletCriticalPrefab, firePoint.position, arrowAngle);        
                BulletCritical bullet = bulletGO.GetComponent<BulletCritical>();
                Debug.Log("critical spell");

                if (bullet != null)
                {
                    bullet.Seek(target);
                }
            }
            else
            {
                GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, arrowAngle);        
                Bullet bullet = bulletGO.GetComponent<Bullet>();
                Debug.Log("critical spell fail");
                if (bullet != null)
                {
                    bullet.Seek(target);
                }
            }
        }
        else //전사
        {         
            float dice = Random.Range(0,100);
            if (dice < warexprob) //전사 범위공격 확률
            {
                GameObject bulletGO = (GameObject)Instantiate(bulletCriticalPrefab, firePoint.position, arrowAngle);        
                Bullet bullet = bulletGO.GetComponent<Bullet>();
                if (bullet != null)
                    {
                        bullet.Seek(target);
                    }
                if (gameObject.CompareTag("Warrior")) //전사의 경우 slow 적용
                    {
                        targetEnemy.Slow(slowAmount); //slow는 enemy.cs에 적용
                    }
            }
            else
            {
                GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, arrowAngle);        
                Bullet bullet = bulletGO.GetComponent<Bullet>();
                if (bullet != null)
                    {
                        bullet.Seek(target);
                    }
                if (gameObject.CompareTag("Warrior")) //전사의 경우 slow 적용
                    {
                        targetEnemy.Slow(slowAmount); //slow는 enemy.cs에 적용
                    }                
            }           

        }        
    }


    void OnDrawGizmosSelected() //클릭한 것의 Range 그리기
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);
    }

    
}

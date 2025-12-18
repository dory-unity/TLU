using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCritical : MonoBehaviour
{
    private Transform target;
    public float speed = 3f;
    public float startdamage = 200f;
    public float damage;
    public float explosionRadius = 0f;
    private Vector3 impactOffset;
    public GameObject impactEffect;
    public string soundname;
    private float damageUp;    
    private float archerDamage;
    private float warriorDamage;
    private float wizardDamage;
    private float damageArrow;
    private float damageSpell;
    private float damageSlash;
    private float spelldamage;
    

    public void Seek(Transform _target) //이 함수를 이용해서 다른 script의 transform을 가져옴.
    //여기서는 Turret에서 _target을 가져옴
    {
        target = _target;
    }

    void Awake() 
    {
        
    }
    void Start()
    {
        if(FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play(soundname);
        }        
        damageUp = Upgrade.damageUp;
        archerDamage = Upgrade.archerDamage;
        warriorDamage = Upgrade.warriorDamage;
        wizardDamage = Upgrade.wizardDamage;
        damageArrow = Upgrade.damageArrow;        
        damageSpell = Upgrade.damageSpell;
        damageSlash = Upgrade.damageSlash;
        //damageUp=ES3.Load<float>("damage",1);
        //damageArrow=ES3.Load<float>("damageArrow",1);
        //damageSpell=ES3.Load<float>("damageSpell",1);
        //damageSlash=ES3.Load<float>("damageSlash",1);
        Debug.Log("critical bullet damageUp : " + damageUp);        
        impactOffset = new Vector3 (0, 0, -1.1f);     
        UpgradeDamage(); //damage는 각 무기별 damage로 변환,  upgrade 반영하도록 함   
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;            
        }

        Vector2 dir = target.position - transform.position; //bullet과 target 사이의 dir
        float distanceThisFrame = speed * Time.deltaTime;        

        if (dir.magnitude <= distanceThisFrame) //거의 다 오면 맞은거로 하자
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        
        //2D로 bullet의 각도 (여기선 arrow의 각도)
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion arrowAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = arrowAngle;
    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position + impactOffset , Quaternion.identity);        
        Destroy(effectIns, 0.5f); //2초 후에 임펙트 제거
        if (explosionRadius > 0f)
        {
            Explode();
            Debug.Log("Explode");
        }
        else
        {
            Damage(target);
        }
                
        Destroy(gameObject); //맞으면 bullet을 제거
    }
    void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,explosionRadius);
        
        foreach (Collider2D collider in colliders)
        {            
            if (collider.CompareTag("Enemy")) //Enemy에 collider 설정 확인
            {
                Damage (collider.transform); //damage 중첩됨
                Debug.Log("Explode Enemy");
            }            
        }

    }

    void Damage(Transform enemy) //critical은 법사와 궁사에게만 적용
    {   
        if(gameObject.CompareTag("Arrow"))
        {
            damage = damage * Upgrade.critical;    
            Enemy e = enemy.GetComponent<Enemy>();

            if(e != null)
            {
                e.TakeDamage(damage);
                Debug.Log("Damage : " + damage);
            }        
        }        

        if(gameObject.CompareTag("Spell")) //wizard의 경우, delete 적용
        {            
            spelldamage = damage * Upgrade.deletedam; //법사 궁극 중복 방지를 위해 spelldamage 도입
            
            Enemy e = enemy.GetComponent<Enemy>();

            if(e != null)
            {
                e.TakeDamage(spelldamage);
                Debug.Log("Damage : " + spelldamage);
            }
        }


        
    }

    void UpgradeDamage()
    {
        if (gameObject.CompareTag("Arrow"))
        {
            damage=startdamage * Upgrade.archerUp * archerDamage * damageArrow ; //damageUP은 playerpref
        }
        else if (gameObject.CompareTag("Slash"))
        {
            damage=startdamage * Upgrade.warriorUp * warriorDamage * damageSlash;
        }
        else if (gameObject.CompareTag("Spell"))
        {
            damage=startdamage * Upgrade.wizardUp * wizardDamage * damageSpell;
        }
        else
        {
            Debug.Log("Damage Upgrade Error");
        }
    }



}

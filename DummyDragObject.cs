using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDragObject : MonoBehaviour
{    
    public Transform nodeUIParent;
    private Transform nodeUI;
    private GameObject nodeUIGO;
    private Vector2 startPosition;
    public TurretBlueprint newTurret;
    public bool mergeReady;        
    public string legend;

    [Header ("Optional")]
    public GameObject node;
    BuildManager buildManager;
    private void Start() {
        startPosition = transform.position;
        buildManager = BuildManager.instance;        
        //nodeUI = GameObject.Find("NodeUI").transform.Find("NodeCanvas");
        //nodeUIGO = nodeUI.gameObject;        
        nodeUIParent = GameObject.FindWithTag("NodeUI").transform;
        nodeUI = nodeUIParent.transform.Find("NodeCanvas");
        nodeUIGO = nodeUI.gameObject;
    }

    
    
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (!collision.CompareTag("Node")) //Node가 아니면 return;
        {
            return;
        }                      
        if (!collision.GetComponent<Node>().turret) //merge할 turret이 없으면 return;
        {
            return;
        }

        //collision 한 대상은 node이다.
        node = collision.gameObject;
        float distance = Vector3.Distance(startPosition, node.GetComponent<Transform>().position);
        if (distance < 0.3)
        {
            return; //가까우면 = 나 자신이니까 return;
        }
        Debug.Log("Collision!!!!");

        string thisGameobjectTag = gameObject.tag;
        string collisionTurretTag = node.GetComponent<Node>().turret.tag;        
        string collisionTurretLegend = node.GetComponent<Node>().turret.GetComponent<Turret>().legend;
        
        if( thisGameobjectTag == collisionTurretTag && legend == collisionTurretLegend)
        {            
            buildManager.SelectTurretToMerge(newTurret);
            mergeReady=true;
            nodeUIGO.SetActive(false);  
        }        
    }


    
    private void OnTriggerStay2D(Collider2D collision) 
    {
        if (!collision.CompareTag("Node")) //Node가 아니면 return;
        {
            return;
        }                      
        if (!collision.GetComponent<Node>().turret) //merge할 turret이 없으면 return;
        {
            return;
        }

        //collision 한 대상은 node이다.
        node = collision.gameObject;
        float distance = Vector3.Distance(startPosition, node.GetComponent<Transform>().position);
        if (distance < 0.3)
        {
            return; //가까우면 = 나 자신이니까 return;
        }
        Debug.Log("Collision!!!!");

        string thisGameobjectTag = gameObject.tag;
        string collisionTurretTag = node.GetComponent<Node>().turret.tag;        
        string collisionTurretLegend = node.GetComponent<Node>().turret.GetComponent<Turret>().legend;
        
        if( thisGameobjectTag == collisionTurretTag && legend == collisionTurretLegend)
        {            
            buildManager.SelectTurretToMerge(newTurret);
            mergeReady=true;
        }        
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {        
        buildManager.ResetTurretMerge();
        mergeReady = false;
    }  

    void OnDrawGizmos() //클릭한 것의 Range 그리기
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,0.3f);
    }    
    
}

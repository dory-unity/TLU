using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector2 mousePosition;    
    public TurretBlueprint turretSelf;
    //public static bool mouseButtonReleased;
    
    [Header ("Optional")]
    public GameObject turret;
    public GameObject node;
    public GameObject dummy;
    DummyDragObject dummyDragObject;

    [HideInInspector]
	public TurretBlueprint turretBlueprint;
    BuildManager buildManager;
    void Start() 
    {
        buildManager = BuildManager.instance;   
    }
    private void OnMouseDown() 
    {
        //mouseButtonReleased = false;
        GameObject _dummy = (GameObject)Instantiate(turretSelf.dummy, transform.position, Quaternion.identity);      
        dummy = _dummy;
        dummyDragObject = dummy.GetComponent<DummyDragObject>();
    }
    
    private void OnMouseDrag() 
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dummy.transform.position = new Vector3(mousePosition.x, mousePosition.y,-0.02f);           
    }
    

    private void OnMouseUp() 
    {   
        if(dummyDragObject.mergeReady)
        {
            BuildTurret(buildManager.GetTurretToMerge());
            Destroy(dummyDragObject.node.GetComponent<Node>().turret);
            dummyDragObject.node.GetComponent<Node>().UpdateTurret(turret);
            buildManager.ResetTurretMerge();                        
            Destroy(dummy);
            Destroy(gameObject);

        }
        else
        {
            Destroy(dummy);
        }        
    }    

    void BuildTurret(TurretBlueprint blueprint)
    {
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, dummyDragObject.node.transform.position+new Vector3(0,0,-1.01f), Quaternion.identity);
        turret = _turret;
        turretBlueprint = blueprint;        
    }
    
}

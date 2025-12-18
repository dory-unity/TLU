using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    // 모든 노드를 추적하기 위한 정적 리스트
    private static List<Node> allNodes = new List<Node>();

    private Vector3 positionOffset;
    private Vector3 effectOffset;
    BuildManager buildManager;
    
    // UI 참조 최적화: 매번 Find하지 않고 필요할 때만 참조하거나 캐싱
    private static Transform shop;
    private static Transform canvas;

    [Header("Optional")]
    public GameObject turret;

    [HideInInspector]
    public TurretBlueprint turretBlueprint;

    void OnEnable()
    {
        // 노드가 활성화될 때 리스트에 추가
        allNodes.Add(this);
    }

    void OnDisable()
    {
        // 노드가 비활성화되거나 파괴될 때 리스트에서 제거
        allNodes.Remove(this);
    }

    void Start()
    {        
        buildManager = BuildManager.instance;
        
        // 정적 변수 초기화 (한 번만 수행)
        if (shop == null) shop = GameObject.Find("Canvas")?.transform.Find("Shop");
        if (canvas == null) canvas = GameObject.Find("Canvas")?.transform;

        positionOffset = new Vector3(0, 0, -1.01f);
        effectOffset = new Vector3(0, 0.3f, -2.02f);
    }

    void OnMouseDown() 
    {
        // 어떤 노드든 클릭하면 랜덤한 위치에 건설 시도
        BuildOnRandomNode();
    }

    private void BuildOnRandomNode()
    {
        if (!buildManager.CanBuild) return;

        // 1. 비어 있는 노드들만 필터링
        List<Node> emptyNodes = new List<Node>();
        foreach (Node node in allNodes)
        {
            if (node.turret == null)
            {
                emptyNodes.Add(node);
            }
        }

        // 2. 비어 있는 노드가 있다면 랜덤하게 하나 선택
        if (emptyNodes.Count > 0)
        {
            int randomIndex = Random.Range(0, emptyNodes.Count);
            Node targetNode = emptyNodes[randomIndex];

            // 3. 선택된 노드에 건설 수행
            targetNode.PerformBuild(buildManager.GetTurretToBuild());
            
            // 건설 후 상태 초기화
            buildManager.ResetTurret();
            UpdateShopUI();
        }
        else
        {
            Debug.Log("모든 노드에 터렛이 가득 찼습니다!");
        }
    }

    // 실제 건설 로직 (선택된 노드에서 실행됨)
    public void PerformBuild(TurretBlueprint blueprint)
    {
        if (blueprint == null || blueprint.prefab == null) return;

        GameObject _turret = Instantiate(blueprint.prefab, transform.position + positionOffset, Quaternion.identity);
        turret = _turret;
        turretBlueprint = blueprint;
        
        GameObject effect = Instantiate(buildManager.buildEffect, transform.position + effectOffset, Quaternion.identity);
        Destroy(effect, 3f);
    }

    // UI 텍스트 초기화 로직 (Static 참조 활용)
    private void UpdateShopUI()
    {
        if (shop == null || canvas == null) return;

        Image turretImage = shop.Find("TurretImage").GetComponent<Image>();
        Image turretImage2 = canvas.Find("TurretImage2").GetComponent<Image>();
        
        if (turretImage != null && turretImage2 != null)
            turretImage.sprite = turretImage2.sprite;

        Transform textParent = shop.Find("TurretText");
        if (textParent != null)
        {
            textParent.Find("DamageText").GetComponent<Text>().text = null;
            textParent.Find("RangeText").GetComponent<Text>().text = null;
            textParent.Find("SpeedText").GetComponent<Text>().text = null;
            textParent.Find("LegendText").GetComponent<Text>().text = null;
        }
    }

    public void UpdateTurret(GameObject _turret)
    {
        turret = _turret;
    }
}
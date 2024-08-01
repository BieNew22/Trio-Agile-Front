using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using NavMeshSurface = NavMeshPlus.Components.NavMeshSurface;
using static UnityEditor.Experimental.GraphView.GraphView;


public class GameManager : MonoBehaviour
{
    public LayerMask unitLayer; // ������ ���� ���̾�
    public LayerMask groundLayer; // ���� ���� ���̾�
    public LayerMask resourceLayer; // �ڿ��� ���� ���̾�
    private List<GameObject> selectedUnits = new List<GameObject>(); //���õ� ����
    private GameObject selelctedResource; // ���õ� �ڿ�

<<<<<<< Updated upstream:Assets/Scripts/GameManager.cs
    public NavMeshSurface surface1; // 1�� navmesh surface
    public NavMeshSurface surface2; // 2�� navmesh surface
=======
    public LayerMask floor0; // 1층 이동을 지원하는 레이어.
    public LayerMask floor1; // 2층 이동을 지원하는 레이어.

    //다수의 유닛을 선택하기 위한 게임 오브젝트 리스트 & 선택된 자원 오브젝트
    private List<GameObject> selectedUnits = new List<GameObject>(); //선택된 유닛
    private GameObject selelctedResource; // 선택된 자원

    public NavMeshSurface surface0; // 1층 navmesh surface
    public NavMeshSurface surface1; // 2층 navmesh surface

>>>>>>> Stashed changes:Assets/Scripts/Manager/GameManager.cs

    void Update()
    {
        UnitSelection();
        UnitMovement();
        UpdateNavMeshSurface();
    }

    //���� ����
    void UnitSelection()
    {
        if (Input.GetMouseButtonDown(0)) // ���� Ŭ��
        {
            //���콺 ��ġ ����
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //���콺 ��ġ ��ǥ���� raycast
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, unitLayer);
            if (hit.collider != null)
            {
                Debug.Log("Hit: " + hit.collider.name); // ����� �α� 
                // tag�� ������ �͸�
                if (hit.collider.CompareTag("Unit"))
                {
                    GameObject selectedUnit = hit.collider.gameObject;
                    ToggleUnitSelection(selectedUnit);
                }
            }
            else
            {
                ClearSelection();
            }
        }
    }

    //NavMesh surface�� �����ϴ� �Լ�
    void SwitchNavMeshSurface(NavMeshSurface surface)
    {
        if (surface == null)
        {
            Debug.LogError("SwitchNavMeshSurface: surface is null");
            return;
        }
        var units = FindObjectsOfType<UnitInterface>();
        foreach (var unit in units)
        {
            unit.SetNavMeshSurface(surface);
        }
    }

    //�������� ���� ������ ���̾� ���� �Լ�
    void SwitchSortingLayer(string layerName, int order)
    {
        if (layerName == null)
        {
            Debug.LogError("SwitchSortingLayer: layerName & order is null");
            return;
        }
        var units = FindObjectsOfType<UnitInterface>();
        foreach (var unit in units)
        {
            unit.SetSortingLayer(layerName, order);
        }
    }

    //� �̺�Ʈ�� �������� �� Navmesh surface�� ��ȯ������ ���ϴ� �Լ�. ���� �������� ���������� ���.
    void UpdateNavMeshSurface()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchNavMeshSurface(surface0);
            SwitchSortingLayer("Floor-0", 3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchNavMeshSurface(surface1);
            SwitchSortingLayer("Floor-1", 3);
        }
    }

    //�巡�׸� �����Ͽ� �ټ��� ������ �����ϴ� �Լ� (�̿ϼ�)
    void ToggleUnitSelection(GameObject unit)
    {
        if (selectedUnits.Contains(unit))
        {
            selectedUnits.Remove(unit);
            //SetOutlineEffect(unit, false);
        }
        else
        {
            selectedUnits.Add(unit);
            //SetOutlineEffect(unit, true);
        }
    }

    //void setoutlineeffect(gameobject unit, bool showoutline)
    //{
    //    outlineeffect outlineeffect = unit.getcomponent<outlineeffect>();
    //    if (outlineeffect != null)
    //    {
    //        outlineeffect.setoutline(showoutline);
    //    }
    //}

    // ������ ������ ���� ����.
    void ClearSelection()
    {
        foreach (GameObject unit in selectedUnits)
        {
            //SetOutlineEffect(unit, false);
        }
        selectedUnits.Clear();
    }

    // ���ֿ� ������ ��ũ��Ʈ�� �̵������ �ο��ϴ� �Լ�.
    void UnitMovement()
    {
        if (Input.GetMouseButtonDown(1)) // ������ Ŭ��
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            foreach (GameObject unit in selectedUnits)
            {
<<<<<<< Updated upstream:Assets/Scripts/GameManager.cs
                unit.GetComponent<UnitInterface>().MoveTo(mousePosition);
=======
                int agentID = unit.GetComponent<NavMeshAgent>().agentTypeID;
                bool isTransit = CheckLayer(mousePosition, agentID);
                unit.GetComponent<IUnit>().MoveTo(mousePosition, isTransit);
>>>>>>> Stashed changes:Assets/Scripts/Manager/GameManager.cs
            }
            ClearSelection();
        }
    }

<<<<<<< Updated upstream:Assets/Scripts/GameManager.cs
    //�ڿ�ä�� ��� (���� ��)
=======
    public bool CheckLayer(Vector3 mousePosition, int agentID)
    {
        // 레이어 마스크 생성

        // Raycast 검사
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 0, floor1);
        Debug.Log("Testing_1");
        Debug.Log("Hit: " + hit.collider.name); // 디버그 로그 
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == floor0)
            {
                Debug.Log("Clicked on an object in Layer 1");
                return true;
            }
            else if (hit.collider.gameObject.layer == floor1 && agentID != surface1.agentTypeID)
            {
                Debug.Log("Clicked on an object in Layer 2");
                return true;
            }
            else
            {
                Debug.Log("Testing_2");
                return false;
            }      
        }
        else
        {
            Debug.Log("No object was clicked.");
            return false;
        }
    }

    /// <summary>
    /// 자원채집 명령 함수 (미구현)
    /// </summary>
>>>>>>> Stashed changes:Assets/Scripts/Manager/GameManager.cs
    void orderResource()
    {
        if (selectedUnits != null && selectedUnits.Count > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                foreach (GameObject unit in selectedUnits)
                {
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, resourceLayer))
                    {
                        if (hit.collider != null && hit.collider.CompareTag("Resuorce"))
                        {
                            selelctedResource = hit.collider.gameObject;
<<<<<<< Updated upstream:Assets/Scripts/GameManager.cs
                            unit.GetComponent<UnitInterface>().getResource(selelctedResource);
=======
                            //오류로 잠깐 주석
                           // unit.GetComponent<IUnit>().getResource(selelctedResource);
>>>>>>> Stashed changes:Assets/Scripts/Manager/GameManager.cs
                        }        
                    }
                }
            }
        }
       
    }
}

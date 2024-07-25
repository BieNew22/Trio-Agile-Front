using UnityEngine;
using UnityEngine.AI;
using NavMeshSurface = NavMeshPlus.Components.NavMeshSurface;

public class UnitInterface : MonoBehaviour
{
    private Vector3 targetPosition;
    public Animator animator;
    private NavMeshAgent agent;

    public SpriteRenderer OriginRenderer;


    enum STATE
    {
        MOVE, IDLE, CHOP, HUNT, ATTAK, DIE
    }

    private STATE nowState = STATE.IDLE;
    private int health;
    private int amount; // 1ȸ ä����

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        OriginRenderer = GetComponent<SpriteRenderer>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        SwitchAnimation();
        Flip();
    }
    
    //�ִϸ��̼� ������ ����
    public void SwitchAnimation()
    {
        switch (nowState)
        {
            //���ϸ������� �Ķ���͸� ���� �ִϸ��̼� ����.
            case STATE.MOVE:
                animator.SetFloat("Move", 1000f);
                if (!agent.pathPending && (agent.remainingDistance <= 0.1f))
                {
                    animator.SetFloat("Move", 0f);
                }
                break;
            default:
                animator.SetFloat("Move", 0f);
                break;
        }
    }

    //���� �̵� �Լ�
    public void MoveTo(Vector3 position)
    {
        targetPosition = new Vector3(position.x, position.y, 0);
        nowState = STATE.MOVE;
        agent.SetDestination(targetPosition); // navMesh ������ ����.
        Flip();
    }

    //navMesh surface ������� ����
    public void SetNavMeshSurface(NavMeshSurface surface)
    {
        if (agent != null && surface != null)
        {
            // ���⼭�� agentTypeID�� ����Ͽ� NavMeshSurface�� �����մϴ�.
            agent.agentTypeID = surface.agentTypeID;
        }
    }
    
    //�������� ���� ���̾� ����.
    public void SetSortingLayer(string layerName, int order)
    {
        OriginRenderer.sortingLayerName = layerName;
        OriginRenderer.sortingOrder = order;
    }


    /// <summary>
    /// �ϲ� ���� ��ȯ
    /// </summary>
    private void Flip()
    {
        // ������ ���� �̵�
        if (agent.velocity.x > 0)
        {
            OriginRenderer.flipX = false;
        }
        // ���� ���� �̵�
        if (agent.velocity.x < 0)
        {
            OriginRenderer.flipX = true;
        }
    }

    public void getResource(GameObject selectedResource)
    {
        MoveTo(selectedResource.transform.position);
        int holdAmount = selectedResource.GetComponent<Resource>().Gather(amount);
    }


}

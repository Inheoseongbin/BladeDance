using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    protected CommonAIState _currentState;

    private Transform _targetTrm;
    public Transform TargetTrm => _targetTrm;

    private NavAgentMovement _navMovement;
    public NavAgentMovement NavMovement => _navMovement;


    private AgentAnimator _agentAnimator; //1
    public AgentAnimator AgentAnimator => _agentAnimator; //2

    private EnemyVFXManager _vfxManager;
    public EnemyVFXManager VFXManager => _vfxManager;

    protected virtual void Awake()
    {
        List<CommonAIState> states = new List<CommonAIState>();
        transform.Find("AI").GetComponentsInChildren<CommonAIState>(states);

        //�� ������Ʈ�� ���� �¾��� ���⼭ �� ����
        states.ForEach(s => s.SetUp(transform));

        _navMovement = GetComponent<NavAgentMovement>();
        _agentAnimator = transform.Find("Visual").GetComponent<AgentAnimator>(); //3
        _vfxManager =GetComponent<EnemyVFXManager>();
    }

    protected virtual void Start()
    {
        _targetTrm = GameManager.Instance.PlayerTrm; 
        //���߿� ���� ������ ���Ǿ�� ���氡���ϴ�.

    }
    
    public void ChangeState(CommonAIState nextState)
    {
        //������Ʈ ü���� 
        _currentState?.OnExitState();
        _currentState = nextState;
        _currentState?.OnEnterState();
    }

    void Update()
    {
        _currentState?.UpdateState();
    }
}

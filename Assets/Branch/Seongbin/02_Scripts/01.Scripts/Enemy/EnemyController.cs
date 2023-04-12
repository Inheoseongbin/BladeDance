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

        //각 스테이트에 대한 셋업이 여기서 들어갈 예정
        states.ForEach(s => s.SetUp(transform));

        _navMovement = GetComponent<NavAgentMovement>();
        _agentAnimator = transform.Find("Visual").GetComponent<AgentAnimator>(); //3
        _vfxManager =GetComponent<EnemyVFXManager>();
    }

    protected virtual void Start()
    {
        _targetTrm = GameManager.Instance.PlayerTrm; 
        //나중에 직접 오버랩 스피어로 변경가능하다.

    }
    
    public void ChangeState(CommonAIState nextState)
    {
        //스테이트 체인지 
        _currentState?.OnExitState();
        _currentState = nextState;
        _currentState?.OnEnterState();
    }

    void Update()
    {
        _currentState?.UpdateState();
    }
}

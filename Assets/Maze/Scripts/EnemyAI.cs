using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public static Action OnPlayerAttacked;
    [SerializeField] float _attackRange;
    [SerializeField] float _speed;
    [SerializeField] Transform[] _wayPoints;
    [SerializeField] Transform _player;
    public int _currentWaypointIndex;
    private NavMeshAgent _agent;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateUpAxis = false;
        _agent.updateRotation = false;
        _agent.speed = _speed;
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector3.Distance(_player.position,transform.position);
        Debug.Log($"Distance:{playerDistance}");
        if(playerDistance < _attackRange){
            _agent.SetDestination(_player.position);
            if(_agent.remainingDistance < 0.5f) OnPlayerAttacked?.Invoke();
            return;
        }
        
        if(_agent.remainingDistance < 0.5f)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _wayPoints.Length;
            _agent.SetDestination(_wayPoints[_currentWaypointIndex].position);
        }
    }
}

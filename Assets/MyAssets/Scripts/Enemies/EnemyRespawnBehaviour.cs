using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRespawnBehaviour : MonoBehaviour
{

	private NavMeshAgent _navMeshAgent;
	private EnemyWaypointManager _waypointManager;
	private bool _firstSpawn = true;

	private void Awake()
	{
		_navMeshAgent = GetComponent<NavMeshAgent>();
		_waypointManager = GetComponentInChildren<EnemyWaypointManager>();
	}

	private void OnEnable()
	{
		if(!_firstSpawn)
		{
			_navMeshAgent.SetDestination(_waypointManager.GetCurrentWaypointPosition());
		}
	}

	void Start()
    {
		_firstSpawn = false;
    }

}

#pragma warning disable 649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWaypoint : MonoBehaviour
{
	[SerializeField]
	[Tooltip("GameObject of an enemy which waypoint it is")]
	private GameObject _enemyGameObject;

	[SerializeField]
	[Tooltip("Transform of an object which will hold all the waypoint checkpoints so they won't move with enemy")]
	private Transform _newWaypointParent;

	private EnemyWaypointManager _enemyWaypointManager;

	private NavMeshAgent _enemyNavMeshAgent;


	private void Awake()
	{
		_enemyWaypointManager = _enemyGameObject.GetComponentInChildren<EnemyWaypointManager>();
		_enemyNavMeshAgent = _enemyGameObject.GetComponent<NavMeshAgent>();
	}

	private void Start()
	{
		_enemyNavMeshAgent.SetDestination(_enemyWaypointManager.GetNextWaypointPosition());
		transform.SetParent(_newWaypointParent);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == _enemyGameObject)
		{
			_enemyNavMeshAgent.SetDestination(_enemyWaypointManager.GetNextWaypointPosition());
		}
	}
}

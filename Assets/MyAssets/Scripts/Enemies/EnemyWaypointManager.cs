using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaypointManager : MonoBehaviour
{
	private enum OnLastWaypointBehaviour
	{
		GoToFirst,
		Backtrack,
		GoToSpecific
	};

	[SerializeField]
	[Tooltip("Behaviour which will happen when target reaches the position of last waypoint")]
	private OnLastWaypointBehaviour _lastWaypointBehaviour = OnLastWaypointBehaviour.GoToFirst;

	[SerializeField]
	[Tooltip("In casse \"Go To Specific\" is selected: from which waypoint in row to repeat the cycle" +
		"\n Note: First waypoint is number 0")]
	private int _waypointToRepeatFrom = 0;

	private int _currentWaypointIndex = 0;
	private int _lastWaypointIndex;
	private List<Vector3> _waypointPositions = new List<Vector3>();
	private bool _isGoingBackwards = false;

	private void Awake()
	{
		Transform[] waypointsTransforms = GetComponentsInChildren<Transform>();

		foreach(Transform waypointTransform in waypointsTransforms)
		{
			if(waypointTransform != this.transform)
			{
				_waypointPositions.Add(waypointTransform.position);
			}
		}

		_lastWaypointIndex = _waypointPositions.Count;
	}

	public Vector3 GetCurrentWaypointPosition()
	{
		return _waypointPositions[_currentWaypointIndex];
	}

	public Vector3 GetNextWaypointPosition()
	{
		if(!_isGoingBackwards)
		{
			_currentWaypointIndex++;
		}
		else
		{
			_currentWaypointIndex--;
		}

		if(_currentWaypointIndex < 0)
		{
			_currentWaypointIndex = 0;
			_isGoingBackwards = false;
		}

		if(_currentWaypointIndex >= _lastWaypointIndex)
		{
			switch(_lastWaypointBehaviour)
			{
				case OnLastWaypointBehaviour.GoToFirst:
					_currentWaypointIndex = 0;
					break;
				case OnLastWaypointBehaviour.Backtrack:
					_currentWaypointIndex = _lastWaypointIndex - 1;
					_isGoingBackwards = true;
					break;
				case OnLastWaypointBehaviour.GoToSpecific:
					_currentWaypointIndex = _waypointToRepeatFrom;
					break;
				default:
					_currentWaypointIndex = 0;
					break;
			}
		}

		return _waypointPositions[_currentWaypointIndex];
	}
}

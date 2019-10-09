#pragma warning disable 649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Speed in degrees per second of the cube rotation")]
	private float _rotationSpeed = 50.0f;

	[SerializeField]
	[Tooltip("Time it takes for cube to reach end high/low point")]
	private float _movementDuration = 1.5f;

	[SerializeField]
	[Tooltip("High point the cube will reach")]
	private Transform _highPointTransform;

	[SerializeField]
	[Tooltip("Low point the cube will reach")]
	private Transform _lowPointTransform;

	private void Start()
	{
		MoveCubeUp();
	}

	private void Update()
	{
		transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime, Space.World);
	}

	private void MoveCubeUp()
	{
		iTween.MoveTo(this.gameObject, iTween.Hash(
				"position"	, _highPointTransform.position,
				"time"		, _movementDuration,
				"easetype"	, iTween.EaseType.easeInOutQuad,
				"oncomplete", "MoveCubeDown"
				)
		);
	}

	private void MoveCubeDown()
	{
		iTween.MoveTo(this.gameObject, iTween.Hash(
				"position", _lowPointTransform.position,
				"time", _movementDuration,
				"easetype", iTween.EaseType.easeInOutQuad,
				"oncomplete", "MoveCubeUp"
				)
		);
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObjectAfterTime : MonoBehaviour
{

	[SerializeField]
	[Tooltip("How long in seconds until disabling GameObject after enabling it")]
	private float _disableAfterSeconds = 2.0f;

	private void OnEnable()
	{
		Invoke("DisableGameObject", _disableAfterSeconds);
	}

	private void DisableGameObject()
	{
		gameObject.SetActive(false);
	}

	private void OnDisable()
	{
		CancelInvoke();
	}

}

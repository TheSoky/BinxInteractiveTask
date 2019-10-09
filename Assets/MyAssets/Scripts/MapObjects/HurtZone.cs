using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtZone : MonoBehaviour
{
	[SerializeField]
	[Tooltip("How much damage is zone doing to player per second")]
	private float _damagePerSecond = 10.0f;

	private PlayerHealth _playerHealth;

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			_playerHealth = other.GetComponent<PlayerHealth>();
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player" && _playerHealth != null)
		{
			_playerHealth.ModifyHealth(-(_damagePerSecond * Time.deltaTime));
		}
	}
}

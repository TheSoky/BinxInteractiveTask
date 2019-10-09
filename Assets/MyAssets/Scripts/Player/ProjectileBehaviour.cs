using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Amount of damage this projectile will cause")]
	private float _projectileDamage = 10.0f;

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Enemy")
		{
			collision.gameObject.GetComponent<EnemyHealth>().ModifyHealth(-_projectileDamage);
		}
		gameObject.SetActive(false);
	}
}

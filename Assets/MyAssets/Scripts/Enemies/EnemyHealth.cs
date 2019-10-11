using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

	[SerializeField]
	[Tooltip("Maximum amount of health this enemy unit can have")]
	private float _maxhealth = 100.0f;

	[SerializeField]
	[Tooltip("Time needed for the respawn of enemy")]
	private float _respawnTime = 30.0f;

	public float _currentHealth;

	private Renderer _renderer;

	private void Awake()
	{
		_renderer = GetComponent<Renderer>();
	}

	private void OnEnable()
	{
		_currentHealth = _maxhealth;
	}

	public void ModifyHealth(float amountModified)
	{
		_currentHealth += amountModified;
		_renderer.material.color = Color.Lerp(Color.red, Color.white, _currentHealth / _maxhealth);
		if(_currentHealth <= 0.0f)
		{
			TriggerDeath();
		}
	}

	private void TriggerDeath()
	{
		//TODO create and add death animation
		gameObject.SetActive(false);
	}

	private void OnDisable()
	{
		GameManager.Instance.CheckIfGameIsWon();
		Invoke("ActivateEnemy", _respawnTime);
	}

	private void ActivateEnemy()
	{
		gameObject.SetActive(true);
		_currentHealth = _maxhealth;
		_renderer.material.color = Color.white;
	}

}

#pragma warning disable 649

using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Maximum amount of health a player can have")]
	private float _maxHealth = 100.0f;

	[SerializeField]
	[Tooltip("Slider which represents players health in UI")]
	private Slider _healthBarSlider;

	public float _currentHealth;

	private void Awake()
	{
		_currentHealth = _maxHealth;
		_healthBarSlider.value = 1;
	}

	public void ModifyHealth(float amountModified)
	{
		_currentHealth += amountModified;
		_currentHealth = Mathf.Min(_currentHealth, _maxHealth);
		_healthBarSlider.value = _currentHealth / _maxHealth;
		if(_currentHealth <= 0.0f)
		{
			GameManager.Instance.TriggerEndGame(false);
		}
	}

}

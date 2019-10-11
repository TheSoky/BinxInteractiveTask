#pragma warning disable 649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickupController : MonoBehaviour
{
	[SerializeField]
	[Tooltip("How much distance can be object spotted and picked up from")]
	private float _rayLenght = 20.0f;

	[SerializeField]
	[Tooltip("Layer on which are objects that can be picked up")]
	private LayerMask _pickupableLayer;

	[SerializeField]
	[Tooltip("Prefix for pick up tooltip text")]
	private string _pickupableTextPrefix = "[E] Pick up ";

	[SerializeField]
	[Tooltip("Text box which shows the tooltip for pick up")]
	private Text _pickupableText;

	private ShootingController _shootingController;
	private PlayerHealth _playerHealth;

	private void Awake()
	{
		_shootingController = GetComponentInParent<ShootingController>();
		_playerHealth = GetComponentInParent<PlayerHealth>();
	}

	private void Update()
	{
		RaycastHit hitResult;
		bool isSuccessful = Physics.Raycast(transform.position, transform.forward, out hitResult, _rayLenght, _pickupableLayer);
		if(isSuccessful)
		{
			_pickupableText.text = _pickupableTextPrefix + hitResult.transform.name;
			if(Input.GetButtonDown("Pickup"))
			{
				switch(hitResult.transform.name)
				{
					case "Green Cube":
						GameManager.Instance.CollectGreenCube();
						hitResult.transform.gameObject.SetActive(false);
						break;

					case "Yellow Cube":
						GameManager.Instance.CollectYellowCube();
						hitResult.transform.gameObject.SetActive(false);
						break;

					case "Purple Sphere":
						_playerHealth.ModifyHealth(25.0f);
						hitResult.transform.gameObject.SetActive(false);
						break;

					case "Orange Sphere":
						_shootingController.AddAmmo(30);
						hitResult.transform.gameObject.SetActive(false);
						break;

					default:
						hitResult.transform.gameObject.SetActive(false);
						break;
				}
			}
		}
		else
		{
			_pickupableText.text = "";
		}
	}
}

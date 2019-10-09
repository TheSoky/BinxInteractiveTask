#pragma warning disable 649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingController : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Prefab which will be used for bullet")]
	private GameObject _projectilePrefab;

	[SerializeField]
	[Tooltip("Transform containing position and rotation from which projectile will be shot")]
	private Transform _shootingTransform;

	[SerializeField]
	[Tooltip("Time in seconds between each shot")]
	private float _secondsBetweenShots = 0.1f;

	[SerializeField]
	[Tooltip("Time in seconds needed to relaod the clip")]
	private float _reloadTimeInSeconds = 2.5f;

	[SerializeField]
	[Tooltip("Total amount of ammunition player start's with")]
	private int _startingAmmoAmount = 150;

	[SerializeField]
	[Tooltip("Amount of maximum ammo stored in one clip")]
	private int _ammoPerClip = 30;

	[SerializeField]
	[Tooltip("Force at which projectile will be shot out")]
	private float _projectileForce = 10.0f;

	[SerializeField]
	[Tooltip("Prefix text for ammunition")]
	private string _ammoPrefixText = "Ammo: ";

	[SerializeField]
	[Tooltip("Text box which gives player info on ammunition")]
	private Text _ammoText;

	private int _currentAmmoInClip = 0;

	private int _currentAmmoOutsideTheClip = 0;

	private bool _isReloading = false;

	private void Start()
	{
		PoolManager.Instance.AddNewPool(_projectilePrefab, _ammoPerClip);

		_currentAmmoInClip = Mathf.Clamp(_startingAmmoAmount, _startingAmmoAmount, _ammoPerClip);
		_currentAmmoOutsideTheClip = _startingAmmoAmount - _currentAmmoInClip;
		_ammoText.text = _ammoPrefixText + _currentAmmoInClip.ToString() + " / " + _currentAmmoOutsideTheClip.ToString();
	}

	private void Update()
	{
		if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Reload"))
		{
			StartCoroutine(ShootingAndReloading());
		}
	}

	private IEnumerator ShootingAndReloading()
	{
		while((Input.GetButton("Fire1") || Input.GetButtonDown("Reload")) && !_isReloading)
		{
			if(_currentAmmoInClip > 0 && Input.GetButton("Fire1"))
			{
				ShootProjectile();
				_currentAmmoInClip -= 1;
				yield return new WaitForSeconds(_secondsBetweenShots);
			}
			else if(!_isReloading && _currentAmmoOutsideTheClip > 0)
			{
				_isReloading = true;
				yield return new WaitForSeconds(_reloadTimeInSeconds);
				int bulletsTransferedToClip = Mathf.Clamp(_ammoPerClip - _currentAmmoInClip, _ammoPerClip - _currentAmmoInClip, _currentAmmoOutsideTheClip);
				_currentAmmoInClip += bulletsTransferedToClip;
				_currentAmmoOutsideTheClip -= bulletsTransferedToClip;
				_isReloading = false;
			}
			else
			{
				yield return null;
			}
			_ammoText.text = _ammoPrefixText + _currentAmmoInClip.ToString() + " / " + _currentAmmoOutsideTheClip.ToString();
		}

		yield return new WaitForSeconds(_reloadTimeInSeconds);
		_isReloading = false;
		yield return null;
	}

	private void ShootProjectile()
	{
		GameObject projectile = PoolManager.Instance.GetNextAvailableObject(_projectilePrefab);
		projectile.transform.position = _shootingTransform.position;
		projectile.transform.rotation = _shootingTransform.rotation;
		projectile.SetActive(true);
		projectile.GetComponent<Rigidbody>().velocity = _shootingTransform.forward * _projectileForce;
	}

	public void AddAmmo(int amountOfAmmo)
	{
		_currentAmmoOutsideTheClip += amountOfAmmo;
		_ammoText.text = _ammoPrefixText + _currentAmmoInClip.ToString() + " / " + _currentAmmoOutsideTheClip.ToString();
	}
}

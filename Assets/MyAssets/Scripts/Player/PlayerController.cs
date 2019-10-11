#pragma warning disable 649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Speed of movements as units per second")]
    private float _movementSpeed = 3.0f;

    [SerializeField]
    [Tooltip("Gravity which constantly affects the player")]
    private Vector3 _gravity = Physics.gravity;

    [SerializeField]
    [Tooltip("Duration in sseconds at which will player be going upwards during jump")]
    private float _jumpDuration = 1.5f;

    [SerializeField]
    [Tooltip("Transform of an object which will check if the player is on the ground")]
    private Transform _groundDetectionObjectTransform;

	[SerializeField]
	[Tooltip("Layer mask on which is ground set")]
	private LayerMask _groundLayerMask;

	[SerializeField]
	[Tooltip("Main Camera transform, FPS camera for player in this scene")]
	//Necessary because otherwisse it takes camera from loading scene
    private Transform _mainCameraTransform;

    //Cached character controller for access in code
    private CharacterController _characterController;

	//Checks the status of Jump() coroutine
	bool _isJumping = false;

    private void Start() 
    {
        _characterController = GetComponent<CharacterController>();
        if(_characterController == null) 
        {
            Debug.LogError("Players Character Controller not found");
        }
    }

    private void Update() 
    {
        Vector3 movement = Vector3.zero;
        movement.x = Input.GetAxis("Horizontal") * _movementSpeed;
        movement.z = Input.GetAxis("Vertical") * _movementSpeed;

        movement = _mainCameraTransform.TransformDirection(movement);

		if(Input.GetButtonDown("Jump") && CheckIfOnGround() && !_isJumping) 
		{
			StartCoroutine(Jump());
		}

		movement = movement + _gravity;
		
		_characterController.Move(movement * Time.deltaTime);
    }

    private bool CheckIfOnGround() 
    {
		return Physics.Linecast(transform.position, _groundDetectionObjectTransform.position, _groundLayerMask);
    }

	private IEnumerator Jump() 
	{
		_gravity *= -1;
		_isJumping = true;

		yield return new WaitForSeconds(_jumpDuration);

		_gravity *= -1;
		_isJumping = false;
	}
}

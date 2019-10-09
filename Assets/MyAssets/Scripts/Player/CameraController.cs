#pragma warning disable 649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Speed of camera rotation as degrees per second multiplied by mouse axis")]
    private float _rotationSpeed = 1.0f;

	[SerializeField]
	[Tooltip("Inverts the controls for mouse input")]
	private bool _invertControls = false;

    [SerializeField]
    [Tooltip("Maximum rotation of a camera along the local X axis" + 
        "\nNote: Values greater than 180 will result in infinite rotation")]
    private float _maxRotationX = 200.0f;

    [SerializeField]
    [Tooltip("Maximum rotation of a camera along the local Y axis"+
        "\nNote: Values greater than 180 will result in infinite rotation")]
    private float _maxRotationY = 85.0f;

	private void Awake() 
	{
		if(!_invertControls)
		{
			_rotationSpeed *= -1;
		}
	}

	private void Update() 
    {
        float mouseInputX = Input.GetAxis("Mouse X") * _rotationSpeed;
        float mouseInputY = Input.GetAxis("Mouse Y") * _rotationSpeed;

        Vector3 finalCameraRotation = transform.localEulerAngles;

        finalCameraRotation.x += mouseInputY;
        finalCameraRotation.y -= mouseInputX;

        if(finalCameraRotation.x>180) {
            finalCameraRotation.x -= 360;
        }

        if(finalCameraRotation.x<-180) {
            finalCameraRotation.x += 360;
        }

        if(finalCameraRotation.y>180) {
            finalCameraRotation.y -= 360;
        }

        if(finalCameraRotation.y<-180) {
            finalCameraRotation.y += 360;
        }

        finalCameraRotation.x = Mathf.Clamp(finalCameraRotation.x, -_maxRotationY, _maxRotationY);
        finalCameraRotation.y = Mathf.Clamp(finalCameraRotation.y, -_maxRotationX, _maxRotationX);
        finalCameraRotation.z = 0.0f;
        transform.localRotation =Quaternion.Euler(finalCameraRotation);
        

    }
}

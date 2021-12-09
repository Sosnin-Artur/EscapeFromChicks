using UnityEngine;
using System.Collections;

public class MouseLookMobile : MonoBehaviour 
{	
	[SerializeField] private float sensitivityHor = 9.0f;
	[SerializeField] private float sensitivityVert = 9.0f;
	
	[SerializeField] private float minimumVert = -45.0f;
	[SerializeField] private float maximumVert = 45.0f;
    
	[SerializeField] private RotationAxes axes = RotationAxes.MouseXAndY;
	[SerializeField] private TouchField touchField;

	private float _rotationX = 0;
	
	private void Update() 
	{
		float axisX = touchField.touchDist.x;
		float axisY = touchField.touchDist.y;

		if (axes == RotationAxes.MouseX) 
		{
			transform.Rotate(0, axisX * sensitivityHor, 0);
		}
		else if (axes == RotationAxes.MouseY) 
		{
			_rotationX -= axisY * sensitivityVert;
			_rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
			
			transform.localEulerAngles = new Vector3(_rotationX, transform.localEulerAngles.y, 0);
		}
		else 
		{
			float rotationY = transform.localEulerAngles.y + axisX * sensitivityHor;

			_rotationX -= axisY * sensitivityVert;
			_rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

			transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
		}
	}
}
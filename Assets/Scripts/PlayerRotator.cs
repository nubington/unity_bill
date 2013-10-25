using UnityEngine;
using System.Collections;

public class PlayerRotator : MonoBehaviour
{
	public Transform Player;
	
	public float UpdateRotator(float rotateSpeed)
	{
		transform.position = Player.transform.position;
		
		Vector3 rotation = Vector3.zero;
		
		float mouseX = Input.GetAxis("Mouse X");
		
		rotation.y = mouseX * rotateSpeed * Time.deltaTime;
		
		if (Screen.fullScreen)
			rotation.y *= 5;
		
		
		float mouseY = Input.GetAxis("Mouse Y");
		
		rotation.x = -mouseY * rotateSpeed * Time.deltaTime;
		
		if (Screen.fullScreen)
			rotation.x *= 5;
		
		//transform.eulerAngles += rotation;
		
		transform.eulerAngles = new Vector3(Mathf.Clamp(transform.eulerAngles.x + rotation.x, .01f, 89.9f), transform.eulerAngles.y + rotation.y, transform.eulerAngles.z + rotation.z);
		
		Debug.Log (transform.eulerAngles);
		
		//transform.eulerAngles = new Vector3(Mathf.Clamp ());
		
		return mouseX * rotateSpeed * Time.deltaTime;
		//return new Vector3(0f, rotation.y, 0f);
	}
}

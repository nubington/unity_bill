using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public GameObject player;
	public Transform PlayerRotator;
	public float RotateSpeed;
	public float ZoomSpeed;
	public float Height;
	public float MinRotationX, MaxRotationX;
	public float MinZoom, MaxZoom;
	
	float rotationX;
	Vector3 offset;
	float zoom;
	float angle;
	
	void Start()
	{
		angle = -player.transform.rotation.eulerAngles.y * Mathf.Deg2Rad - Mathf.PI / 2f;
		
		offset.y = Height;
		offset.x = zoom * Mathf.Cos(angle);
		offset.z = zoom * Mathf.Sin(angle);
		
		RotationX = transform.rotation.x;
		
		zoom = 5;
	}
	
	void LateUpdate()
	{	
		/*rotateX();
		
		angle = -player.transform.rotation.eulerAngles.y * Mathf.Deg2Rad - Mathf.PI / 2f;
		
		offset.x = zoom * Mathf.Cos(angle);
		offset.z = zoom * Mathf.Sin(angle);
		offset.y = -(zoom * Mathf.Cos(RotationX * Mathf.Deg2Rad - Mathf.PI));
		
		transform.position = player.transform.position + offset;
		
		transform.position = Vector3.MoveTowards(player.transform.position, transform.position, zoom);
		
		transform.LookAt(player.transform.position);*/
		
		transform.position = PlayerRotator.position - PlayerRotator.TransformDirection(Vector3.forward * zoom);
		
		
		transform.LookAt(PlayerRotator.position);
		
		//transform.rotation = transform.rotation * new Vector3(Mathf.Max(transform.rotation.x, 89.99f), transform.rotation.y, transform.rotation.z);
		
		doZoom();
	}
	
	void rotateX()
	{
		float mouseY = Input.GetAxis("Mouse Y");
		
		float rotateSpeed = player.GetComponent<PlayerController>().RotateSpeed;
		
		float rotateAmount = mouseY * rotateSpeed * Time.deltaTime;
		
		if (Screen.fullScreen)
			rotateAmount *= 5;
			
		RotationX += rotateAmount;
	}
	
	void doZoom()
	{
		float delta = Input.GetAxis("Mouse ScrollWheel");
		
		Zoom -= delta * ZoomSpeed * Time.deltaTime;
	}
	
	public float RotationX
	{
		get
		{
			return rotationX;
		}
		set
		{
			rotationX = Mathf.Clamp (value, MinRotationX, MaxRotationX);
			//rotationX = value;
		}
	}
	
	public float Zoom
	{
		get
		{
			return zoom;
		}
		set
		{
			zoom = Mathf.Clamp(value, MinZoom, MaxZoom);
		}
	}
}

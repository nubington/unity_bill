using UnityEngine;
using System.Collections.Generic;

public class PeanutController : MonoBehaviour
{
	public float RotateSpeed;
	public float Speed;
	public int Damage;
	float angle;
	
	Vector3 randomRotate;
	
	void Start()
	{
		angle = -transform.rotation.eulerAngles.y * Mathf.Deg2Rad + Mathf.PI / 2f;
		
		randomRotate = Random.insideUnitSphere;
	}
	
	void Update()
	{
		/*Vector3 rotate = new Vector3(Random.value, Random.value, Random.value);
		
		rotate *= RotateSpeed * Time.deltaTime;
		
		transform.Rotate(rotate);*/
		
		move();
		
		transform.Rotate(randomRotate * RotateSpeed * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Wall")
		{
			PlayerController.Peanuts.Remove(gameObject);
			Destroy (gameObject);
		}
	}
	
	void move()
	{
		transform.position += (new Vector3(Speed * Mathf.Cos(angle), 0f, Speed * Mathf.Sin(angle)) * Time.deltaTime);
	}
}

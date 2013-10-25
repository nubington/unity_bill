using UnityEngine;
using System.Collections.Generic;

public class PopCanTrigger : MonoBehaviour
{
	public static List<GameObject> PopCans = new List<GameObject>();
	public static List<GameObject> MovingPopCans = new List<GameObject>();
	
	public static int DefaultDamage = 1;
	
	public float LifeSpan;
	public static float CriticalVelocity { get; private set; }
	public bool IsAtCriticalVelocity { get; private set; }
	public int Damage = DefaultDamage;
	public float TimeSinceHit = .5f;
	
	float timeElapsed;
	
	static PopCanTrigger()
	{
		CriticalVelocity = 2f;
	}
	
	void Start()
	{
		transform.position = transform.parent.transform.position;
		transform.rotation = transform.parent.transform.rotation;
		transform.localScale = Vector3.one;
	}
	
	void Update()
	{
		timeElapsed += Time.deltaTime;
		TimeSinceHit += Time.deltaTime;
		
		// remove after time elapsed >= life span
		if (timeElapsed >= LifeSpan)
		{
			RemovePopCan(transform.parent.gameObject);
			Destroy(transform.parent.gameObject);
			return;
		}
		
		updateMovingStatus();
	}
	
	void updateMovingStatus()
	{
		// if velocity is >= critical velocity, make sure it is in list
		if (transform.parent.rigidbody.velocity.magnitude >= CriticalVelocity)
		{
			if (!MovingPopCans.Contains(transform.parent.gameObject))
				MovingPopCans.Add(transform.parent.gameObject);
			
			IsAtCriticalVelocity = true;
		}
		// if velocity < critical velocity, make sure it is NOT in list
		else
		{
			MovingPopCans.Remove(transform.parent.gameObject);
			
			IsAtCriticalVelocity = false;
		}
	}
	
	public static void AddPopCan(GameObject popCan)
	{
		PopCans.Add(popCan);
	}
	
	public static void RemovePopCan(GameObject popCan)
	{
		PopCans.Remove(popCan);
		MovingPopCans.Remove(popCan);
	}
}

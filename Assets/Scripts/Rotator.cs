using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour 
{
	void Update ()
	{
		transform.Rotate(new Vector3(15, 30, 35) * Time.deltaTime);
		//transform.Rotate (Vector3.right * Time.deltaTime);
	}
}

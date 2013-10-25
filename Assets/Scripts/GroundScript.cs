using UnityEngine;
using System.Collections;

public class GroundScript : MonoBehaviour
{
	public float Width, Height;
	
	void Start()
	{
		/*transform.localScale.x = Width / 5f;
		transform.localScale.y = 1f;
		transform.localScale.z = Height / 5f;*/
		
		transform.localScale = new Vector3(Width / 10f, 1f, Height / 10f);
	}
}

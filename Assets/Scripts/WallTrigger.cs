using UnityEngine;
using System.Collections;

public class WallTrigger : MonoBehaviour
{
	void Start()
	{
		transform.position = transform.parent.transform.position;
		transform.rotation = transform.parent.transform.rotation;
		transform.localScale = Vector3.one;
	}
}

using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour
{
	public Transform Ground;
	public float WallWidth, WallHeight;
	
	public Transform North, South, East, West;
	
	void Start()
	{
		GroundScript ground = Ground.GetComponent<GroundScript>();
		
		North.position = new Vector3(0f, WallHeight / 2f, ground.Height / 2 - WallWidth / 2);
		North.localScale = new Vector3(ground.Width, WallHeight, WallWidth);
		
		South.position = new Vector3(0f, WallHeight / 2f, -ground.Height / 2 + WallWidth / 2);
		South.localScale = new Vector3(ground.Width, WallHeight, WallWidth);
		
		East.position = new Vector3(ground.Width / 2 - WallWidth / 2, WallHeight / 2f, 0f);
		East.localScale = new Vector3(WallWidth, WallHeight, ground.Height);
		
		West.position = new Vector3(-ground.Width / 2 + WallWidth / 2, WallHeight / 2f, 0f);
		West.localScale = new Vector3(WallWidth, WallHeight, ground.Height);
	}
}

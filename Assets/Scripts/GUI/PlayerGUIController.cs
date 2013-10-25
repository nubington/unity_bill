using UnityEngine;
using System.Collections;

public class PlayerGUIController : MonoBehaviour
{
	public Transform Player;
	
	void OnGUI()
	{
		PlayerController player = Player.GetComponent<PlayerController>();
		
		GUI.TextField(new Rect(0, Screen.height - 23, 55, 23), player.HP + "/" + PlayerController.MaxHp);
	}
}

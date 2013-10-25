using UnityEngine;
using System.Collections;

public class EnemyGUIController : MonoBehaviour
{
	public Transform Enemy;
	public float HpBarWidth, HpBarHeight;
	public Texture RedTexture, BlackTexture;
	
	EnemyController enemy;
	
	void Start()
	{
		enemy = Enemy.GetComponent<EnemyController>();
	}
	
	void OnGUI()
	{
		float hpBarPosX = Screen.width - HpBarWidth - 1, hpBarPosY = 1;
		
		// black bar
		GUI.DrawTexture(new Rect(hpBarPosX, hpBarPosY, HpBarWidth, HpBarHeight), BlackTexture);
		
		// red bar
		float redBarWidth = HpBarWidth * enemy.HPPercent;
		GUI.DrawTexture(new Rect(hpBarPosX, hpBarPosY, redBarWidth, HpBarHeight), RedTexture);
		
		// hp string
		string hpString = enemy.HP + "/" + EnemyController.MaxHp;
		Vector2 stringSize = GUI.skin.label.CalcSize(new GUIContent(hpString));
		
		Vector2 stringPosition = new Vector2(hpBarPosX + HpBarWidth / 2 - stringSize.x / 2, hpBarPosY);
		
		GUI.Label(new Rect(stringPosition.x, stringPosition.y, stringSize.x, stringSize.y), enemy.HP + "/" + EnemyController.MaxHp);
	}
}

using UnityEngine;
using System.Collections;

public class OLDEnemyHealthBarScript : MonoBehaviour
{
	public Transform Enemy;

	void Start()
	{
		//transform.position = camera.camera.WorldToViewportPoint(transform.parent.transform.position);
		//transform.localScale = new Vector3(Enemy.localScale.normalized.x - 1, -Enemy.localScale.y / 100, 1);
		transform.localScale = new Vector3(Enemy.localScale.normalized.x / 20, -(Enemy.localScale.normalized.y / 5), 1);
	}
	
	void Update()
	{
		Vector3 viewportPoint = Camera.main.WorldToViewportPoint(new Vector3(Enemy.position.x, Enemy.position.y + Enemy.localScale.normalized.y * 2, Enemy.position.z));
		
		transform.position = viewportPoint;
		
		EnemyController enemy = Enemy.GetComponent<EnemyController>();
		transform.localScale = new Vector3(Enemy.localScale.normalized.x * (enemy.HP / (float)EnemyController.MaxHp) / 20, -(Enemy.localScale.normalized.y / 5), 1);
		Debug.Log (Enemy.localScale.normalized.x / 20 * (enemy.HP / (float)EnemyController.MaxHp));
		
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
		
		//EnemyController enemy = Enemy.GetComponent<EnemyController>();
		
		float percentHp = enemy.HP / (float)EnemyController.MaxHp;
		float hpBarWidth = 100 * percentHp;
		
		Rect p = guiTexture.pixelInset;
		
		//guiTexture.pixelInset.width = hpBarWidth;
		guiTexture.pixelInset.Set(p.x, p.y, hpBarWidth, p.height);
	}
	
	void LateUpdate()
	{
		//guiTexture.transform.position = camera.camera.WorldToViewportPoint(transform.parent.transform.position);
	}
	
	void OnGUI()
	{
		//transform.position = Camera.main.WorldToViewportPoint(transform.parent.transform.position);
	}
}

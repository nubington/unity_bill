using UnityEngine;
using System.Collections;

public class EnemyHealthBarController : MonoBehaviour
{
	float fullWidthScale;
    public Texture texture;
	
	void Start()
	{
		fullWidthScale = transform.localScale.x;
	}
	
	void Update()
	{
		//transform.LookAt(Camera.main.transform);
		
		//transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back,
            //Camera.main.transform.rotation * Vector3.up);
		
		//transform.rotation = Quaternion.LookRotation(Camera.main.transform.position) * Quaternion.Euler(90, 0, 0);
		
		EnemyController enemy = transform.parent.GetComponent<EnemyController>();
		
		float width = fullWidthScale * enemy.HPPercent;
		
		transform.localScale = new Vector3(width, transform.localScale.y, transform.localScale.z);
		
		transform.LookAt(Camera.main.transform.position);
		transform.rotation *= Quaternion.Euler(90, 0, 0);
		
		//transform.Translate(Vector3.right * (fullWidthScale - width) / 100f);
		
		//transform.Translate(Vector3.right.normalized * (1f - enemy.HPPercent));
        //GUI.DrawTexture(new Rect(100, 100, 100, 100), texture);
	}
	
	void OnGUI()
	{
		/*Vector3 position = Camera.main.WorldToScreenPoint(transform.position);
		
		GUI.DrawTexture(new Rect(position.x, Screen.height - position.y, width, height), texture);*/
	}
}

using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	public static int MaxHp = 10;
	public static float HurtAlpha = .3f;
	
	public static List<GameObject> Peanuts = new List<GameObject>();
	
	public float MoveSpeed;
	public float ShootDelay;
	public float RotateSpeed;
	public float JumpMod;
	public Transform Rotator;
	public Transform Peanut;
	public Transform Ground;
	public Transform Enemy;
	public Transform HurtGUITexture;
	public float HurtFadeSpeed;
	
	PlayerRotator rotator;
	
	float timeSinceLastShoot;
	float initialPosY;
	bool jumping;
	int hp = MaxHp;
	
	void Start()
	{
		rotator = Rotator.GetComponent<PlayerRotator>();
		initialPosY = transform.position.y;
		generateRespawnPoints();
	}
	
	void Update()
	{
		move();
		
		clampPosition();
		
		rotate();
		
		shoot();
		
		jump();
		
		updateRotator();
		
		fadeHurtGUITexture();
	}
	
	void OnTriggerEnter(Collider other)
	{
		//renderer.bounds.size
		//collider.transform.position
		
		if (other.tag == "Pop Can" && other.transform.parent.rigidbody.velocity.magnitude >= PopCanTrigger.CriticalVelocity)
		{
			//if (other.rigidbody.velocity.magnitude >= PopCanTrigger.CriticalVelocity);
			//Debug.Log(other.transform.parent.rigidbody.velocity.magnitude + " " + PopCanTrigger.CriticalVelocity);
			//death();
			hurtByCan(other);
		}
	}
	
	void move()
	{
		Vector3 move = Vector3.zero;
		
		if (Input.GetKey(KeyCode.A))
			move.x -= 1f;
		if (Input.GetKey(KeyCode.D))
			move.x += 1f;
		if (Input.GetKey(KeyCode.S))
			move.z -= 1f;
		if (Input.GetKey(KeyCode.W))
			move.z += 1f;
		
		if (move == Vector3.zero)
			return;
		
		float angle = Mathf.Atan2(move.z, move.x);
		
		move.x = MoveSpeed * Mathf.Cos(angle);
		move.z = MoveSpeed * Mathf.Sin(angle);
		
		transform.Translate(move * Time.deltaTime);
	}
	
	void clampPosition()
	{
		Vector3 position = new Vector3();
		
		position.x = Mathf.Clamp(transform.position.x, -(Ground.collider.bounds.size.x - 1) / 2 + collider.bounds.size.x / 2, (Ground.collider.bounds.size.x - 1) / 2 - collider.bounds.size.x / 2);
		position.z = Mathf.Clamp(transform.position.z, -(Ground.collider.bounds.size.z - 1) / 2 + collider.bounds.size.z / 2, (Ground.collider.bounds.size.z - 1) / 2 - collider.bounds.size.z / 2);
		position.y = Mathf.Max(.45f, transform.position.y);
		
		transform.position = position;
	}
	
	void rotate()
	{
		updateRotator();
	}
	
	void updateRotator()
	{
		//transform.Rotate(rotator.UpdateRotator(RotateSpeed));
		transform.Rotate(new Vector3(0f, rotator.UpdateRotator(RotateSpeed), 0f));
	}
	
	void shoot()
	{
		timeSinceLastShoot += Time.deltaTime;
		
		if (!jumping && Input.GetMouseButton(0) && timeSinceLastShoot >= ShootDelay)
		{
			timeSinceLastShoot = 0;
			
			Peanuts.Add(((Transform)Instantiate(Peanut, transform.position, transform.rotation)).gameObject);
			//GameObject peanut = Instantiate(Peanut);
		}
	}
	
	void jump()
	{
		if (!jumping)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				transform.position = new Vector3(transform.position.x, initialPosY + .01f, transform.position.z);
				rigidbody.AddForce(Vector3.up * JumpMod);
				jumping = true;
			}
		}
		else
		{
			if (transform.position.y <= initialPosY)
			{
				transform.position = new Vector3(transform.position.x, initialPosY, transform.position.z);
				jumping = false;
			}
		}
	}
	
	List<Vector2> respawnPoints;
    void generateRespawnPoints()
    {
        respawnPoints = new List<Vector2>();

        for (float x = transform.localScale.x / 2; x <= Ground.collider.bounds.size.x - transform.localScale.x / 2; x += Ground.collider.bounds.size.x / 10)
        {
            for (float z = transform.localScale.z / 2; z <= Ground.collider.bounds.size.z - transform.localScale.z / 2; z += Ground.collider.bounds.size.z / 10)
            {
                respawnPoints.Add(new Vector2(x - Ground.collider.bounds.size.x / 2, z - Ground.collider.bounds.size.z / 2));
            }
        }
    }
	
	/*void death()
	{
		int startIndex = Random.Range(0, respawnPoints.Count);
        float adjustedSpawnSafetyFactor = (Ground.GetComponent<GroundScript>().Width + Ground.GetComponent<GroundScript>().Height) / 2;

        while (true)
        {
            if (tryToRespawn(startIndex, adjustedSpawnSafetyFactor))
                break;
            adjustedSpawnSafetyFactor *= .9f;
        }
		
		transform.LookAt(Enemy.position);
		
		foreach (GameObject peanut in Peanuts)
		{
			if (peanut != null)
				Destroy(peanut);
		}
		
		Peanuts.Clear();
		
		Enemy.GetComponent<EnemyController>().HP += 5;
		
		rigidbody.velocity = Vector3.zero;
		
		audio.Play();
	}*/
	
	void hurtByCan(Collider can)
	{
		PopCanTrigger popCanTrigger = can.gameObject.GetComponent<PopCanTrigger>();
		
		if (popCanTrigger.TimeSinceHit < .5f)
			return;
		else
			popCanTrigger.TimeSinceHit = 0f;
		
		HP -= popCanTrigger.Damage;
		
		HurtGUITexture.guiTexture.color = new Color(HurtGUITexture.guiTexture.color.r, HurtGUITexture.guiTexture.color.g, HurtGUITexture.guiTexture.color.b, HurtAlpha);
		
		audio.Play();
	}
	
	bool tryToRespawn(int index, float factor)
    {
        for (int i = 0; i < respawnPoints.Count; i++, index++)
        {
			transform.position = new Vector3(respawnPoints[index % respawnPoints.Count].x, transform.position.y, respawnPoints[index % respawnPoints.Count].y);
			
			if (isNearEnemy(factor) || isNearPopCan(factor))
				continue;
			
            return true;
        }
        return false;
    }
	
	bool isNearEnemy(float factor)
	{
		return (Vector3.Distance(transform.position, Enemy.position) < (transform.localScale.x * factor));
	}
	
	bool isNearPopCan(float factor)
	{
		foreach (GameObject popCan in PopCanTrigger.MovingPopCans)
		{
			if (Vector3.Distance(transform.position, popCan.transform.position) < (transform.localScale.x * factor))
				return true;
		}
		return false;
	}
	
	void fadeHurtGUITexture()
	{
		if (HurtGUITexture.guiTexture.color.a == 0)
			return;
		
		HurtGUITexture.guiTexture.color = new Color(HurtGUITexture.guiTexture.color.r, HurtGUITexture.guiTexture.color.g, HurtGUITexture.guiTexture.color.b, HurtGUITexture.guiTexture.color.a - HurtFadeSpeed * Time.deltaTime);
	}
	
	public int HP
	{
		get
		{
			return hp;
		}
		set
		{
			hp = Mathf.Clamp(value, 0, MaxHp);
		}
	}
}
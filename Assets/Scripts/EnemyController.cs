using UnityEngine;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
	public static int MaxHp = 100;
	
	public Transform Ground;
	public Transform Player;
	public Transform PopCan;
	public float MoveSpeed;
	public float ShootDelay;
	public float ThrowForce;
	int hp = MaxHp;
	public float HPPercent { get; private set; }
	
	Vector2 moveTarget;
	float posY;
	float timeSinceLastShoot;
	
	void Start()
	{
		posY = transform.localScale.y / 2f;
		
		moveTarget = new Vector2();
		moveTarget.x = (Ground.collider.bounds.size.x - transform.localScale.x) * Random.value - Ground.collider.bounds.size.x / 2;
		moveTarget.y = (Ground.collider.bounds.size.z - transform.localScale.z) * Random.value - Ground.collider.bounds.size.z / 2;
		
		HPPercent = 1f;
	}
	
	void Update()
	{
		checkIfHitMoveTarget();
		
		moveTowardsMoveTarget();
		
		rotate();
		
		shoot();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Peanut")
		{
			HP -= other.GetComponent<PeanutController>().Damage;
			
			audio.Play();
			Destroy(other.gameObject);
		}
	}
	
	void checkIfHitMoveTarget()
	{
		if (transform.position.x == moveTarget.x && transform.position.z == moveTarget.y)
		{
			moveTarget.x = (Ground.collider.bounds.size.x - transform.localScale.x) * Random.value - (Ground.collider.bounds.size.x - transform.localScale.x) / 2;
			moveTarget.y = (Ground.collider.bounds.size.z - transform.localScale.z) * Random.value - (Ground.collider.bounds.size.z - transform.localScale.z) / 2;
		}
	}
	
	void moveTowardsMoveTarget()
	{
		Vector2 difference = moveTarget - new Vector2(transform.position.x, transform.position.z);

        float ratio = Mathf.Abs(difference.x) / Mathf.Abs(difference.y);

        float moveX = MoveSpeed * Time.deltaTime;
        float moveZ = MoveSpeed * Time.deltaTime;

        if (Mathf.Abs(difference.x) < moveX && Mathf.Abs(difference.y) < moveZ)
        {
            transform.position = new Vector3(moveTarget.x, posY, moveTarget.y);
            return;
        }
        else if (Mathf.Abs(difference.x) < moveX)
        {
            moveX = 0;
            if (difference.y < 0)
                moveZ = -moveZ;
        }
        else if (Mathf.Abs(difference.y) < moveZ)
        {
            moveZ = 0;
            if (difference.x < 0)
                moveX = -moveX;
        }
        else if (ratio > 1.0f)
        {
            moveZ = Mathf.Min(moveZ / ratio, moveZ);

            if (difference.x < 0)
                moveX = -moveX;
            if (difference.y < 0)
                moveZ = -moveZ;
        }
        else if (ratio < 1.0f)
        {
            moveX = Mathf.Min(moveX * ratio, moveX);

            if (difference.x < 0)
                moveX = -moveX;
            if (difference.y < 0)
                moveZ = -moveZ;
        }
        else
        {
            if (difference.x < 0)
                moveX = -moveX;
            if (difference.y < 0)
                moveZ = -moveZ;
		}
		
		transform.position = new Vector3(transform.position.x + moveX, posY, transform.position.z + moveZ);
	}
	
	void rotate()
	{
		transform.LookAt(Player);
	}
	
	void shoot()
	{
		timeSinceLastShoot += Time.deltaTime;
		
		if (timeSinceLastShoot >= ShootDelay)
		{
			timeSinceLastShoot = 0;
			
			Transform popCan = Instantiate(PopCan, transform.position + transform.forward * transform.collider.bounds.size.x / 2, transform.rotation) as Transform;
			
			popCan.Rotate(Random.insideUnitSphere);
			
			/*float angle = Mathf.Atan2 (Player.position.z - transform.position.z, Player.position.x - transform.position.x);
			
			popCan.gameObject.rigidbody.AddForce(new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * ThrowForce * Vector3.Distance(transform.position, Player.position));*/
			
			popCan.gameObject.rigidbody.AddForce((transform.forward * ThrowForce)  + (transform.forward * ThrowForce) * Vector3.Distance(transform.position, Player.position));
			
			PopCanTrigger.AddPopCan(popCan.gameObject);
		}
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
			
			HPPercent = hp / (float)MaxHp;
		}
	}
}

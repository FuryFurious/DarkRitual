using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBehaviour_Ranged : MonoBehaviour {

	public bool playerInSight = false;
	public float playerDistance = 5f;
	[HideInInspector]
	public GameObject player;
	public GameObject bulletPrefab;

	public Animator animator;

	public int enemyHealth = 50;

	// Movement variables
	float movementRange = 1;
	int stepMultiplier;
	bool isMoving = true;
	float movementTime;
	Vector2 movementDirection;
	Vector2 nullMovement2 = new Vector2 (0, 0);
	Vector3 nullMovement3 = new Vector3 (0, 0, 0);

	// Attack variables
	public float attackThreshold_maxValue = 4;
	float attackThreshold;
	//	public int enemyDamage = 10;

	public float speed = 1.0f;

	// Use this for initialization
	void Start () {
		stepMultiplier = Random.Range(1, 4);
		movementRange *= stepMultiplier;
		movementTime = movementRange;
		movementDirection = GetNewDirection ();

		attackThreshold = attackThreshold_maxValue;
		animator = GetComponent<Animator> ();
	}


	void FixedUpdate () {
		attackThreshold -= Time.deltaTime;
		if (attackThreshold < 0)
			attackThreshold = 0;

		// Run towards Player if in sight. Does only work with 1 Player right now
		if (playerInSight) {

			if (player == null)
			{
				playerInSight = false;
				return;
			}

			var heading = player.transform.position - gameObject.transform.position;
			var distance = heading.magnitude;



			if (attackThreshold < 0.1f) {
				GameObject spawnedBulled = (GameObject)GameObject.Instantiate (bulletPrefab, gameObject.transform.position, Quaternion.identity);
				Vector3 direction = heading / distance;
				direction.z = 0;

				//            Mathf.Atan2(direction.y, direction.x);
				spawnedBulled.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90.0f);
				//TODO: "attack"
				//player.GetComponent<Hea>().playerHealth-= enemyDamage;
				attackThreshold += attackThreshold_maxValue;
			}


			// Attack
			if (distance < 1.5f){


			}

			// Hold distance. Is already done via colliders
			if (distance > playerDistance){
				Vector3 direction = heading / distance;
				direction.z = 0;
				gameObject.transform.Translate (direction * Time.deltaTime * speed);
			}
			animator.SetBool("IsMoving", true);
		} 
		else {
			gameObject.transform.Translate (movementDirection * Time.deltaTime * speed, 0);
			movementTime -= Time.deltaTime;
			if (movementTime < 0) {
				if (Random.Range (0, 2) < 1)
				{
					movementDirection = GetNewDirection();
					animator.SetBool("IsMoving", true);
				}

				else
				{
					movementDirection = nullMovement2;
					animator.SetBool("IsMoving", false);
				}

				movementTime = movementRange;
			}

		}
	}

	Vector2 GetNewDirection(){

		return( Random.insideUnitCircle);
	}

}

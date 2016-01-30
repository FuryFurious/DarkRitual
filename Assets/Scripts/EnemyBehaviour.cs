﻿using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public bool playerInSight = false;
	public GameObject player;

	public int enemyHealth = 10;

	// Movement variables
	float movementRange = 1;
	int stepMultiplier;
	bool isMoving = true;
	float movementTime;
	Vector2 movementDirection;
	Vector2 nullMovement2 = new Vector2 (0, 0);
	Vector3 nullMovement3 = new Vector3 (0, 0, 0);

	// Attack variables
	public float attackThreshold_maxValue = 1;
	float attackThreshold;
	public int enemyDamage = 10;

	float speed = 0.5f;

	// Use this for initialization
	void Start () {
		stepMultiplier = Random.Range(1, 4);
		movementRange *= stepMultiplier;
		movementTime = movementRange;
		movementDirection = GetNewDirection ();

		attackThreshold = attackThreshold_maxValue;

	}
	void Update () {
		
	}

	void FixedUpdate () {
		// Run towards Player if in sight. Does only work with 1 Player right now
		if (playerInSight) {
			var heading = player.transform.position - gameObject.transform.position;
			var distance = heading.magnitude;
			//Debug.Log (distance);
			/*
			if (distance > 1.5f){
				// Attack
				if (attackThreshold < 1) {
					player.GetComponent<PlayerController>().playerHealth-= enemyDamage;

				}

			}
			*/
			if (distance > 1f){
				Vector3 direction = heading / distance;
				direction.z = 0;
				gameObject.transform.Translate (direction * Time.deltaTime * speed);
			}
		} 
		else {
			gameObject.transform.Translate (movementDirection * Time.deltaTime * speed, 0);
			movementTime -= Time.deltaTime;
			if (movementTime < 0) {
				if (Random.Range (0, 2) < 1)
					movementDirection = GetNewDirection ();
				else
					movementDirection = nullMovement2;
				movementTime = movementRange;
			}

		}
	}

	Vector2 GetNewDirection(){

		return( Random.insideUnitCircle);
	}

}
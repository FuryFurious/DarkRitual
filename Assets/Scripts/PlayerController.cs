using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public int playerHealth = 100;
	public float speed = 2f;
	SpriteRenderer sprite;
	public GameObject bulletPrefab;

	public List<GameObject> enemyList = new List<GameObject> ();


	public Vector3 mousePosition = new Vector3(0,0,0);

	// Use this for initialization
	void Start () {
		sprite = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

	}


	void FixedUpdate () {
		// Axis movement
		float axisH = Input.GetAxis ("Horizontal");
		float axisV = Input.GetAxis ("Vertical");
		// Mouse input
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = 0;
		var heading = mousePosition - gameObject.transform.position;
		var distance = heading.magnitude;
		Vector3 direction = heading / distance;
		//Debug.Log (mousePosition);

		// Shooting via left mouseclick
		if (Input.GetMouseButtonUp (0)) {
			GameObject spawnedBulled = (GameObject)GameObject.Instantiate (bulletPrefab, gameObject.transform.position, Quaternion.identity);
			spawnedBulled.GetComponent<Movement> ().direction = direction; 

//            Mathf.Atan2(direction.y, direction.x);
            spawnedBulled.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90.0f);
		}
			

		// Move via key input
		gameObject.transform.Translate (axisH * speed * Time.deltaTime, axisV * speed * Time.deltaTime, 0);
		// Flip vertically considering movement direction
		if(axisH < 0 && sprite.flipX == false)
			sprite.flipX = true;
		if (axisH > 0 && sprite.flipX)
			sprite.flipX = false;



	}
}
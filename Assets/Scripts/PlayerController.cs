using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {

	public int playerHealth = 100;
	public float speed = 2f;
	SpriteRenderer sprite;
	public GameObject bulletPrefab;

	public List<GameObject> enemyList = new List<GameObject> ();

    private Animator spriteAnimator;


	public Vector3 mousePosition = new Vector3(0,0,0);

	// Use this for initialization
	void Start () {
		sprite = gameObject.GetComponent<SpriteRenderer> ();

        spriteAnimator = gameObject.GetComponent<Animator>();
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
		if (Input.GetMouseButtonUp (0)) 
        {
			GameObject spawnedBulled = (GameObject)GameObject.Instantiate (bulletPrefab, gameObject.transform.position, Quaternion.identity);
			spawnedBulled.GetComponent<Movement> ().direction = direction; 

//            Mathf.Atan2(direction.y, direction.x);
            spawnedBulled.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90.0f);
		}
			

		// Move via key input
        Vector2 movementVector = new Vector2(axisH * speed * Time.deltaTime, axisV * speed * Time.deltaTime);
        gameObject.transform.Translate(movementVector.x, movementVector.y, 0);


        if (Mathf.Abs(axisH) > 0.25f || Mathf.Abs(axisV) > 0.25f)
        {
            spriteAnimator.SetBool("IsMoving", true);
        }

        else
        {
            spriteAnimator.SetBool("IsMoving", false);
        }


        // Flip vertically considering movement direction
        if (heading.x > 0.0f)
            sprite.flipX = true;

        else if (heading.x < 0.0f)
            sprite.flipX = false;

	}
}
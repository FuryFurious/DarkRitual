using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float Bulletspeed = 1f;
	public Vector2 direction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {

		gameObject.transform.position +=  new Vector3(direction.x * Bulletspeed * Time.deltaTime, direction.y * Bulletspeed * Time.deltaTime, 0);
	}
}

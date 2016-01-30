using UnityEngine;
using System.Collections;

public class CrosshairBehaviour : MonoBehaviour {


	Vector3 mousePosition = new Vector3(0,0,0);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {

		// Set position of the crosshair
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = 0;
		gameObject.transform.position = mousePosition;
	}


	void setPos(Vector2 pos)
	{
		
	}
}

using UnityEngine;
using System.Collections;

public class AttackRadius : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			GameObject enemyObject = gameObject.transform.parent.gameObject;
			playerObject.GetComponent<EnemyBehaviour> ().player = coll.gameObject;
			//playerObject.GetComponent<EnemyBehaviour> ().playerInSight = true;
		}
	}			

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player")
			gameObject.transform.parent.gameObject.GetComponent<EnemyBehaviour> ().playerInSight = false;		
	}

}

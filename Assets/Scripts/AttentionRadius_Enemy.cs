using UnityEngine;
using System.Collections;

public class AttentionRadius_Enemy : MonoBehaviour {

	//bool playerInSight = false;



	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//Collider2D collider = gameObject.GetComponent<CircleCollider2D> ();
		//collider.

	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			gameObject.transform.parent.gameObject.GetComponent<PlayerController> ().enemyList.Add (coll.gameObject);


			GameObject playerObject = gameObject.transform.parent.gameObject;
			playerObject.GetComponent<EnemyBehaviour> ().player = coll.gameObject;
			playerObject.GetComponent<EnemyBehaviour> ().playerInSight = true;
		}
	}	
			//playerInSight = true;			

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Enemy")
			gameObject.transform.parent.gameObject.GetComponent<EnemyBehaviour> ().playerInSight = false;		
	}
}

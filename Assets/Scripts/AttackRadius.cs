using UnityEngine;
using System.Collections;

public class AttackRadius : MonoBehaviour {

    EnemyBehaviour behave;

	// Use this for initialization
	void Start () {
        behave = gameObject.transform.parent.gameObject.GetComponent<EnemyBehaviour>();
	}



	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Enemy") {

            behave.player = coll.gameObject;
            behave.playerInSight = true;
		}
	}			

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player")
            behave.playerInSight = false;		
	}

}

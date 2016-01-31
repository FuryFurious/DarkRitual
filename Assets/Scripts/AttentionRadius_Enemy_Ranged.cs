using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyBehaviour_Ranged))]
public class AttentionRadius_Enemy_Ranged : MonoBehaviour {
	//bool playerInSight = false;
	private EnemyBehaviour_Ranged enemyBehaviour_Ranged;


	void Start () 
	{
		GameObject playerObject = gameObject.transform.parent.gameObject;
		enemyBehaviour_Ranged = playerObject.GetComponent<EnemyBehaviour_Ranged>();
	}


	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {

			enemyBehaviour_Ranged.player = coll.gameObject;
			enemyBehaviour_Ranged.playerInSight = true;
		}
	}	

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player")
			enemyBehaviour_Ranged.playerInSight = false;		
	}
}

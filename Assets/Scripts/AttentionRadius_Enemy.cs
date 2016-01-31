using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyBehaviour))]
public class AttentionRadius_Enemy : MonoBehaviour {

	//bool playerInSight = false;
    private EnemyBehaviour enemyBehaviour;


	void Start () 
    {
        GameObject playerObject = gameObject.transform.parent.gameObject;
        enemyBehaviour = playerObject.GetComponent<EnemyBehaviour>();
	}
	

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			
            enemyBehaviour.player = coll.gameObject;
            enemyBehaviour.playerInSight = true;
		}
	}	

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player")
            enemyBehaviour.playerInSight = false;		
	}
}

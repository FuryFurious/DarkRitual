using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class DamagerDealer : MonoBehaviour {

    public int damage;
    public string targetTag;

    public bool removeOnCollision = true;

    void OnCollisionStay2D(Collision2D collider)
    {
        HandleDamageStuff(collider.collider);
   
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        HandleDamageStuff(collider);
    }

    void HandleDamageStuff(Collider2D collider)
    {
        if (collider.tag == "Obstacle" && removeOnCollision)
        {
            //TODO: add possibility to play kill animation
            GameObject.Destroy(gameObject);
        }

        else if (collider.tag == targetTag)
        {
            Health health = collider.GetComponent<Health>();

            if (health)
            {
                health.ReceiveDamage(damage);

                if (health.health <= 0)
                {
                    health.DestroyGameObject();
                }
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Health : MonoBehaviour 
{

    public int health;
    public SpriteRenderer renderer;

    public float coloredDuration;
    private float curColoredDuration;

    private Color oldColor = Color.white;
    private bool restoredColor = false;



    public void DestroyGameObject()
    {
        //TODO: spawn / play dead stuff

        PortalConnection portalConnection = GetComponent<PortalConnection>();

        if (portalConnection)
        {
            portalConnection.OnDeath();
        }


        if (gameObject.tag == "Player")
            SceneManager.LoadScene("defaultScene");
          

        GameObject.Destroy(gameObject);
    }

    public void ReceiveDamage(int dmg)
    {
        health -= dmg;

        OnDamageReceived();
    }

    void Update()
    {

        if (!restoredColor)
        {
            curColoredDuration -= Time.deltaTime;

            if (curColoredDuration <= 0.0f)
            {
                if (renderer)
                {
                    renderer.color = oldColor;
                    restoredColor = true;
                }
            }
        }


    }

    public void OnDamageReceived()
    {
        if (renderer)
        {
            curColoredDuration = coloredDuration;

            if(restoredColor)
                oldColor = renderer.color;
            
            renderer.color = Color.red;
            restoredColor = false;
        }
    }
}

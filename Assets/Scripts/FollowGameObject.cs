using UnityEngine;
using System.Collections;

public class FollowGameObject : MonoBehaviour {

    public GameObject target;

    public float speed;
    public float minDistToFollow;


  //  public float a_curRumbleTime;
   // public float a_rumbleStrength;


    private float curRumbleTime;
    private float rumbleStrength;

    private bool playerExists = true;
	
	// Update is called once per frame
	void Update () 
    {
        if (curRumbleTime > 0.0f)
        {
            curRumbleTime -= Time.deltaTime;

            Vector2 offset = Random.insideUnitCircle;

            gameObject.transform.Translate(offset.x * rumbleStrength * Time.deltaTime, offset.y * rumbleStrength * Time.deltaTime, 0.0f);
        }

	}

    void FixedUpdate()
    {


        if (target != null)
        {
            Vector2 toTarget = new Vector2(target.transform.position.x, target.transform.position.y) - new Vector2(transform.position.x, transform.position.y);

            if (toTarget.magnitude > minDistToFollow)
            {
                gameObject.transform.Translate(toTarget.x * Time.deltaTime * speed, toTarget.y * Time.deltaTime * speed, 0);
            }
        }

        else
        {
            GameObject tmpTarget = GameObject.FindGameObjectWithTag("Player");

            if (tmpTarget)
            {
                this.target = tmpTarget;
                this.transform.position = new Vector3(this.target.transform.position.x, this.target.transform.position.y, this.transform.position.z);
            }
     

        }


    }

    public void Rumble(float time, float strength)
    {
        this.curRumbleTime = time;
        this.rumbleStrength = strength;
    }
}

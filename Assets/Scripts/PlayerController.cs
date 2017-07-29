using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : NetworkBehaviour
{

	public int playerHealth = 100;
	public float speed = 2f;
	SpriteRenderer sprite;
	public GameObject bulletPrefab;

	public List<GameObject> enemyList = new List<GameObject> ();

    private Animator spriteAnimator;


	public Vector3 mousePosition = new Vector3(0,0,0);

    public GameObject _cameraPrefab;

    private Camera _myCamera;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        if (isLocalPlayer)
        {
            var instance = Instantiate(_cameraPrefab);
            _myCamera = instance.GetComponent<Camera>();
            instance.GetComponent<FollowGameObject>().target = gameObject;

            sprite = gameObject.GetComponent<SpriteRenderer>();
            spriteAnimator = gameObject.GetComponent<Animator>();


            GetComponent<Rigidbody2D>().position = GameObject.Find("PlayerSpawnPos(Clone)").transform.position;
        }

       
    }


    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            // Axis movement
            float axisH = Input.GetAxisRaw("Horizontal");
            float axisV = Input.GetAxisRaw("Vertical");
            // Mouse input
            mousePosition = _myCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            var heading = mousePosition - gameObject.transform.position;
            var distance = heading.magnitude;
            Vector3 direction = heading / distance;
            //Debug.Log (mousePosition);

            
            // Shooting via left mouseclick
            if (Input.GetMouseButtonUp (0)) 
                CmdShoot(direction);
                

            // Move via key input
            Vector2 movementVector = new Vector2(axisH * speed * Time.deltaTime, axisV * speed * Time.deltaTime);
            gameObject.transform.Translate(movementVector.x, movementVector.y, 0);


            if (Mathf.Abs(axisH) > 0.25f || Mathf.Abs(axisV) > 0.25f)
            {
                spriteAnimator.SetBool("IsMoving", true);
            }

            else
            {
                spriteAnimator.SetBool("IsMoving", false);
            }


            // Flip vertically considering movement direction
            if (heading.x > 0.0f)
                sprite.flipX = true;

            else if (heading.x < 0.0f)
                sprite.flipX = false;

        }
    }

    [Command]
    private void CmdShoot(Vector2 direction)
    {
        GameObject spawnedBulled = (GameObject)GameObject.Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.identity);

        //            Mathf.Atan2(direction.y, direction.x);
        spawnedBulled.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90.0f);

        spawnedBulled.GetComponent<Rigidbody2D>().velocity = direction * 10.0f;

        NetworkServer.Spawn(spawnedBulled);
    }
}

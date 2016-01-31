using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class LevelRebuilder : MonoBehaviour {

  // [HideInInspector]
   //public  WorldGenerator generator;
    public string sceneName;

	// Use this for initialization
	void Start () 
    {
       // generator = GameObject.Find("WorldManager").GetComponent<WorldGenerator>();
       // Debug.Assert(generator);
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
           // generator.CreateNewWorld();
            SceneManager.LoadScene(sceneName);
            
        }
    }
	
	
}

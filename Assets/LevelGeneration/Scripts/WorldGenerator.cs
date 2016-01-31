using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldGenerator : MonoBehaviour 
{
    public DoAbstractWorldGenerator worldGenerator;

	
	// Update is called once per frame
	void Update () 
    {
        //generate new map:
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (worldGenerator != null)
            {
                worldGenerator.Init();
                worldGenerator.InterpretLevel();
            }
        
        }

        else if (Input.GetKeyDown(KeyCode.P))
        {
            if (worldGenerator != null)
            {
                worldGenerator.DoSteps();
                worldGenerator.InterpretLevel();
            }
        }




	}










}

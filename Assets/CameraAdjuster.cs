using UnityEngine;
using System.Collections;

public class CameraAdjuster : MonoBehaviour 
{	
	// Update is called once per frame
	void LateUpdate () 
    {
        transform.position = new Vector3(RoundToNearestPixel(gameObject.transform.position.x, Camera.main), RoundToNearestPixel(gameObject.transform.position.y, Camera.main), gameObject.transform.position.z);    
	}

    public static float RoundToNearestPixel(float unityUnits, Camera viewingCamera)
    {
        float valueInPixels = (Screen.height / (viewingCamera.orthographicSize * 2.0f)) * unityUnits;
        valueInPixels = Mathf.Round(valueInPixels);
        float adjustedUnityUnits = valueInPixels / (Screen.height / (viewingCamera.orthographicSize * 2.0f));
        return adjustedUnityUnits;
    }
}

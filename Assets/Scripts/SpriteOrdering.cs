using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteOrdering : MonoBehaviour {

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Debug.Assert(spriteRenderer);
    }

	void Update () 
    {
         spriteRenderer.sortingOrder = -(int)(gameObject.transform.position.y * 1000.0f);
	}
}

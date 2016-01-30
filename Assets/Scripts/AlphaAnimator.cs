using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class AlphaAnimator : MonoBehaviour {

    public float speed;
    private float curTime;

    
    public float minAlpha;
    public float maxAlpha;

    private SpriteRenderer spriteRenderer;

    

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        Debug.Assert(spriteRenderer != null);
    }

	// Update is called once per frame
	void Update () {

        curTime += Time.deltaTime * speed;
        float t = (Mathf.Sin(curTime) + 1.0f) * 0.5f;
        float curAlpha = ((1.0f - t) * minAlpha + t * maxAlpha);

        Color oldColor = spriteRenderer.color;
        spriteRenderer.color = new Color(oldColor.r, oldColor.g, oldColor.b, curAlpha);
	}
}

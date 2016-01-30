using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(ParticleSystem))]
public class PortalStoneTarget : MonoBehaviour {

    public float timeToFade;
    public PortalStoneManager manager;

    private float totalTimeToFade;

    private ParticleSystem particleSystem;
    private bool triggered;

    [Flags]
    public enum PortalColor 
    { 
        None = 0, 
        Red = 1, 
        Green = 2, 
        Blue = 4, 
        Yellow = 8, 
        Magenta = 16 
    }

    public PortalColor targetColor;


    void Start()
    {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        Debug.Assert(particleSystem);

        totalTimeToFade = timeToFade;
    }


    void Update()
    {
        if (triggered)
        {
            timeToFade -= Time.deltaTime;

            float t = timeToFade / totalTimeToFade;

            particleSystem.startColor = new Color(particleSystem.startColor.r, particleSystem.startColor.g, particleSystem.startColor.b, t);


            if (timeToFade <= 0)
            {
                
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
           PortalStoneHolder holder = other.gameObject.GetComponent<PortalStoneHolder>();

           Debug.Assert(holder != null);
           triggered = true;

        }
    }
}

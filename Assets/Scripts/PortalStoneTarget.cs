using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(ParticleSystem))]
public class PortalStoneTarget : MonoBehaviour 
{
    public static int NUM_COLORS = 5;

    public float timeToFade;
    private PortalStoneManager manager;

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

    public PortalColor GetColorFromNumber(int id)
    {
        switch (id)
        {
            case 0:
                return PortalColor.Red;

            case 1:
                return PortalColor.Blue;

            case 2:
                return PortalColor.Blue;

            case 3:
                return PortalColor.Blue;

            case 4:
                return PortalColor.Blue;

            default:
                break;
        }

        return PortalColor.None;
    }

    public PortalColor targetColor;


    void Start()
    {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        Debug.Assert(particleSystem);

        totalTimeToFade = timeToFade;

        manager = gameObject.transform.parent.GetComponent<PortalStoneManager>();
        Debug.Assert(manager);
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
                manager.AddColor(this.targetColor);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
           PortalStoneHolder holder = other.gameObject.GetComponent<PortalStoneHolder>();
           Debug.Assert(holder != null);
           if ((holder.color & this.targetColor) != PortalColor.None)
           {
               triggered = true;
           }

     
          

        }
    }


}

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
    public bool triggered;

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

    public static PortalColor GetColorFromNumber(int id)
    {
        switch (id)
        {
            case 0:
                return PortalColor.Blue;

            case 1:
                return PortalColor.Green;

            case 2:
                return PortalColor.Magenta;

            case 3:
                return PortalColor.Red;

            case 4:
                return PortalColor.Yellow;

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



}

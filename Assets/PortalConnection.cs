using UnityEngine;
using System.Collections;

public class PortalConnection : MonoBehaviour 
{
    public PortalStoneManager manager;

    public PortalStoneTarget.PortalColor color;

    void OnDeath()
    {
        manager.AddColor(color);
    }
}

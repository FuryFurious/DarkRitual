using UnityEngine;
using System.Collections;

public class PortalConnection : MonoBehaviour 
{
    public PortalStoneManager manager;

    public PortalStoneTarget.PortalColor color;

    public void OnDeath()
    {
        if (manager)
        {
            manager.AddColor(color);
        }
          
    }
}

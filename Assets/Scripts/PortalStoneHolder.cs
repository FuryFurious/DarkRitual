using UnityEngine;
using System.Collections;

public class PortalStoneHolder : MonoBehaviour {

    [HideInInspector]
    public PortalStoneTarget.PortalColor color;

    public void AddColor(PortalStoneTarget.PortalColor color)
    {
        this.color |= color;
    }
}

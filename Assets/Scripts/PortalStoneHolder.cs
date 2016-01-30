using UnityEngine;
using System.Collections;

public class PortalStoneHolder : MonoBehaviour {

    [HideInInspector]
    public PortalStoneTarget.PortalColor color;

    void Start()
    {
        //TODO: remove: debug
        color = ~PortalStoneTarget.PortalColor.None;
    }


    public void AddColor(PortalStoneTarget.PortalColor color)
    {
        this.color |= color;
    }
}

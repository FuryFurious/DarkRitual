using UnityEngine;
using System.Collections;

public class PortalStoneManager : MonoBehaviour {

    private PortalStoneTarget.PortalColor neededColors = PortalStoneTarget.PortalColor.Blue | PortalStoneTarget.PortalColor.Green | PortalStoneTarget.PortalColor.Magenta | PortalStoneTarget.PortalColor.Red | PortalStoneTarget.PortalColor.Yellow;

    public GameObject portalGameObject;




    public void AddColor(PortalStoneTarget.PortalColor usedColors)
    {
        this.neededColors = this.neededColors & (~usedColors);

        if (this.neededColors == PortalStoneTarget.PortalColor.None)
        {
            portalGameObject.SetActive(true);
        }
    }



}

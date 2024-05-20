using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onmobileaction : MonoBehaviour
{
    public void OnMouseUp()
    {
        playercontroller.instance.mobileplayerinput = playercontroller.MobilePlayerInput.idle;
    }
    public void OnMouseDown()
    {
        if (gameObject.name == "left")
        {
            playercontroller.instance.mobileplayerinput = playercontroller.MobilePlayerInput.left;
        }
        if (gameObject.name == "right")
        {
            playercontroller.instance.mobileplayerinput = playercontroller.MobilePlayerInput.right;
        }
        if (gameObject.name == "up")
        {
            playercontroller.instance.mobileplayerinput = playercontroller.MobilePlayerInput.up;
        }
        if (gameObject.name == "down")
        {
            playercontroller.instance.mobileplayerinput = playercontroller.MobilePlayerInput.down;
        }
    }


}

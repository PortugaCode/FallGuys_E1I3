using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTeam : MonoBehaviour
{
    public void SelectRed()
    {
        GameManager.instance.isRed = true;
        ChangeColor(GameManager.instance.isRed);
    }

    public void SelectBlue()
    {
        GameManager.instance.isRed = false;
        ChangeColor(GameManager.instance.isRed);
    }

    private void ChangeColor(bool isRed)
    {
        RoomPlayerControl[] rpcs = FindObjectsOfType<RoomPlayerControl>();

        foreach (RoomPlayerControl rpc in rpcs)
        {
            if (rpc.isLocalPlayer)
            {
                rpc.ChangeColor(isRed);
            }
        }
    }
}

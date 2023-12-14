using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTeam : MonoBehaviour
{
    public void SelectRed()
    {
        RoomPlayerControl[] rpcs = FindObjectsOfType<RoomPlayerControl>();

        foreach (RoomPlayerControl rpc in rpcs)
        {
            if (rpc.isLocalPlayer)
            {
                rpc.isRed = true;

                gameObject.SetActive(false);
            }
        }
    }

    public void SelectBlue()
    {
        RoomPlayerControl[] rpcs = FindObjectsOfType<RoomPlayerControl>();

        foreach (RoomPlayerControl rpc in rpcs)
        {
            if (rpc.isLocalPlayer)
            {
                gameObject.SetActive(false);
            }
        }
    }
}

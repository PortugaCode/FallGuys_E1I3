using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Mirror;

public class CameraControl : NetworkBehaviour
{
    [SerializeField] private CinemachineFreeLook cam;

    private void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            if (player.GetComponent<PlayerControl>().isLocalPlayer)
            {
                cam.Follow = player.gameObject.transform;
                cam.LookAt = player.gameObject.transform;
            }
        }
    }
}

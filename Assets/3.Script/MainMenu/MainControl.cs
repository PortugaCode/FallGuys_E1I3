using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControl : MonoBehaviour
{
    public void CreateRoom()
    {
        var manager = RoomManager.singleton;

        // if 첫 번째 플레이어면 (생성된 방이 없으면)
        manager.StartHost();

        // if 이미 생성된 방이 있으면
        //manager.StartClient();
    }
}

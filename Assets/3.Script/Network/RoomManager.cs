using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RoomManager : NetworkRoomManager
{
    public override void OnRoomServerConnect(NetworkConnectionToClient conn)
    {
        // 서버에서 새로 입장한 클라이언트를 감지했을 때 호출되는 함수
        base.OnRoomServerConnect(conn);

        // 클라이언트들에게 이 오브젝트가 생성되었음을 알림
        //NetworkServer.Spawn("게임오브젝트");
    }
}

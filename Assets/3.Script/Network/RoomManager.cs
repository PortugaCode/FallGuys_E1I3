using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RoomManager : NetworkRoomManager
{
    public override void OnRoomServerConnect(NetworkConnectionToClient conn)
    {
        // �������� ���� ������ Ŭ���̾�Ʈ�� �������� �� ȣ��Ǵ� �Լ�
        base.OnRoomServerConnect(conn);

        // Ŭ���̾�Ʈ�鿡�� �� ������Ʈ�� �����Ǿ����� �˸�
        //NetworkServer.Spawn("���ӿ�����Ʈ");
    }
}

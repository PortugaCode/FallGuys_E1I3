using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControl : MonoBehaviour
{
    public void CreateRoom()
    {
        var manager = RoomManager.singleton;

        // if ù ��° �÷��̾�� (������ ���� ������)
        manager.StartHost();

        // if �̹� ������ ���� ������
        //manager.StartClient();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            userNames = new string[8] { "", "", "", "", "", "", "", "" };
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // [Player Info]
    public int localIndex = -1;
    public string userName = string.Empty;
    public bool isRed = false;

    // [Room]
    private float[,] positions = new float[,] { { -5.0f, 1.75f }, { -1.75f, 1.75f }, { 1.75f, 1.75f }, { 5.0f, 1.75f }, { -5.0f, -1.75f }, { -1.75f, -1.75f }, { 1.75f, -1.75f }, { 5.0f, -1.75f } };

    // [Timer]
    public float timer;
    public float roundTime = 180.0f;
    private bool isTimerOn = false;

    // [Clients]
    public string[] userNames;
    public int redScore = 0;
    public int blueScore = 0;

    public void SetRoomPosition(GameObject obj, int index)
    {
        obj.transform.GetChild(0).gameObject.SetActive(true);
        obj.transform.position = new Vector3(positions[index, 0], positions[index, 1], 0);
        obj.transform.rotation = Quaternion.Euler(new Vector3(0, 180.0f, 0));

        localIndex = index;
    }

    public void ActivateModel()
    {
        GameObject[] roomPlayers = GameObject.FindGameObjectsWithTag("RoomPlayer");

        foreach (GameObject roomPlayer in roomPlayers)
        {
            roomPlayer.transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(FindName_co(roomPlayer));
        }
    }

    private IEnumerator FindName_co(GameObject roomPlayer)
    {
        while(true)
        {
            if (GameManager.instance.userNames[roomPlayer.GetComponent<RoomPlayerControl>().index] != string.Empty)
            {
                roomPlayer.transform.GetChild(3).GetComponent<TextMeshPro>().text = GameManager.instance.userNames[roomPlayer.GetComponent<RoomPlayerControl>().index];
                yield break;
            }

            yield return null;
        }
    }

    public void StartTimer()
    {
        if (timer == 0)
        {
            isTimerOn = true;
            timer = roundTime;
            StartCoroutine(Timer_co());
        }
    }

    private IEnumerator Timer_co()
    {
        while (timer > 0)
        {
            if (!isTimerOn)
            {
                timer = 0;
                yield break;
            }

            timer -= Time.deltaTime;
            yield return null;
        }

        timer = 0;
        isTimerOn = false;

        // 게임 종료

        yield break;
    }
}

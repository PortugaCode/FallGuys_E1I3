using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // [Player Info]
    public string userName = string.Empty;
    public bool isRed = false;
    public int currentRank = 0;

    // [Room]
    private float[,] positions = new float[,] { { -5.0f, 1.75f }, { -1.75f, 1.75f }, { 1.75f, 1.75f }, { 5.0f, 1.75f }, { -5.0f, -1.75f }, { -1.75f, -1.75f }, { 1.75f, -1.75f }, { 5.0f, -1.75f } };

    // [UI]
    private GameObject canvas;

    // [Timer]
    public float timer;
    public float roundTime = 180.0f;
    private bool isTimerOn = false;

    public void SetRoomPosition(GameObject obj, int index)
    {
        obj.transform.GetChild(0).gameObject.SetActive(true);
        obj.transform.position = new Vector3(positions[index, 0], positions[index, 1], 0);
        obj.transform.rotation = Quaternion.Euler(new Vector3(0, 180.0f, 0));
    }

    public void ActivateModel()
    {
        GameObject[] roomPlayers = GameObject.FindGameObjectsWithTag("RoomPlayer");

        foreach (GameObject roomPlayer in roomPlayers)
        {
            roomPlayer.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void ActivateRecord()
    {
        if (canvas == null)
        {
            canvas = GameObject.FindGameObjectWithTag("Canvas");
        }

        GameObject ui = canvas.transform.GetChild(currentRank).gameObject;
        ui.SetActive(true);
        ui.GetComponent<Text>().text = $"{currentRank + 1}등 - {userName}";
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

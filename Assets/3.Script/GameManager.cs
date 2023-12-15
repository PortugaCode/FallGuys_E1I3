using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool isRed = false;

    // [Room]
    private float[,] positions = new float[,] { { -5.0f, 1.75f }, { -1.75f, 1.75f }, { 1.75f, 1.75f }, { 5.0f, 1.75f }, { -5.0f, -1.75f }, { -1.75f, -1.75f }, { 1.75f, -1.75f }, { 5.0f, -1.75f } };

    public void SetRoomPosition(GameObject obj, int index)
    {
        obj.transform.position = new Vector3(positions[index, 0], positions[index, 1], 0);
    }
}

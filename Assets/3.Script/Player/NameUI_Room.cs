using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class NameUI_Room : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private TextMeshPro nameText;

    private void Start()
    {
        mainCamera = Camera.main;
        nameText.text = GameManager.instance.userName;
    }

    private void Update()
    {
        if (mainCamera == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
        }
    }
}

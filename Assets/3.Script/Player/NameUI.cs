using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class NameUI : NetworkBehaviour
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
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
}

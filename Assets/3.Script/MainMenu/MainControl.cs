using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainControl : MonoBehaviour
{
    [SerializeField] private InputField inputField;

    public void SaveUserName()
    {
        GameManager.instance.userName = inputField.text;
    }
}

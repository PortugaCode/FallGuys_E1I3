using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTeam : MonoBehaviour
{
    public void SelectRed()
    {
        GameManager.instance.isRed = true;
        gameObject.SetActive(false);
    }

    public void SelectBlue()
    {
        gameObject.SetActive(false);
    }
}

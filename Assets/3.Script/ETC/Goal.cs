using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<PlayerControl>().isGoal)
            {
                other.GetComponent<PlayerControl>().isGoal = true;
                other.GetComponent<PlayerControl>().Goal(GameManager.instance.localIndex);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)    // collide with player
        {
            // Communicate to game manager that player is finished
            GameObject.FindObjectOfType<GameManager>().GameWin();
        }
    }
}

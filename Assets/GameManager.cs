using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void GameLose()
    {
        Debug.Log("Game Over");
        // Soundeffect for losing
    }
    
    public void GameWin()
    {
        Debug.Log("Buoy found, Game Won!");
        // Soundeffect for winning
    }
}

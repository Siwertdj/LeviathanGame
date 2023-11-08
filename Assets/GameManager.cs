using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private AudioSource _globalAudio;
    private bool _gameStarted;

    [SerializeField] AudioClip intro;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip ending;

    private GameObject player;
    private AudioSource _buoy;
    private GameObject monster;
    
    private void Start()
    {
        _globalAudio = GetComponent<AudioSource>();
        _buoy = GameObject.FindGameObjectWithTag("Buoy").GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        monster = GameObject.FindGameObjectWithTag("Monster");
        setPlayerActive(false);
        // Soundeffect for the intro
        _globalAudio.PlayOneShot(intro);
    }

    private void Update()
    {
        if (!_globalAudio.isPlaying && !_gameStarted)
        {
            setPlayerActive(true);
            _gameStarted = true;
        }
    }

    public void GameLose()
    {
        Debug.Log("Game Over");
        setPlayerActive(false);
        // Soundeffect for losing
        _globalAudio.PlayOneShot(death);
    }
    
    public void GameWin()
    {
        Debug.Log("Buoy found, Game Won!");
        _buoy.volume = 0.3f;
        monster.SetActive(false);
        setPlayerActive(false);
        // Soundeffect for winning
        _globalAudio.PlayOneShot(ending);
    }

    private void setPlayerActive(bool set)
    {
        player.GetComponent<PlayerJoyStickController>().enabled = set;
    }
}

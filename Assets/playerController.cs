using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class playerController : MonoBehaviour
{
    CharacterController playerControl;
    public float turnSpeed;
    public float speed;

    public GameObject leftOar;
    public GameObject rightOar;
    public AudioSource rightSound;
    public AudioSource leftSound;
    public AudioSource centerStage;

    public AudioClip[] splashes;
    public AudioClip splash1;
    public AudioClip splash2;
    public AudioClip splash3;
    public AudioClip splash4;
    public AudioClip splash5;
    public AudioClip splash6;

    void Start()
    {
        rightSound = rightOar.GetComponent<AudioSource>();
        leftSound = leftOar.GetComponent<AudioSource>();
        centerStage = GetComponent<AudioSource>();

        playerControl = GetComponent<CharacterController>();
        splashes = new AudioClip[] {
            splash1,
            splash2,
            splash3,
            splash4,
            splash5,
            splash6,
            /*
            (AudioClip)Resources.Load("Splashes/Footsteps_Water_Jump_Light_01.wav"),
            (AudioClip)Resources.Load("Splashes/Footsteps_Water_Jump_Light_02.wav"),
            (AudioClip)Resources.Load("Splashes/Footsteps_Water_Jump_Light_03.wav"),
            (AudioClip)Resources.Load("Splashes/Footsteps_Water_Jump_Light_04.wav"),
            (AudioClip)Resources.Load("Splashes/Footsteps_Water_Jump_Light_05.wav"),
            (AudioClip)Resources.Load("Splashes/Footsteps_Water_Jump_Light_06.wav"),
            //(AudioClip)Resources.Load("Other/Growl.wav")
            */
        };
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int soundIndexLeft = Random.Range(0, 5);
        int soundIndexRight = Random.Range(0, 5);
        while (soundIndexLeft == soundIndexRight)
            soundIndexRight = Random.Range(0, 5);

        if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
        {
            if ((!leftSound.isPlaying && !rightSound.isPlaying))
            {
                playerControl.Move(gameObject.transform.rotation * Vector3.forward * speed);
                leftSound.PlayOneShot(splashes[soundIndexLeft]);
                rightSound.PlayOneShot(splashes[soundIndexRight]);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (!leftSound.isPlaying)
            {
                gameObject.transform.rotation *= Quaternion.Euler(0, -turnSpeed, 0);
                leftSound.PlayOneShot(splashes[soundIndexLeft]);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (!rightSound.isPlaying)
            {
                gameObject.transform.rotation *= Quaternion.Euler(0, turnSpeed, 0);
                rightSound.PlayOneShot(splashes[soundIndexRight]);
            }
        }
    }
}

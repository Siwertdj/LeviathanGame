using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    
    [SerializeField] float turnSpeed;
    [SerializeField] float speed;

    private AudioSource _leftSound;
    private AudioSource _rightSound;
    private AudioSource _centerStage;

    [SerializeField] AudioClip[] splashes;
    [SerializeField] AudioClip splash1;
    [SerializeField] AudioClip splash2;
    [SerializeField] AudioClip splash3;
    [SerializeField] AudioClip splash4;
    [SerializeField] AudioClip splash5;
    [SerializeField] AudioClip splash6;

    void Start()
    {
        // find all audiosources in the children of Player
        AudioSource[] sources = GetComponentsInChildren<AudioSource>();
        _leftSound = sources[0];
        _rightSound = sources[1];
        _centerStage = sources[2];

        _rigidbody = GetComponent<Rigidbody>();
        
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
            if ((!_leftSound.isPlaying && !_rightSound.isPlaying))
            {
                Debug.Log("Row");
                
                Vector3 moveDirection = (gameObject.transform.rotation * Vector3.forward).normalized;
                
                _rigidbody.AddForce(moveDirection * speed);
                _leftSound.PlayOneShot(splashes[soundIndexLeft]);
                _rightSound.PlayOneShot(splashes[soundIndexRight]);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (!_leftSound.isPlaying)
            {
                gameObject.transform.rotation *= Quaternion.Euler(0, -turnSpeed, 0); 
                _leftSound.PlayOneShot(splashes[soundIndexLeft]);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (!_rightSound.isPlaying)
            {
                gameObject.transform.rotation *= Quaternion.Euler(0, turnSpeed, 0);
                _rightSound.PlayOneShot(splashes[soundIndexRight]);
            }
        }
        
    }

}

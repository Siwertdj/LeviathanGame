using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJoyStickController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] float turnSpeed;
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    [Tooltip("The force put on the rowboat when using the keys of a keyboard")]
    [SerializeField] float _keyForce;
    private bool _debounceLeft = false;
    private bool _debounceRight = false;

    [Tooltip("How far to the side the sticks must be pushed to register input (0-1)")]
    [SerializeField] float _deadStickRadius;

    private Vector2 _lastLeftVector;
    private Vector2 _lastRightVector;
    
    private PlayerInputs _inputs;
    private InputAction _rowLeft;
    private InputAction _rowRight;

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

    [SerializeField] float _splashVolumeFactor;

    private void Awake()
    {
        _inputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        _rowLeft = _inputs.Player.RowLeft;
        _rowRight = _inputs.Player.RowRight;
        _rowLeft.Enable();
        _rowRight.Enable();
    }

    private void OnDisable()
    {
        _rowLeft.Disable();
        _rowRight.Disable();
    }

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
            splash6
        };
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 currentLeft = _rowLeft.ReadValue<Vector2>();
        Vector2 currentRight = _rowRight.ReadValue<Vector2>();

        float LeftForce = 0;
        float RightForce = 0;

        int soundIndexLeft = Random.Range(0, 5);
        int soundIndexRight = Random.Range(0, 5);
        while (soundIndexLeft == soundIndexRight)
            soundIndexRight = Random.Range(0, 5);

        // Left Input
        {
            if (Input.GetKey(KeyCode.A) && !_debounceLeft)
            {
                _debounceLeft = true;
                LeftForce = _keyForce;
                _leftSound.PlayOneShot(splashes[soundIndexLeft], Mathf.Abs(LeftForce) * _splashVolumeFactor);
            }
            else if (!Input.GetKey(KeyCode.A))
                _debounceLeft = false;

            if (_lastLeftVector.magnitude > _deadStickRadius && currentLeft.magnitude > _deadStickRadius)
            {
                if (currentLeft.x > 0)
                {
                    LeftForce = currentLeft.y - _lastLeftVector.y;
                    if (Mathf.Abs(currentLeft.y) < 0.2)
                        _leftSound.PlayOneShot(splashes[soundIndexLeft], Mathf.Abs(LeftForce) * _splashVolumeFactor);
                }
            }
        }

        // Right Input
        {
            if (Input.GetKey(KeyCode.D) && !_debounceRight)
            {
                _debounceRight = true;
                RightForce = _keyForce;
                _rightSound.PlayOneShot(splashes[soundIndexRight], Mathf.Abs(RightForce) * _splashVolumeFactor);
            }
            else if (!Input.GetKey(KeyCode.D))
                _debounceRight = false;

            if (_lastRightVector.magnitude > _deadStickRadius && currentRight.magnitude > _deadStickRadius)
            {
                if (currentRight.x < 0)
                {
                    RightForce = currentRight.y - _lastRightVector.y;
                    if (Mathf.Abs(currentRight.y) < 0.2)
                        _rightSound.PlayOneShot(splashes[soundIndexRight], Mathf.Abs(RightForce) * _splashVolumeFactor);
                }
            }
        }

        _rigidbody.AddTorque(gameObject.transform.rotation * Vector3.up * (LeftForce - RightForce) * turnSpeed, ForceMode.Acceleration);
        _rigidbody.AddForce(_rigidbody.rotation * Vector3.forward * (LeftForce + RightForce) * (_rigidbody.velocity.magnitude > maxSpeed ? 0 : speed), ForceMode.Force);

        _lastLeftVector = currentLeft;
        _lastRightVector = currentRight;
    }

}

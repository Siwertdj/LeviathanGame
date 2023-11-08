using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterBehaviour : MonoBehaviour
{
    [SerializeField] private Transform planet;

    [SerializeField] private float attackReach;
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private float moveDelay;
    private float _timer;
    private bool warned;
    
    private Rigidbody _rigidbody;
    private CapsuleCollider _capsuleCollider;

    private AudioSource source;
    [SerializeField] private AudioClip snarl;
    [SerializeField] private AudioClip idle;
    [SerializeField] private float _volumeScale;


    // Start is called before the first frame update
    private void Awake()
    {
        FindSpawn();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        source = GetComponent<AudioSource>();
        Physics.IgnoreCollision(_capsuleCollider, GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>());
        Physics.IgnoreLayerCollision(7, 8);
        source.Play();
        source.loop = true;
    }

    void FixedUpdate()
    {
        //this makes the monster swim at intervals
        if (_timer >= moveDelay)
        {
            Move();
            _timer = 0;
        }

        _timer += Time.deltaTime;
    }

    void Move()
    {
        Vector3 moveDirection = (gameObject.transform.rotation * Vector3.forward).normalized;
        _rigidbody.AddForce(moveDirection * (movementSpeed));
    }
    
    public void RotateToSound(Vector3 location)
    {
        //find position from gameObject.transform.position to location
        
        // rotate towards sound
        // perpendicularity to sphere is done by the GravityBody of this gameobject.
        gameObject.transform.LookAt(location);
        // it will rotate towards the sound, then keep moving into that direction, even if it passes that point
        if (Vector3.Distance(location, gameObject.transform.position) <= attackReach)
        {
            Attack();
        }
    }

    void Attack()
    {
        // TODO: Play monster sound here.
        // TODO: Implement a warning to the player
            // if (warningGiven) { ... lose }
        if (!warned)
        {
            warned = true;
            source.PlayOneShot(snarl, _volumeScale);
        }
        else
        {
            // game over
            FindObjectOfType<GameManager>().GameLose();
        }

    }
    
    void FindSpawn()
    {
        Vector3 randomDir;

        // Generate random values for x, y, and z dimensions, between -1 and 1
        float x = Random.Range(-1, 1);
        float y = Random.Range(-1, 1);
        float z = Random.Range(-1, 1);

        randomDir = new Vector3(x, y, z).normalized;

        float radius = Math.Max(Math.Max(planet.localScale.x, planet.localScale.y), planet.localScale.z);
        
        gameObject.transform.position = randomDir * radius;
    }
}

//namespaces that give access to libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //create variables here, public or private reference. there will be a data type (int, float, bool, strings)
    //every variable has a name
    //optional a value assigned to a variable
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    //store gameobject in reference
    private SpawnManager _spawnManager;

    //bool for isTripleShotActive
    //where do we handle it? Inside fire laser.
    bool isTripleShotActive = false;
    bool isShieldActive = false;

    //variable reference to Shield Visualizer
    [SerializeField]
    private GameObject _tripleShot;

    [SerializeField]
    private GameObject _shieldVisualizer;



    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position (0, 0, 0)
        //scripts get added as components
        // access other components when dragged in
        transform.position = new Vector3(0, 0, 0);
        //vector 3 defines all position types
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _shieldVisualizer.SetActive(false);
        //getting access to spawn manager script
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }

    }

    // Update is called once per frame 60 frames/sec
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
        


    }

    public void SpeedBoost()
    {
        StartCoroutine("SpeedBoostActivate");
    }

    IEnumerator SpeedBoostActivate()
    {
        _speed += 5f;
        yield return new WaitForSeconds(5f);
        _speed -= 5f;
    }

    public void ShieldsPowerUp(){
        StartCoroutine("ShieldsWorking");
    }

    IEnumerator ShieldsWorking(){
        isShieldActive = true;
        _shieldVisualizer.SetActive(true);
        yield return new WaitForSeconds(20f);
        isShieldActive = false;
        _shieldVisualizer.SetActive(false);

    }

    void CalculateMovement()
    {
              //local variable, store input to access below
        //Get hierarchy terms from Edit/Project Settings/Input Manager
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Vector3.right = new Vector3(1, 0, 0), it's a shortcut
        //convert per frame to per second, time it takes to convert last frame to current frame
        transform.Translate(new Vector3(-_speed, 0, 0) * horizontalInput * Time.deltaTime);
        transform.Translate(new Vector3(0, _speed, 0) * verticalInput * Time.deltaTime);
        //above could be turned into one line of code, horizontal and vertical inputs as numbers in vector parenthesis

        //player bounds

        //to clamp instead of using the following if/else:
        //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3, 0), transform.position.z)

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else if (transform.position.y < -3)
        {
            transform.position = new Vector3(transform.position.x, -3, transform.position.z);
        }

        //restrain player to horizontal bounds of window

        if (transform.position.x > 11.28)
        {
            transform.position = new Vector3(-11, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -11.28)
        {
            transform.position = new Vector3(11, transform.position.y, transform.position.z);
        }

    }

    public void TripleShotActivate(){
        StartCoroutine("TripleShot");
    }

    IEnumerator TripleShot(){
        isTripleShotActive = true;
        yield return new WaitForSeconds(5f);
        isTripleShotActive = false;

    }

    void FireLaser() {
        
            _canFire = Time.time + _fireRate;
            //Quaternion.identity = default rotation of the prefab
            
            if (isTripleShotActive == false)
            {
                 Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);

            }
            else {
                Instantiate(_tripleShot, transform.position + new Vector3(-.770f, 0.16f, 0), Quaternion.identity);

            }
            //Time.time is how long the game has been running

            //if triple shot active is true, fire three lasers instead of one

    }

    public void Damage()
    {
        if (isShieldActive == true)
        {
            isShieldActive = false;
            _shieldVisualizer.SetActive(false);

        }
        else if (isShieldActive == false)
        {
        _lives -= 1;

        if (_lives < 1)
        {
            //Communicate with Spawn Manager to stop spawning
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
        }
    }
}

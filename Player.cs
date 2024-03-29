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
    private int _lives = 1;
    //store gameobject in reference
    private SpawnManager _spawnManager;
    [SerializeField]
    private int _score;

    private UIManager _UIManager;

    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioClip _powerUpSoundClip;

    [SerializeField]
    private AudioSource _audioSource;

    //bool for isTripleShotActive
    //where do we handle it? Inside fire laser.
    bool isTripleShotActive = false;
    bool isShieldActive = false;

    //variable reference to Shield Visualizer
    [SerializeField]
    private GameObject _tripleShot;

    [SerializeField]
    private GameObject _shieldVisualizer;

    public SaveObject so;

    //handle to player animation




    // Start is called before the first frame update
    void Start()
    {
        //assign handle to player animation
        //_playerAnimation.Move()

        transform.position = new Vector3(0, 5, 0);
        //vector 3 defines all position types
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        _shieldVisualizer.SetActive(false);
        //getting access to spawn manager script
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }

        if (_UIManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source on the player is NULL");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
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
        _audioSource.clip = _powerUpSoundClip;
        _audioSource.Play();
        StartCoroutine("SpeedBoostActivate");
    }

    IEnumerator SpeedBoostActivate()
    {
        _speed += 5f;
        yield return new WaitForSeconds(1f);
         _audioSource.clip = _laserSoundClip;
        yield return new WaitForSeconds(5f);
        _speed -= 5f;
    }

    public void ShieldsPowerUp(){
        _audioSource.clip = _powerUpSoundClip;
        _audioSource.Play();
        StartCoroutine("ShieldsWorking");
    }

    IEnumerator ShieldsWorking(){
        // isShieldActive = true;
        if (_lives < 3){
         _lives += 1;
        _UIManager.UpdateLives(_lives);
        so.playerLives = _lives;
        }

        // _shieldVisualizer.SetActive(true);
        yield return new WaitForSeconds(1f);
         _audioSource.clip = _laserSoundClip;

        yield return new WaitForSeconds(20f);
        // isShieldActive = false;
        // _shieldVisualizer.SetActive(false);

    }

    public void TripleShotActivate(){
        _audioSource.clip = _powerUpSoundClip;
        _audioSource.Play();
        StartCoroutine("TripleShot");
    }

    IEnumerator TripleShot(){
        isTripleShotActive = true;
        yield return new WaitForSeconds(0.5f);
         _audioSource.clip = _laserSoundClip;
        yield return new WaitForSeconds(5f);
        isTripleShotActive = false;

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

        if (transform.position.y > 7)
        {
            transform.position = new Vector3(transform.position.x, 7, transform.position.z);
        }
        else if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
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


    void FireLaser() {
        
            _canFire = Time.time + _fireRate;
            //Quaternion.identity = default rotation of the prefab
            
            if (isTripleShotActive == false)
            {
                 Instantiate(_laserPrefab, transform.position + new Vector3(0, -0.7f, 0), Quaternion.identity);

            }
            else {
                Instantiate(_tripleShot, transform.position + new Vector3(-.770f, -1f, 0), Quaternion.identity);

            }

            _audioSource.Play();
            //Time.time is how long the game has been running

            //if triple shot active is true, fire three lasers instead of one

            //play the laser audio clip
            //variable to store the audio clip, figure out how to play it

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
        _UIManager.UpdateLives(_lives);


        if (_lives < 1)
        {
            //Communicate with Spawn Manager to stop spawning
            _spawnManager.OnPlayerDeath();
            _UIManager.PlayerDeath();
            Destroy(this.gameObject);
        }
        }
    }

    public void ScorePoints()
    {
        _UIManager.AddPoints(10);
    }

    //add ten to score
    //communicate to UI manager
}

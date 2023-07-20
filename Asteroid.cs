using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotSpeed = 0;

    private SpawnManager _spawnManager;
    private AudioSource _audioSource;
    Animator _animator;



    // [SerializeField]
    // private GameObject _explosion;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _animator = gameObject.GetComponent<Animator>();
        _audioSource = gameObject.GetComponent<AudioSource>();


        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,0) * _rotSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Laser"){
            Debug.Log("Laser");
            Destroy(other.gameObject);
            StartCoroutine("StartingGame");
            // GameObject newExplosion = Instantiate(_explosion, transform.position, Quaternion.identity);
      


        }
    }


           IEnumerator StartingGame()
        {
            _animator.SetTrigger("StartGame");
            _audioSource.Play();
            yield return new WaitForSeconds(0.7f);
            _spawnManager.startSpawn();
            Destroy(this.gameObject);

        }

}



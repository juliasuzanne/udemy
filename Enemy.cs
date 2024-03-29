using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 2f;


    [SerializeField]
    private AudioClip _explosionSoundClip;
    [SerializeField]
    private AudioSource _audioSource;

    private Player _player;
    Animator _animator;

    //handle to animator component

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = gameObject.GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();


         if (_player == null)
        {
            Debug.LogError("The player is NULL");
        }

         if (_animator == null)
        {
            Debug.LogError("The animator is NULL");
        }

            if (_audioSource == null)
        {
            Debug.LogError("Audio Source on the player is NULL");
        }
        else
        {
            _audioSource.clip = _explosionSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //move down 4m/s
        //check if bottom at screen, respawn at top with a new random x position
        transform.Translate(new Vector3(0, _enemySpeed, 0) * Time.deltaTime);

        if (transform.position.y > 8)
        {
            float randomX = Random.Range(-7f, 7f);
            transform.position = (new Vector3(randomX, -3, 0));

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.transform.name);
        //damage player first so the script will still run.
        Debug.Log(other.tag);

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player!=null)
            {
                player.Damage();
                _audioSource.Play();

            }
            //anim trigger here
           StartCoroutine("EnemyDestroy");
        
        }

        //if other is laser
        //destroy enemy and laser (laser first)
        if (other.tag == "Laser")
        {

            Destroy(other.gameObject);
            _player.ScorePoints();
            StartCoroutine("EnemyDestroy");

        }

    }

       IEnumerator EnemyDestroy()
        {
            while(this != null){
            _animator.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            _audioSource.Play();

            yield return new WaitForSeconds(1.0f);
            Destroy(this.gameObject);}

        }
}

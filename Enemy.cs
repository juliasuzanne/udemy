using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = -2f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //move down 4m/s
        //check if bottom at screen, respawn at top with a new random x position
        transform.Translate(new Vector3(0, _enemySpeed, 0) * Time.deltaTime);

        if (transform.position.y < -6)
        {
            float randomX = Random.Range(-7f, 7f);
            transform.position = (new Vector3(randomX, 6, 0));

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.transform.name);
        //if other is Player
        //damage the Player
        //destroy this (enemy)
        //damage player first so the script will still run.
        Debug.Log(other.tag);

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player!=null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }

        //if other is laser
        //destroy enemy and laser (laser first)
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }



    }
}

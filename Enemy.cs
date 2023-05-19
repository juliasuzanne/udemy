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
        transform.position = (new Vector3(Random.Range(-7f,7f), 2, 0));
    }

    // Update is called once per frame
    void Update()
    {
        //move down 4m/s
        //check if bottom at screen, respawn at top with a new random x position
        transform.Translate(new Vector3(0, _enemySpeed, 0) * Time.deltaTime);



    }
}

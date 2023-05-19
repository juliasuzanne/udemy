using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnRoutine");
    }

    // Update is called once per frame
    void Update()
    {
    }

    //spawn game objects every 5 seconds
    //create a coroutine of type IEnumerator -- Yield Events
    //while loop

    IEnumerator SpawnRoutine()
    {
        while (true)
        {            
        Vector3 posToSpawn = new Vector3(Random.Range(-7f, 7f), 7, 0);
        Instantiate(_enemy, posToSpawn, Quaternion.identity);
        yield return new WaitForSeconds(5f);
        }
        // always use infinite loops because we can use yield events
        // instantiate referenced object, enemy prefab
        // yield, wait for 5 seconds

    }

}

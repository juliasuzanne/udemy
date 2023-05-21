using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;

    [SerializeField]
    private GameObject[] _powerUps;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnRoutine");
        StartCoroutine("SpawnPowerUpRoutine");
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
        while (_stopSpawning == false)
        {            
        Vector3 posToSpawn = new Vector3(Random.Range(-7f, 7f), 7, 0);
        GameObject newEnemy = Instantiate(_enemy, posToSpawn, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
        //parent is a type transform, not a gameObject (enemyContainer is a gameObject)
        yield return new WaitForSeconds(5f);
        }
        // always use infinite loops because we can use yield events
        // instantiate referenced object, enemy prefab
        // yield, wait for 5 seconds

    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while(_stopSpawning == false)
        {
        Vector3 posToSpawnPowerUp = new Vector3(Random.Range(-3f, 3), 7, 0);
        GameObject newPowerUp = Instantiate(_powerUps[Random.Range(0, 3)], posToSpawnPowerUp, Quaternion.identity);
        yield return new WaitForSeconds(10f);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }


}

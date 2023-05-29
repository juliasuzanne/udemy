using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotSpeed = 3;

    [SerializeField]
    private GameObject _explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,1) * _rotSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Laser"){
            Debug.Log("Laser");
            Destroy(other.gameObject);
            GameObject newExplosion = Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.5f);

        }
    }

}



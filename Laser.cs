using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //speed variable of 8f
    [SerializeField]
    private float _speed = 8f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //translate laser up
        transform.Translate(new Vector3(0, _speed, 0) * Time.deltaTime);

        //if laser position > 8 on the y
        //destroy object

        if (transform.position.y > 7.1)
        {
            Debug.Log("destroy");
            Destroy(gameObject);

        }
    }
}

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
    private float _speed = 4f; //default value of 0



    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position (0, 0, 0)
        //scripts get added as components
        // access other components when dragged in
        transform.position = new Vector3(0, 0, 0);
        //vector 3 defines all position types
    }

    // Update is called once per frame 60 frames/sec
    void Update()
    {

        //local variable, store input to access below
        //Get hierarchy terms from Edit/Project Settings/Input Manager
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Vector3.right = new Vector3(1, 0, 0), it's a shortcut
        //convert per frame to per second, time it takes to convert last frame to current frame
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(new Vector3(0, _speed, 0) * verticalInput * Time.deltaTime);
        //above could be turned into one line of code, horizontal and vertical inputs as numbers in vector parenthesis

        //player bounds
        //pseudo code - if player position on the y is greater than zero,
        //y pos = 0

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
}

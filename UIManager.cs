using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private int _score;

    [SerializeField]
    private Text _scoreText;
    // Start is called before the first frame update
    void Start()
    {
        _score = 50;
        _scoreText.text = "Score: " + _score;
        
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = "Score: " + _score;
        
    }

    public void AddPoints(int numToAdd){
        _score += numToAdd;
    }

}

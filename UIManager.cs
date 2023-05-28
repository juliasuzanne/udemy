using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    private int _score;

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Text _restartText;

    [SerializeField]
    private Image _livesImage;

    [SerializeField]
    private Sprite[] _lifeSprites;

    private bool _gameOver = false;



    // Start is called before the first frame update
    void Start()
    {
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _livesImage.sprite = _lifeSprites[3];
        _score = 50;
        _scoreText.text = "Score: " + _score;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        _scoreText.text = "Score: " + _score;

        if (_gameOver == true)
        {if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene("Space");
        }}
        
    }

    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _lifeSprites[currentLives];
    }

    public void PlayerDeath()
    {
        _restartText.gameObject.SetActive(true);
        _gameOver = true;
        StartCoroutine("GameOverFlicker");

    }

    IEnumerator GameOverFlicker(){
        while(true)
       { _gameOverText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        _gameOverText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);}



    }

    public void AddPoints(int numToAdd){
        _score += numToAdd;
    }

}

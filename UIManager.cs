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

    [SerializeField]
    private GameObject _panel;

    GameManager _gameManager;



    // Start is called before the first frame update
    void Start()
    {
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _panel.gameObject.SetActive(false);

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager == null){
            Debug.LogError("The GameManager is NULL");
        }
        
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

        if (Input.GetKeyDown(KeyCode.P)){
            _panel.SetActive(true);
            _gameManager.PauseGame();
        }
        
    }

    public void ResumeGame(){
        _panel.SetActive(false);
        _gameManager.ResumeGame();
    }

    public void MainMenu(){
        _panel.SetActive(false);
        SceneManager.LoadScene("MainMenu");

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

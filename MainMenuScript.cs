using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
   public void LoadGame(){
        SceneManager.LoadScene("Space");
   }

   public void LoadAbout(){
        SceneManager.LoadScene("About");
   }

   public void LoadControls(){
        SceneManager.LoadScene("Controls");
   }

      public void LoadMenu(){
        SceneManager.LoadScene("MainMenu");
   }

}

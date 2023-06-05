using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   private void Start()
   {
      Time.timeScale = 1;
   }

   public void StartGame() => SceneManager.LoadScene(1);
}

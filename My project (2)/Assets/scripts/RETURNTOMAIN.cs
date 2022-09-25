using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RETURNTOMAIN : MonoBehaviour
{
   public void returntoMainMenu()
    {
        SceneManager.LoadScene("main menu");
    }
}

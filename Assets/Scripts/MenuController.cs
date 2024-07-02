using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Jogar()
    {
        SceneManager.LoadScene(0);

    }

    public void SairJogo()
    {
        Application.Quit();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HpHeart : MonoBehaviour
{
    public void DestroyAnimTrigger()
    {
        GetComponent<Animator>().Play("DestroyHeart");
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    public void PlayerDeaht()
    {
        GetComponent<Animator>().Play("DestroyPlayer");
    }
    public void GameEnd()
    {
        PauseMenu.gameIsPaused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

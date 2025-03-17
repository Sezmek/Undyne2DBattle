using UnityEngine;
using UnityEngine.SceneManagement;
public class CutSceneTransition : MonoBehaviour
{

    public float changeTime;


    void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionSceneManager : MonoBehaviour
{
    public static bool neoIsAlive = true;
    public static bool smithSafe = true;
    public static bool bossSafe = true; 

    private void Start()
    {
        neoIsAlive = true;
        smithSafe = true;
        bossSafe = true;
    }

    private void Update()
    {
        //Debug.Log(smithSafe);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }
}

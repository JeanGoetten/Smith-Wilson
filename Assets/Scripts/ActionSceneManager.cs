using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionSceneManager : MonoBehaviour
{
    public static bool neoIsAlive = true;
    public static bool smithSafe = true;

    private void Start()
    {
        neoIsAlive = true;
        smithSafe = true;
    }

    private void Update()
    {
        Debug.Log(smithSafe);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

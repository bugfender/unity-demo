using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToStart : MonoBehaviour
{
    private void Awake()
    {
        Bugfender.Log("BF Game Initialized");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(1);
            Bugfender.SendIssue("BugfenderGame", "Start Game");
        }
    }
}

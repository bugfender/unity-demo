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
            Bugfender.Log("User tapped - loading game scene");
            SceneManager.LoadScene(1);
        }
    }
}

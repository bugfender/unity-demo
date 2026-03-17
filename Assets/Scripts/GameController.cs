using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Diagnostics;

public class GameController : MonoBehaviour 
{
	public static GameController instance;			
	public Text scoreText;						
	public GameObject gameOvertext;				

	private int score = 0;						
	public bool gameOver = false;				
	public float scrollSpeed = -1.5f;


	void Awake()
	{
		if (instance == null)
			instance = this;
		else if(instance != this)
			Destroy (gameObject);
		Bugfender.Log("Game scene loaded - tap to jump, avoid barriers");
	}

	void Update()
	{
		if (gameOver && Input.GetMouseButtonDown(0)) 
		{
			Bugfender.Log("User tapped - restarting game");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	public void Scored()
	{
        if (!gameOver)
        {
            score++;
            scoreText.text = score.ToString();
            if (score == 1 || score % 5 == 0)
                Bugfender.Log($"Score: {score}");
        }
	}

	public void Died()
	{
        Bugfender.Log($"Game over - final score: {score}");
        Bugfender.SendIssue("Game Over", $"The bug was fixed. Final score: {score}");
        Debug.Log("End - The bug was fixed");
        gameOvertext.SetActive (true);
		gameOver = true;
	}
}

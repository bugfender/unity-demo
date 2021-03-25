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
    }

	void Update()
	{
		if (gameOver && Input.GetMouseButtonDown(0)) 
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	public void Scored()
	{
        if (!gameOver)
        {
            score++;
            scoreText.text = score.ToString();

        }
	}

	public void Died()
	{
        Debug.Log("End - The bug was fixed");
        gameOvertext.SetActive (true);
		gameOver = true;
	}
}

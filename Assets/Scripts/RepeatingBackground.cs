using UnityEngine;
using System.Collections;

public class RepeatingBackground : MonoBehaviour 
{

    private BoxCollider2D groundCollider;
	private float horizontalLength;		

	
	private void Awake ()
	{
		groundCollider = GetComponent<BoxCollider2D> ();
		horizontalLength = groundCollider.size.x;
	}


	private void Update()
	{
		if (transform.position.x < -horizontalLength)
		{
			MoveBackground ();
		}
	}

	private void MoveBackground()
	{
	
		Vector2 groundOffSet = new Vector2(horizontalLength * 2f, 0);
		transform.position = (Vector2) transform.position + groundOffSet;
	}
}
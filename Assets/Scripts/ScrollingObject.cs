﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour 
{
	private Rigidbody2D rb2d;


	void Start () 
	{
		
		rb2d = GetComponent<Rigidbody2D>();
		rb2d.linearVelocity = new Vector2 (GameController.instance.scrollSpeed, 0);
	}

	void Update()
	{
		if(GameController.instance.gameOver == true)
		{
			rb2d.linearVelocity = Vector2.zero;
		}
	}
}

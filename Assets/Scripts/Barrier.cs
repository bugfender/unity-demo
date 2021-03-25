﻿using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.GetComponent<Bug>() != null)
		{
			GameController.instance.Scored();
		}
	}
}

using UnityEngine;
using System.Collections;

public class Bug : MonoBehaviour 
{
	public float upForce;					
	private bool isDead = false;			

	private Animator anim;					
	private Rigidbody2D rb2d;				

	void Awake()
	{
		
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (!isDead) 
		{
			if (Input.GetMouseButtonDown(0)) 
			{
                
                anim.SetTrigger("Jump");
				rb2d.velocity = Vector2.zero;
				rb2d.AddForce(new Vector2(0, upForce));
                rb2d.angularVelocity = 0f;
                rb2d.rotation = 30f;
                rb2d.AddTorque(-2);
            }
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		rb2d.velocity = Vector2.zero;
        if (!isDead)
        {
            isDead = true;
            anim.SetTrigger("Die");
            GameController.instance.Died();
        }
	}
}

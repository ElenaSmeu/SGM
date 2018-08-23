using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour {

	//config parameters
	[SerializeField] Paddle MotherPaddle;
	[SerializeField] float xPush = 12f;
	[SerializeField] float yPush = 2f;
	[SerializeField] AudioClip[] ballSounds;
	[SerializeField] float randomBallMovingWhenCollide = 1f;

	int winner=0;
	[SerializeField] int X;
	[SerializeField] float WaitTime=2f;
	//state
	Vector2 paddleToBallVector;
	bool hasStarted = false;

	//cached component references
	AudioSource myAudioSource;
	Rigidbody2D myRigidBody2D;
	

	// Use this for initialization
	void Start () 
	{
		paddleToBallVector = transform.position - MotherPaddle.transform.position;
		myAudioSource = GetComponent<AudioSource>();
		myRigidBody2D = GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(!hasStarted)
		{
			LauchBallOnMouseClick();
			LockBallToPaddle();
		}
    }

    private void LauchBallOnMouseClick()
    {
		if(X==1){
        if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.S))
		{	
			hasStarted = true;
			myRigidBody2D.velocity = new Vector2(xPush, yPush);
		}
		}
		if(X==2){
        if(Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.DownArrow))
		{	
			hasStarted = true;
			myRigidBody2D.velocity = new Vector2(xPush, yPush);
		}
		}
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(MotherPaddle.transform.position.x,
                                                    MotherPaddle.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }
	private void OnCollisionEnter2D(Collision2D other) 
	{	
			if(X==1&& myRigidBody2D.IsTouchingLayers(LayerMask.GetMask("Paddle2")))
			{
				winner=1;
				StartCoroutine(LoadScene());
				
				//SceneManager.LoadScene("Game Over1");
			}
			else if(X==2&& myRigidBody2D.IsTouchingLayers(LayerMask.GetMask("Paddle1")))
			{
				winner=2;
				//StartCoroutine(LoadScene());
				 SceneManager.LoadScene("Game Over2");
			}

		Vector2 velocityBallRandomness 
				= new Vector2
					(Random.Range(0f, randomBallMovingWhenCollide),
						Random.Range(0f, randomBallMovingWhenCollide));
		if(hasStarted)
		{	
			AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
			myAudioSource.PlayOneShot(clip);
			myRigidBody2D.velocity += velocityBallRandomness;
		}

	}
	IEnumerator LoadScene()
	{
		yield return new WaitForSecondsRealtime(WaitTime);
		
		 SceneManager.LoadScene("Game Over"+winner);
	}
}

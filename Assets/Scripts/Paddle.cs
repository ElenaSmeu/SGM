using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Paddle : MonoBehaviour {

	[SerializeField] float paddleSpeed=100f;

	[SerializeField] public int x;
	[SerializeField] float lowMargin;
	[SerializeField] float Margin;


	//cached ref
	GameStatus myGameStatus;
	Ball inGameBall;
	Rigidbody2D myrigidBody;

	// Use this for initialization
	void Start () {
		myGameStatus = FindObjectOfType<GameStatus>();
		inGameBall = FindObjectOfType<Ball>();
		myrigidBody=GetComponent<Rigidbody2D>();
	}
	public int getX()
	{
		return x;
	}
	// Update is called once per frame
	void Update () {
		paddleMove();
		if(GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Wall")))
		{
			myrigidBody.velocity=new Vector2(myrigidBody.velocity.x,0f);
		}
		// Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);

		// 	if(Input.GetKeyDown(KeyCode.W))
		// 	{
		// 		paddlePos.y+= Mathf.Clamp(paddleSpeed, minY, maxY);
		// 		transform.position = paddlePos;
		// 	}
		// 	if(Input.GetKeyDown(KeyCode.S))
		// 	{
		// 		paddlePos.y-= Mathf.Clamp(paddleSpeed, minY, maxY);
		// 		transform.position = paddlePos;
		// 	}
	}
	public void paddleMove()
	{
		
		if(x==1)
		{
			if(Input.GetKeyDown(KeyCode.W))
		{
		myrigidBody.velocity=new Vector2(myrigidBody.velocity.x,paddleSpeed);
		}
		if(Input.GetKeyDown(KeyCode.S))
		 	{
				myrigidBody.velocity=new Vector2(myrigidBody.velocity.x,-paddleSpeed);
		}
		
		}
		else {
			if(Input.GetKeyDown(KeyCode.UpArrow))
		{
		myrigidBody.velocity=new Vector2(myrigidBody.velocity.x,paddleSpeed);
		}
		if(Input.GetKeyDown(KeyCode.DownArrow))
		 	{
				myrigidBody.velocity=new Vector2(myrigidBody.velocity.x,-paddleSpeed);
		}
		
		
		}
	}
	
	// private float GetYPos()
	// {
	// 	if(myGameStatus.IsAutoplayEnabled())
	// 	{
	// 		return inGameBall.transform.position.y;
	// 	}
	// 	else
	// 	{
			
	// 		return CrossPlatformInputManager.GetAxis("Vertical")/ Screen.height * screenHeigthInUnits;
	// 	}
	// }
}

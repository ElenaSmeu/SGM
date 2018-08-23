using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	//config params
	[SerializeField] AudioClip blockBreakSound;
	[SerializeField] GameObject blockSparkesVFX;
	[SerializeField] Sprite[] hitSprites;

	//cached reference
	Level level;

	//state variables
	[SerializeField] int numberOfHitsReceived = 0;

	private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    // Use this for initialization
    private void OnCollisionEnter2D(Collision2D other)
    {
		if(tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        numberOfHitsReceived++;
		int numberOfMaxHits = hitSprites.Length ;
        if (numberOfHitsReceived >= numberOfMaxHits)
        {
            print("Destroyyy");
            DestroyBlock();
            print("Destroyed");
        }
		else
		{
			ShowNextHitSprites();
		}
    }

    private void ShowNextHitSprites()
    {
        int spriteIndex = numberOfHitsReceived - 1;
		if(hitSprites[spriteIndex] != null)
		{
			GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}
		else
		{
			Debug.LogError("Block Sprite is missing from array!" + gameObject.name);
		}
    }

    private void DestroyBlock()
    {
        
        PlayDestroySFX();
        
        Destroy(gameObject);
        level.BlockDestroyed();
		TriggerSparkleEffect();
    }

    private void PlayDestroySFX()
    {
        
        FindObjectOfType<GameStatus>().AddPointsToScore();
       
        AudioSource.PlayClipAtPoint(blockBreakSound,
                                Camera.main.transform.position);
    }

    private void TriggerSparkleEffect()
	{
		GameObject sparkles = Instantiate(blockSparkesVFX, transform.position,transform.rotation);
		Destroy(sparkles, 2f);
	}
}

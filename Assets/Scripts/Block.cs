using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Configuration Variables
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    // State Variables
    int currentHits = 0;

    // Cached Component References
    Level level;
    GameStatus gameStatus;

    private void Start()
    {
        CountBreakableBlocks();
        gameStatus = FindObjectOfType<GameStatus>();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.increaseBreakableBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        currentHits++;
        if (currentHits >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        Sprite nextSprite = hitSprites[currentHits - 1];
        if (nextSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[currentHits - 1];
        }
    }

    private void DestroyBlock()
    {
        PlayBreakSFX();
        Destroy(gameObject);
        level.decreaseBreakableBlocks();
        gameStatus.AddToScore(maxHits);
        TriggerSparklesVFX();
    }

    private void PlayBreakSFX()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position - new Vector3(10, 10, 10));
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}

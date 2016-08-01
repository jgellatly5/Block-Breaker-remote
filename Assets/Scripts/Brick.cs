using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

    public AudioClip crack;
    public Sprite[] hitSprites;
    static public int breakableCount = 0;
    public GameObject Smoke;

    private int timesHit;
    private LevelManager levelManager;
    private bool isBreakable;
    

	// Use this for initialization
	void Start () {
        isBreakable = (this.tag == "Breakable");
        timesHit = 0;
        levelManager = FindObjectOfType<LevelManager>();
        if (isBreakable)
        {
            breakableCount++;
            print(breakableCount);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(crack, transform.position, 0.8f);
        if (isBreakable)
        {
            HandleHits();
        }
    }

    void HandleHits()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            breakableCount--;
            levelManager.BrickDestroyed();
            PuffSmoke();
            Destroy(gameObject,0.1f);
        }
        else
        {
            LoadSprites();
        }
    }
    void PuffSmoke()
    {
        GameObject smokePuff = Instantiate(Smoke, transform.position, Quaternion.identity) as GameObject;
        smokePuff.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    void LoadSprites()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Brick sprite missing");
        }
    }
    // TODO Remove this method once we can actually win!

    void SimulateWin()
    {
        levelManager.LoadNextLevel();
    }
}

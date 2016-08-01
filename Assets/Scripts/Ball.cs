using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    private Paddle Paddle;
    private Vector3 paddleToBallVector;
    private Rigidbody2D rb;
    private bool hasStarted = false;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        Paddle = FindObjectOfType<Paddle>();
        paddleToBallVector = this.transform.position - Paddle.transform.position;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!hasStarted)
        {
            //Lock the ball relative to the paddle.
            this.transform.position = Paddle.transform.position + paddleToBallVector;
            //Wait for a mouse press to launch.
            if (Input.GetMouseButtonDown(0))
            {
                hasStarted = true;
                this.rb.velocity = new Vector2(2f, 10f);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        Vector2 tweak = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
        if (hasStarted)
        {
            audioSource.Play();
            rb.velocity += tweak;
        }
    }
}

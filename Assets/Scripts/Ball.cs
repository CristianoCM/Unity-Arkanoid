using UnityEngine;

public class Ball : MonoBehaviour
{
    // Configuration Variables
    [SerializeField] Paddle paddle;
    [SerializeField] float launchBallX = 2f;
    [SerializeField] float launchBallY = 10f;
    [SerializeField] AudioClip[] audioListBall;
    [SerializeField] float randomFactor = 0.5f;

    // State Variables
    Vector2 paddleToBallDifVector;
    private bool ballLaunched;

    // Cached Component References
    AudioSource ballAudioSource;
    Rigidbody2D ballRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallDifVector = transform.position - paddle.transform.position;
        ballLaunched = false;
        ballAudioSource = GetComponent<AudioSource>();
        ballRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ballLaunched)
        {
            LockBallToPaddle();
            LaunchBallTrigger();
        }
    }

    private void LaunchBallTrigger()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ballLaunched = true;
            ballRigidBody.velocity = new Vector2(launchBallX, launchBallY);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + paddleToBallDifVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ballLaunched)
        {
            PlayCollisionSFX();
            AddTweakToBallVelocity();
        }
    }

    private void AddTweakToBallVelocity()
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));
        ballRigidBody.velocity += velocityTweak;
    }

    private void PlayCollisionSFX()
    {
        AudioClip ac = audioListBall[Random.Range(0, audioListBall.Length)];
        ballAudioSource.PlayOneShot(ac);
    }
}

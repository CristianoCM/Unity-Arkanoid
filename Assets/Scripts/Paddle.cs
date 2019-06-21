using System;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Configuration Variables
    [SerializeField] float screenWidthInUnities = 16f;
    [SerializeField] float maxX = 15f;
    [SerializeField] float minX = 1f;

    // Cached Component References
    GameStatus gameStatus;
    Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputTriggers();
        MovePaddle();
    }

    private void CheckInputTriggers()
    {
        if (Input.GetMouseButtonDown(1))
        {
            gameStatus.SwitchAutoPlay();
        }
    }

    private void MovePaddle()
    {
        float xToBeReached = GetXPos();
        float limitingXtbr = Mathf.Clamp(xToBeReached, minX, maxX);
        UpdatePaddlePosition(limitingXtbr);
    }

    private void UpdatePaddlePosition(float newPaddlePositionX)
    {
        Vector2 paddlePos = new Vector2(newPaddlePositionX, transform.position.y);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (gameStatus.IsAutoPlayEnabled())
        {
            return ball.transform.position.x;
        }

        return Input.mousePosition.x / Screen.width * screenWidthInUnities;
    }
}

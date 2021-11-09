using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float resetTime = 0.5f;
    private bool timerOn = false;
    private bool playerOn = false;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        FallThrough();
        Timer();
    }

    private void FallThrough()
    {
        if (playerOn == true && Input.GetKeyDown(KeyCode.S) || playerOn == true && Input.GetKeyDown(KeyCode.DownArrow))
        {
            effector.rotationalOffset = 180f;
            timerOn = true;
        }
    }

    private void Timer()
    {
        if (timerOn == true)
        {
            if (resetTime > 0f)
            {
                resetTime -= Time.deltaTime;
            }
            else
            {
                timerOn = false;
                resetTime = 0.5f;
                effector.rotationalOffset = 0f;
            }
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerOn = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerOn = false;
        }
    }
}

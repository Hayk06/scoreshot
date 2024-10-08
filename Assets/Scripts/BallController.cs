using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Vector3 startTouchPosition;
    private List<Vector3> swipePositions = new List<Vector3>();
    private Rigidbody2D rb;
    public LineRenderer lineRenderer;
    private bool isBallMoving = false;
    public float swipeForceMultiplier = 500f;
    public float movementDuration = 0.7f;
    private Vector3 initialPosition;
    private Vector3 initialScale;
    public float swipeStartRadius = 5f; // Radius around the ball to detect swipe start
    private bool isSwipeValid = false; // Flag to determine if swipe is valid

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        lineRenderer.positionCount = 0;
        initialPosition = transform.position;
        initialScale = transform.localScale;
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouseInput();
#endif
#if UNITY_IOS || UNITY_ANDROID
        HandleTouchInput();
#endif
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            if (Vector3.Distance(mousePosition, transform.position) <= swipeStartRadius)
            {
                StartSwipe(mousePosition);
                isSwipeValid = true;
            }
            else
            {
                isSwipeValid = false;
            }
        }

        if (Input.GetMouseButton(0) && isSwipeValid)
        {
            ContinueSwipe(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if (Input.GetMouseButtonUp(0) && isSwipeValid && swipePositions.Count > 1)
        {
            EndSwipe();
        }
        else if (Input.GetMouseButtonUp(0) && !isSwipeValid)
        {
            // Reset if swipe is invalid
            swipePositions.Clear();
            lineRenderer.positionCount = 0;
        }
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            if (touch.phase == TouchPhase.Began)
            {
                if (Vector3.Distance(touchPosition, transform.position) <= swipeStartRadius)
                {
                    StartSwipe(touchPosition);
                    isSwipeValid = true;
                }
                else
                {
                    isSwipeValid = false;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isSwipeValid)
            {
                ContinueSwipe(touchPosition);
            }
            else if (touch.phase == TouchPhase.Ended && isSwipeValid && swipePositions.Count > 1)
            {
                EndSwipe();
            }
            else if (touch.phase == TouchPhase.Ended && !isSwipeValid)
            {
                // Reset if swipe is invalid
                swipePositions.Clear();
                lineRenderer.positionCount = 0;
            }
        }
    }

    void StartSwipe(Vector3 startPosition)
    {
        startTouchPosition = startPosition;
        startTouchPosition.z = 0;
        swipePositions.Clear();
        swipePositions.Add(startTouchPosition);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, startTouchPosition);
    }

    void ContinueSwipe(Vector3 currentPosition)
    {
        currentPosition.z = 0;
        if (Vector3.Distance(swipePositions[swipePositions.Count - 1], currentPosition) > 0.1f)
        {
            swipePositions.Add(currentPosition);
            lineRenderer.positionCount = swipePositions.Count;
            lineRenderer.SetPositions(swipePositions.ToArray());
        }
    }

    void EndSwipe()
    {
        StartCoroutine(MoveBallAlongSwipe());
    }

    private IEnumerator MoveBallAlongSwipe()
    {
        isBallMoving = true;
        float time = 0;
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startTouchPosition, swipePositions[swipePositions.Count - 1]);
        float initialSpeed = distance * swipeForceMultiplier;

        for (int i = 1; i < swipePositions.Count; i++)
        {
            Vector3 nextPosition = swipePositions[i];
            while (Vector3.Distance(transform.position, nextPosition) > 0.01f)
            {
                time += Time.deltaTime / movementDuration;
                transform.position = Vector3.MoveTowards(transform.position, nextPosition, initialSpeed * Time.deltaTime);
                transform.localScale = Vector3.Lerp(initialScale, initialScale * 0.5f, time);
                yield return null;
            }
        }

        rb.velocity = Vector2.zero;
        isBallMoving = false;
        lineRenderer.positionCount = 0; // Clear line renderer after the swipe action
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            StartCoroutine(HandleBallInGoal());
        }
        else if (collision.gameObject.CompareTag("Boundary"))
        {
            GameOver();
        }
    }

    private IEnumerator HandleBallInGoal()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        yield return new WaitForSeconds(0.3f); // Delay before resetting ball

        // Reset ball to its initial position and scale
        transform.position = initialPosition;
        transform.localScale = initialScale;
        isBallMoving = false;

        lineRenderer.positionCount = 0; // Clear line renderer after goal
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        // Game over handling code
    }
}


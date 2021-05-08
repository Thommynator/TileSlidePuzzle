using UnityEngine;

public class SwipeDirection
{
    private bool isAvailable;
    private Vector2 swipeStartPosition;
    private Vector2 swipeStopPosition;

    public SwipeDirection()
    {
        isAvailable = false;
        swipeStartPosition = Vector2.zero;
        swipeStopPosition = Vector2.zero;
    }

    public void ListenForSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                isAvailable = false;
                swipeStartPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                swipeStopPosition = touch.position;
                isAvailable = true;
            }
        }
        else
        {
            isAvailable = false;
        }
    }

    private float GetAngleInDeg()
    {
        Vector2 normalizedDirection = (swipeStopPosition - swipeStartPosition).normalized;
        return Mathf.Atan2(normalizedDirection.y, normalizedDirection.x) * Mathf.Rad2Deg;
    }

    public bool IsUpSwipe()
    {
        if (!isAvailable)
        {
            return false;
        }
        float angle = GetAngleInDeg();
        return (angle >= 45 && angle <= 135);
    }

    public bool IsRightSwipe()
    {
        if (!isAvailable)
        {
            return false;
        }
        float angle = GetAngleInDeg();
        return angle >= -45 && angle <= 45;
    }

    public bool IsDownSwipe()
    {
        if (!isAvailable)
        {
            return false;
        }
        float angle = GetAngleInDeg();
        return angle >= -135 && angle <= -45;
    }

    public bool IsLeftSwipe()
    {
        if (!isAvailable)
        {
            return false;
        }
        float angle = GetAngleInDeg();
        return (angle >= -180 && angle <= -135) || (angle >= 135 && angle <= 180);
    }

}
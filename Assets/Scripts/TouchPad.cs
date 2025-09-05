using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPad : MonoBehaviour
{
    [SerializeField] private RectTransform touchPad;
    [SerializeField] private Vector3 _StartPos;
    private float _dragRadius = 50f;
    [SerializeField] Rocket _rocket;
    private bool isPressed = false;
    private int _touchId = -1;

    void Start()
    {
        touchPad = GetComponent<RectTransform>();
        _StartPos = touchPad.position;
        _rocket = GameObject.FindWithTag("Player").transform.GetComponent<Rocket>();
    }

    void FixedUpdate()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                HandleTouchInput();
                break;
            case RuntimePlatform.IPhonePlayer:
                HandleTouchInput();
                break;
            case RuntimePlatform.WindowsEditor:
                HandleInput(Input.mousePosition);
                break;
        }
    }

    void HandleTouchInput()
    {
        int idx = 0;
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                idx++;
                Vector2 touchPos = new Vector2(touch.position.x, touch.position.y);
                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.position.x <= (_StartPos.x + _dragRadius))
                        _touchId = idx;
                    
                }
                else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    if (_touchId == idx)
                    {
                        HandleInput(touchPos);
                    }
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    if (_touchId == idx)
                        _touchId = -1;
                }
            }
        }
    }

    void HandleInput(Vector3 input)
    {
        if (isPressed)
        {
            Vector3 differVector = (input - _StartPos);

            if (differVector.sqrMagnitude > (_dragRadius * _dragRadius))
            {
                differVector.Normalize();
                touchPad.position = _StartPos + differVector * _dragRadius;
            }
            else
            {
                touchPad.position = input;
            }
        }
        else
        {
            touchPad.position = _StartPos;
        }

        Vector3 differ = touchPad.position - _StartPos;
        Vector3 normalDiffer = new Vector3(differ.x / _dragRadius, differ.y / _dragRadius); 

        if (_rocket != null)
        {
            _rocket.OnStickPos(normalDiffer);
        }

    }

    public void OnPointDown()
    {
        isPressed = true;
    }
    public void OnPointUp()
    {
        isPressed = false;
        HandleInput(_StartPos);
    }
}

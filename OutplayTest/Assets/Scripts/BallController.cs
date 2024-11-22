using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/*Script for moving the ball with custom gravity values, and prediction function*/
public class BallController : MonoBehaviour
{
    [SerializeField] private float _gravityVal;
    [SerializeField] private float _speed;
    [SerializeField] private LevelGeneration _levelGeneration;
    [SerializeField] private Vector2 _initialPosition;
    private Rigidbody _rb;
    private Vector3 g;
    private float _predictedXPos = 0f;
    private float _currentXVel;
    private InputController _controls;
    private float _predictedX;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _controls = GetComponent<InputController>();
        ResetBall();
    }

    private void ResetBall()
    {
        if(_rb != null) _rb.velocity = Vector3.zero;
        g = Vector3.zero;
        transform.position = _initialPosition;
    }
    
    private void Update()
    {
        //Launch ball in random upward direction at constant speed
        if (_controls.IsLaunching)
        {
            //Set new gravity value
            g = new Vector3(0f, -_gravityVal, 0f);
            _rb.velocity = GetBallDirection() * _speed;
            _controls.IsLaunching = false;
        }

        if (_controls.IsResetting)
        {
            ResetBall();
            _controls.IsResetting = false;
        }
        
        Vector2 curPos = new Vector2(transform.position.x, transform.position.y);
        if (TryCalculateXPositionAtHeight(_levelGeneration.Height, curPos, _rb.velocity, _gravityVal, _levelGeneration.Width, ref _predictedX))
        {
            _levelGeneration.TargetPosition = new Vector3(_predictedX, _levelGeneration.TargetPosition.y, 0f);
        }
    }

    private void FixedUpdate()
    {
        if (_rb != null)
        {
            Vector3 currentVel = _rb.velocity;
            _rb.velocity += g * Time.deltaTime;
            
            _currentXVel = _rb.velocity.x;
        }
    }
    
    //Prediction Function
    bool TryCalculateXPositionAtHeight(float h, Vector2 p, Vector2 v, float G, float w, ref float xPosition)
    {
        /*Solve for the vertical motion for time when ball reaches height h*/
        float a = -0.5f * G;   // acceleration due to gravity (0.5 * a)
        float b = v.y;         // initial vertical velocity
        float c = p.y - h;     // difference between starting height and target height

        /*
          (Quadratic equation s = ut + 0.5 * at^2)
          a represents coefficient of t^2 (0.5 * at^2)
          
          b represents coefficient of t (the initial velocity v.y)
          
          c represents the constant; difference between initial vertical pos p.y and target height h. 
          It's the displacement in the vertical direction.*/
        
        // Discriminant of the quadratic equation
        float discriminant = b * b - 4 * a * c;
        
        // If the discriminant is negative, there is no real solution
        if (discriminant < 0)
        {
            return false;  // No solution means the ball doesn't reach the height h
        }

        // Calculate the two possible times (going up and/or coming down)
        float sqrtDiscriminant = Mathf.Sqrt(discriminant);
        float t1 = (-b + sqrtDiscriminant) / (2 * a);
        float t2 = (-b - sqrtDiscriminant) / (2 * a);

        // Choose the valid, positive time (either t1 or t2)
        float t = (t1 > 0) ? t1 : t2;

        // Now calculate the horizontal position at this time
        xPosition = p.x + v.x * t;

        if (xPosition >= (0 - _levelGeneration.Width / 2f) && xPosition <= 0 + _levelGeneration.Width / 2f)
        {
            return true;
        }
        return false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Ground")) return;
        _rb.velocity = new Vector3(-_currentXVel, _rb.velocity.y, 0f);
    }

    private Vector2 GetBallDirection()
    {
        return new Vector2(Random.Range(-1,1f), Random.Range(0,1f));
    }
}

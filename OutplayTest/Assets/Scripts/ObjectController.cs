
using UnityEngine;

/*Script that moves an object to 3 points in order until a collision has been made.
 Object will move on x and z axes, frozen on y.*/
public class ObjectController : MonoBehaviour
{
    public ParticleSystem ParticleSystem {get {return _particleSystem;}}
    public AudioManager AudioManager {get {return _audioManager;}}
    public Rigidbody Rb {get {return _rb;}}
    public bool CanMove {get {return _canMove;} set{_canMove = value;}}
    
    [Header("General Properties")]
    [SerializeField] private Transform[] _destinations;
    [SerializeField] private float _speed;
    [SerializeField] private AudioManager _audioManager;
    
    [Header("Particle Properties")]
    [SerializeField] private ParticleSystem _particleSystem;

    private int _currentDestinationCounter;
    private Rigidbody _rb;
    private bool _canMove = true;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        _currentDestinationCounter = 0;
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(!_canMove) return;
        MoveObject();
    }

    private void MoveObject()
    {
        //Check if object has reached destination
        if (HasReachedDestination()) _currentDestinationCounter++;
        if(_currentDestinationCounter >= _destinations.Length) _currentDestinationCounter = 0;
        
        //Store current destination
        Vector3 currentDestination = _destinations[_currentDestinationCounter].position;
        
        //Get direction object should move in
        Vector3 dir = currentDestination - transform.position;
        
        //Apply constant movement speed
        _rb.velocity = dir.normalized * (_speed * Time.fixedDeltaTime);
    }

    private bool HasReachedDestination()
    {
        float magnitudeDiff = Mathf.Abs(_destinations[_currentDestinationCounter].position.magnitude - transform.position.magnitude);
        if (magnitudeDiff < 0.01f  && magnitudeDiff > -0.01f) return true;
        return false;
    }
}


using UnityEngine;

/*Script deals with the collision of the object when reaching destination or collision with entity*/
public class CollisionHandler : MonoBehaviour
{
    private AudioManager _audioManager;
    private ObjectController _objectController;

    private bool _winOrLose = false;

    private void Start()
    {
        _objectController = GetComponent<ObjectController>();
        _audioManager = _objectController.AudioManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Entity"))
        {
            Debug.Log("Collision Enter");
            _audioManager.PlaySound(_audioManager.FailSound);

            _winOrLose = false;
            CollisionResponse();
        }

        if (other.transform.CompareTag("FinalDestination"))
        {
            _audioManager.PlaySound(_audioManager.DestinationSound);
            
            //Remove object from the scene (Deactivate for future use) 
            _winOrLose = true;
            CollisionResponse();
        }
    }

    private void CollisionResponse()
    {
        _objectController.CanMove = false;
        _objectController.Rb.velocity = Vector3.zero;
        
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        
        ParticleSystem particleSystem = _objectController.ParticleSystem;
        var main = particleSystem.main;
        
        //Switch colour depending on reaching destination or not
        if (!_winOrLose)
        {
            main.startColor = Color.red;
        }
        else if (_winOrLose)
        {
            main.startColor = Color.green;
        }  
        
        particleSystem.transform.position = gameObject.transform.position;
        particleSystem.gameObject.SetActive(true);
        Destroy(gameObject, particleSystem.main.duration);
    }
}

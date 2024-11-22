
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip DestinationSound {get {return _destinationSound;}}
    public AudioClip FailSound {get {return _failSound;}}
    public AudioSource AudioSource {get {return _audioSource;}}
    
    [Header("Sound Properties")] 
    [SerializeField] private AudioClip _destinationSound;
    [SerializeField] private AudioClip _failSound;
    [SerializeField] private AudioSource _audioSource;
}

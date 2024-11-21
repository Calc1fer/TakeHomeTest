using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Simple script to generate the borders of the level according to set width value*/
public class LevelGeneration : MonoBehaviour
{
    public float Width {get {return _width;}}
    public float Height {get{return _barrierYPos;}}
    public Vector3 TargetPosition { get{return _targetPrefab.transform.position;} set{_targetPrefab.transform.position = value;} }
    
    [SerializeField] private float _width;
    [SerializeField] private GameObject _borderPrefab;
    [SerializeField] private GameObject _heightBarrierPrefab;
    [SerializeField] private GameObject _targetPrefab;
    [SerializeField] private float _wallThickness = 0.2f;
    [SerializeField] private float _wallHeightScale;
    [SerializeField] private float _barrierYPos; //Height
    
    private GameObject _leftWall;
    private GameObject _rightWall;
    private GameObject _ground;
    private GameObject _barrier;
    
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate and position walls
        InstantiateGround();
        InstantiateWalls();
        InstantiateHeightBarrier();
        InstantiateTargetObject();
    }

    private void InstantiateWalls()
    {
        float wallOffset = _wallThickness / 2;
        
        Vector3 leftWallPos = new Vector3((-_width / 2) - wallOffset, _wallHeightScale / 2 - wallOffset, 0f);
        Vector3 rightWallPos = new Vector3((_width / 2) + wallOffset, _wallHeightScale / 2 - wallOffset, 0f);
        
        _leftWall = Instantiate(_borderPrefab, leftWallPos, Quaternion.identity);
        _rightWall = Instantiate(_borderPrefab, rightWallPos, Quaternion.identity);
        
        _leftWall.transform.localScale = new Vector3(_wallThickness, _wallHeightScale, _wallThickness);
        _rightWall.transform.localScale = new Vector3(_wallThickness, _wallHeightScale, _wallThickness);
    }

    private void InstantiateGround()
    {
        //Instantiate and position ground
        _ground = Instantiate(_borderPrefab, Vector3.zero, Quaternion.identity);
        _ground.transform.localScale = new Vector3(_width, _wallThickness, _wallThickness);
        _ground.tag = "Ground";
    }

    private void InstantiateHeightBarrier()
    {
        //Instantiate height barrier (Visual for seeing along x-axis of specific height where the ball could intersect)
        Vector3 barrierHeight = new Vector3(0f, _barrierYPos, 0f);
        _barrier = Instantiate(_heightBarrierPrefab, barrierHeight, Quaternion.identity);
        _barrier.transform.localScale = new Vector3(_width, _wallThickness, _wallThickness);
    }

    private void InstantiateTargetObject()
    {
        Vector3 pos = new Vector3(0f, _barrierYPos, 0f);
        _targetPrefab = Instantiate(_targetPrefab, pos, Quaternion.identity);
    }
}

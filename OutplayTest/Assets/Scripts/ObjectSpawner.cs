
using System.Collections.Generic;
using UnityEngine;

/*Script to spawn objects at random locations*/
public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _entityCount = 100f;
    [SerializeField] private float _xRange = 15f;
    [SerializeField] private float _zRange = 15f;
    
    private HashSet<Vector3> _spawnPositions = new HashSet<Vector3>();
    
    // Start is called before the first frame update
    void Start()
    {
        GeneratePositions();
    }

    private void GeneratePositions()
    {
        Vector3 pos = new Vector3(Random.Range(-_xRange, _xRange), 0f, Random.Range(-_zRange, _zRange));
        
        //Check if in the hashset
        if(_spawnPositions.Contains(pos)) GeneratePositions(); //Recursively call the function until a non-existent position has been found
        else
        {
            //If not then add to the list
            _spawnPositions.Add(pos);

        }

        //Use recursion until list count equals total entities
        if(_spawnPositions.Count != _entityCount) GeneratePositions();
        else
        {
            //Spawn the entities
            SpawnEntities();
        }
        
    }
    
    //Function to spawn the entities at generated locations
    private void SpawnEntities()
    {
        Debug.Log(_spawnPositions.Count);
        foreach (Vector3 pos in _spawnPositions)
        {
            GameObject obj = Instantiate(_prefab, pos, Quaternion.identity);
        }
    }
}

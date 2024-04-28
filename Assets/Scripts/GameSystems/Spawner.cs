using UnityEngine;
public class Spawner : MonoBehaviour
{
   [SerializeField] private EnemyPool enemyPool;
   [SerializeField] private int rows = 2; 
   [SerializeField] private int columns = 2;
   [SerializeField] private float horizontalSpacing = 30f; 
   [SerializeField] private float verticalSpacing = 30f;
    
    private int _totalEnemies;
    private int _destroyedEnemies; 
    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        _totalEnemies = rows * columns;
        _destroyedEnemies = 0;
        Vector3 shipPosition = FindObjectOfType<ShipController>().transform.position;
        Vector3 startPosition = new Vector3(0, shipPosition.y + 8f, 0); 

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = startPosition +
                                   new Vector3(col * horizontalSpacing - (columns * horizontalSpacing) / 2,
                                       -row * verticalSpacing, 0);

                GameObject enemy = enemyPool.GetObject();
                if (enemy != null)
                {
                    enemy.transform.position = position;
                    enemy.transform.rotation = Quaternion.identity;
                }
            }
        }
    }
    
    
    public void OnEnemyDestroyed(GameObject enemy)
    {
        enemyPool.ReturnObject(enemy);
        _destroyedEnemies++;
        
        if (_destroyedEnemies >= _totalEnemies)
        {
            SpawnEnemies();
        }
    }
}

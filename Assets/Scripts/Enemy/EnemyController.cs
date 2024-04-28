using UnityEngine;
public class EnemyController : Enemy,IDescription
{ 
    [SerializeField] private float _speed = 3f;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private ScoreManager _scoreManager;
    [Space(10)]
    [Header("Moving")]
    private bool movingRight = true;
    private float boundary = 2.5f; 
    private float downShift = 0.5f; 
   [Space(10)]
   [Header("Drop")]
   public GameObject[] powerUpPrefabs;
    private void Start()
    {
        _spawner = FindObjectOfType<Spawner>();
        _scoreManager = FindObjectOfType<ScoreManager>();
    }
    private void Update()
    {
       Move();
    }

    public override void Move()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
        }
        if (transform.position.x > boundary || transform.position.x < -boundary)
        {
            movingRight = !movingRight;
            transform.position = new Vector2(transform.position.x, transform.position.y - downShift);
        }
    }

    public override void AddScore()
    {
        _scoreManager.AddPoints(Random.Range(3,15)); 
    }

    public void Deaths()
    {
        _spawner.OnEnemyDestroyed(gameObject);
        if (powerUpPrefabs.Length > 0)
        {
            Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], transform.position, Quaternion.identity);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ship"))
        {
            AddScore();
            Deaths();
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            Deaths();
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            AddScore();
            Deaths();
        }
    }
}

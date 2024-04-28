using UnityEngine;
public class ProjectileController : MonoBehaviour,IDescription
{
    [SerializeField] private GameObject _effects;
    private ProjectTilePool _pool;
    private float _speed = 10f;
    private int index;
    private void Start()
    {
        _pool = FindObjectOfType<ProjectTilePool>();
        index = FindObjectOfType<ShipController>().currentProjectileIndex;
    }
    private void Update()
    {
       Move(); 
    }
    public void Move()
    {
        transform.Translate(0,_speed * Time.deltaTime,0f);
    }

    public void Deaths()
    {
        _pool.ReturnObject(gameObject,index); 
      //  Destroy(gameObject);
        Instantiate(_effects, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
           Deaths(); 
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            Deaths();
        }
    }
}

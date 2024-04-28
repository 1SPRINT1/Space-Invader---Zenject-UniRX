using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShipController : MonoBehaviour,IDescription
{
    [SerializeField] private GameObject _effect;
    [SerializeField] private Button _shootButton;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private ProjectTilePool _projectTilePool;
    [Space(10)]
    [SerializeField] private float _speed = 5.0f;
    private Vector2 movement;
    [Space(10)]
    [Header("Fire")]
    public float fireRate = 0.5f;
    private float lastFireTime;
    public GameObject[] projectilePrefabs; 
    public int currentProjectileIndex = 0;
    [Space(10)]
    [Header("Other - Setting")]
    private Subject<Unit> onShoot = new Subject<Unit>();
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
       onShoot 
            .ThrottleFirst(TimeSpan.FromSeconds(fireRate)) 
            .Subscribe(_ => ShootButton())
            .AddTo(this);
       
        _shootButton.onClick.AsObservable()
            .Subscribe(_ => onShoot.OnNext(Unit.Default))
            .AddTo(this);
    
        Observable.EveryFixedUpdate()
            .Subscribe(_ => _rb.MovePosition(_rb.position + movement * _speed * Time.fixedDeltaTime))
            .AddTo(this);
        
        Observable.EveryUpdate()
            .Select(_ => GetMovementInput())
            .Subscribe(input => movement = input)
            .AddTo(this);
    }
    private Vector2 GetMovementInput()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            
            movement = (touchPosition - (Vector2)transform.position).normalized;
        }
        else if (Input.acceleration != Vector3.zero)
        {
            movement = new Vector2(Input.acceleration.x, Input.acceleration.y);
        }

        return movement;
    }
    private void Update()
    {
      //  Move();
    }
    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + movement * _speed * Time.fixedDeltaTime); 
    }
    public void Move()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            
            movement = (touchPosition - (Vector2)transform.position).normalized;
        }
        else if (Input.acceleration != Vector3.zero)
        {
            movement = new Vector2(Input.acceleration.x, Input.acceleration.y);
        }
    }
    private void Fire()
    {
        GameObject projectile = _projectTilePool.GetObject(currentProjectileIndex);
        if (projectile != null)
        {
            projectile.transform.position = transform.position + new Vector3(0, 0.5f, 0);
            projectile.transform.rotation = Quaternion.identity;
        }
    }

    public void ShootButton()
    {
        if (Time.time > lastFireTime + fireRate)
        {
            Fire();
            lastFireTime = Time.time;
        }
    }
    public void Deaths()
    {
        _effect.SetActive(true);
        _effect.transform.position = transform.position;
        _shootButton.enabled = false;
        this.gameObject.SetActive(false);
       // Destroy(gameObject);
      //  Instantiate(_effect, transform.position, Quaternion.identity);
      //  _shootButton.SetActive(false);
    }
    public void ChangeProjectileType(int newTypeIndex)
    {
        if (newTypeIndex >= 0 && newTypeIndex < projectilePrefabs.Length)
        {
            currentProjectileIndex = newTypeIndex;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Deaths();
        } 
    }
}

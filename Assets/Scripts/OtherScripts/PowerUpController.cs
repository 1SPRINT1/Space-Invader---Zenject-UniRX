using UnityEngine;
using Random = UnityEngine.Random;
public class PowerUpController : MonoBehaviour
{
    public int projectileTypeIndex;

    private void Start()
    {
        projectileTypeIndex = Random.Range(0, 3);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ship"))
        {
            ShipController ship = other.GetComponent<ShipController>();
            if (ship != null)
            {
                ship.ChangeProjectileType(projectileTypeIndex);
                Destroy(gameObject);
            }
        }
    }
}

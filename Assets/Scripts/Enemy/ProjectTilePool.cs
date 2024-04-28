using System.Collections.Generic;
using UnityEngine;
public class ProjectTilePool : MonoBehaviour
{
     public GameObject[] projectilePrefabs; // Массив префабов снарядов
    private Queue<GameObject>[] projectileQueues; // Массив очередей для каждого типа снаряда
    
    public int initialPoolSize = 5;
    public int maxProjectilesOnScreen = 10;
    private int[] activeProjectilesCount;

    private void Start()
    {
        InitializePools();
    }

    public void InitializePools()
    {
        projectileQueues = new Queue<GameObject>[projectilePrefabs.Length];
        activeProjectilesCount = new int[projectilePrefabs.Length];

        for (int i = 0; i < projectilePrefabs.Length; i++)
        {
            projectileQueues[i] = new Queue<GameObject>();
            activeProjectilesCount[i] = 0;

            for (int j = 0; j < initialPoolSize; j++)
            {
                GameObject newProjectile = Instantiate(projectilePrefabs[i]);
                newProjectile.SetActive(false);
                projectileQueues[i].Enqueue(newProjectile);
            }
        }
    }

    public GameObject GetObject(int projectileIndex)
    {
        if (projectileIndex < 0 || projectileIndex >= projectileQueues.Length)
            return null;

        if (activeProjectilesCount[projectileIndex] >= maxProjectilesOnScreen)
        {
            RemoveExcessProjectiles(projectileIndex);
            return null;
        }

        Queue<GameObject> queue = projectileQueues[projectileIndex];
        GameObject projectile;

        if (queue.Count > 0)
        {
            projectile = queue.Dequeue();
        }
        else
        {
            projectile = Instantiate(projectilePrefabs[projectileIndex]);
        }

        projectile.SetActive(true);
        activeProjectilesCount[projectileIndex]++;
        return projectile;
    }

    public void ReturnObject(GameObject projectile, int projectileIndex)
    {
        if (projectileIndex < 0 || projectileIndex >= projectileQueues.Length)
            return;

        projectile.SetActive(false);
        projectileQueues[projectileIndex].Enqueue(projectile);
        activeProjectilesCount[projectileIndex]--;
    }
    private void RemoveExcessProjectiles(int projectileIndex)
    {
        var activeProjectiles = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                activeProjectiles.Add(child.gameObject);
            }
        }
        for (int i = activeProjectiles.Count - 1; i >= maxProjectilesOnScreen; i--)
        {
            PowerUpController projectileComponent = activeProjectiles[i].GetComponent<PowerUpController>();
            if (projectileComponent != null)
            {
                ReturnObject(activeProjectiles[i], projectileComponent.projectileTypeIndex);
            }
        }
    }
    
}

using System.Collections.Generic;
using UnityEngine;
public class EnemyPool : MonoBehaviour
{
   [SerializeField] private GameObject objectPrefab;
   [SerializeField] private int initialPoolSize = 6;

    private Queue<GameObject> objectsQueue = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject newObj = Instantiate(objectPrefab);
            newObj.SetActive(false);
            objectsQueue.Enqueue(newObj);
        }
    }

    public GameObject GetObject()
    {
        if (objectsQueue.Count > 0)
        {
            GameObject obj = objectsQueue.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        
        GameObject newObj = Instantiate(objectPrefab);
        newObj.SetActive(true);
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        objectsQueue.Enqueue(obj);
    }
}

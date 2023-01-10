using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    public int counter;
    public int index;


    void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
        counter = 0;
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Récupération des coordonnées du pointeur de la souris
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15));

        GameObject newObject = SharedInstance.GetPooledObject();

        if (Input.GetMouseButtonDown(0) && (counter < amountToPool))
        {
                newObject.transform.position = worldPosition;
                pooledObjects[counter].SetActive(true);
                counter++;
        }
        else if (Input.GetMouseButtonDown(0) && (counter == amountToPool))
        {
            if (index < amountToPool - 1)
            {
                pooledObjects[index].transform.position = worldPosition;
                index++;
            }
            else if(index == amountToPool - 1)
            {
                pooledObjects[index].transform.position = worldPosition;
                index = 0;
            }
            
         }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}

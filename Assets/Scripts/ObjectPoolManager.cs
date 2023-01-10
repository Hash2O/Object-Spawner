using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    //Objet � instancier
    [SerializeField] private GameObject _newObject;

    //Liste pour stocker les objets
    public List<GameObject> _gameObjects = new ();

    //Le premier objet stock� dans la liste
    private GameObject _firstObjectOfTheList;

    //Nombre maximum d'objets 
    public int _nbrMaxObject;

    //Nomnre d'objets cr��s (� comparer au max d'objets)
    public int _nbrOfObject;
    

    void Start()
    {
        _nbrOfObject = 0;
        _nbrMaxObject = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //R�cup�ration des coordonn�es du pointeur de la souris
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15));

        //Ici, on remplit la liste avec les objets cr��s
        if (Input.GetMouseButtonDown(0) && (_nbrOfObject < _nbrMaxObject))
        {
            Instantiate(_newObject, worldPosition, Quaternion.identity);    //Instanciation

            _gameObjects.Add(_newObject);   //On l'ajoute � la liste

            _nbrOfObject++;     //Incr�ment du compteur / condition du if
        }
        //L�, on recycle les objets de la liste quand on a atteint le quotat d'objets
        else if (Input.GetMouseButtonDown(0) && (_nbrOfObject == _nbrMaxObject))
        {
            _firstObjectOfTheList = _gameObjects[0]; //On cible le premier objet de la liste, donc le plus ancien

            _firstObjectOfTheList.transform.position = worldPosition;    //Instanciation de l'objet � la position 

            _gameObjects.RemoveAt(0); //on retire l'objet � l'index 0, premier de la liste

            _gameObjects.Add(_firstObjectOfTheList);   //On l'ajoute � la fin de la liste
        }
    }


}

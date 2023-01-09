using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraManager : MonoBehaviour
{

    //Source: http://www.unity3d-france.com/unity/phpBB3/viewtopic.php?t=16239

    //Permet de récupérer les infos de la caméra
    private Camera cam;

    //Permet de gérer la profondeur de spawn
    [SerializeField] private float zPosition;

    //Détermine quel objet va être instancié à chaque clic
    [SerializeField] private GameObject objectPrefab;

    //Permet de limiter le nombre d'objets à l'écran
    public int compteurMaxObjets;

    public int indexListeObjets;

    //Tableau permettant de stocker les clones du prefab choisi
    private List<GameObject> tableauPrefabs = new List<GameObject>();

    //Pour afficher certaines données à l'écran
    [SerializeField] private TextMeshProUGUI nombreObjetsMax;
    [SerializeField] private TextMeshProUGUI indexObjets;

    void Start()
    {
        //Initialisation de la caméra
        cam = Camera.main;

        //Initialisation du compteur max d'objets
        compteurMaxObjets = 10;

        indexListeObjets = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(compteurMaxObjets > 0)
            {
                InstancierDesGameObjectsEtLesMettreDansUneListe();
            }
            else if (compteurMaxObjets == 0)
            {
                if (indexListeObjets <= 9)
                {
                    InstancierLesGameObjetsDeLaListe();
                }
                else 
                {
                    indexListeObjets = 0;
                    InstancierLesGameObjetsDeLaListe();
                }
                
            }

        }
    }

    void InstancierDesGameObjectsEtLesMettreDansUneListe()
    {
        //On récupère les coordonnées du pointeur via la caméra
        Vector3 wordPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zPosition));

        //On instancie le gameobject à l'emplacement du pointeur, et à la profondeur voulue
        var item = (Instantiate(objectPrefab, wordPos, Quaternion.identity));

        //Décrémentation du compteur à chaque nouvelle instanciation
        compteurMaxObjets--;
        //Debug.Log("Compteur d'objets restants : " + compteurMaxObjets);
        nombreObjetsMax.text = "Compteur d'objets restants : " + compteurMaxObjets;

        //On insère le gameobject instancié dans la liste
        tableauPrefabs.Add(item);

        //Lister les cubes déjà présents dans la liste à chaque clic

        int index = -1; //Point de départ de l'indexation, à -1 pour avoir un premier résultat à 0
        foreach (GameObject element in tableauPrefabs)
        {
            index++;
            //Debug.Log(element.name + " " + index);
            indexObjets.text = "Index du dernier objet créé : " + index;
        }
    }

    void InstancierLesGameObjetsDeLaListe()
    {
        Vector3 wordPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zPosition));

        Instantiate(tableauPrefabs[indexListeObjets], wordPos, Quaternion.identity);

        indexObjets.text = "Index du nouvel objet créé : " + indexListeObjets;

        indexListeObjets++;
    }

    //Object Pooling : https://learn.unity.com/tutorial/introduction-to-object-pooling#
    //Gamedev.guru : https://thegamedev.guru/unity-cpu-performance/object-pooling/

    //Cette fonction provient du Scripting Reference de Unity
    void OnGUI()
    {
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();


    }
}

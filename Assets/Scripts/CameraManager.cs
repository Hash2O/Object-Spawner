using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraManager : MonoBehaviour
{

    //Source: http://www.unity3d-france.com/unity/phpBB3/viewtopic.php?t=16239

    //Permet de r�cup�rer les infos de la cam�ra
    private Camera cam;

    //Permet de g�rer la profondeur de spawn
    [SerializeField] private float zPosition;

    //D�termine quel objet va �tre instanci� � chaque clic
    [SerializeField] private GameObject objectPrefab;

    //Permet de limiter le nombre d'objets � l'�cran
    public int compteurMaxObjets;

    public int indexListeObjets;

    //Tableau permettant de stocker les clones du prefab choisi
    private List<GameObject> tableauPrefabs = new List<GameObject>();

    //Pour afficher certaines donn�es � l'�cran
    [SerializeField] private TextMeshProUGUI nombreObjetsMax;
    [SerializeField] private TextMeshProUGUI indexObjets;

    void Start()
    {
        //Initialisation de la cam�ra
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
        //On r�cup�re les coordonn�es du pointeur via la cam�ra
        Vector3 wordPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zPosition));

        //On instancie le gameobject � l'emplacement du pointeur, et � la profondeur voulue
        var item = (Instantiate(objectPrefab, wordPos, Quaternion.identity));

        //D�cr�mentation du compteur � chaque nouvelle instanciation
        compteurMaxObjets--;
        //Debug.Log("Compteur d'objets restants : " + compteurMaxObjets);
        nombreObjetsMax.text = "Compteur d'objets restants : " + compteurMaxObjets;

        //On ins�re le gameobject instanci� dans la liste
        tableauPrefabs.Add(item);

        //Lister les cubes d�j� pr�sents dans la liste � chaque clic

        int index = -1; //Point de d�part de l'indexation, � -1 pour avoir un premier r�sultat � 0
        foreach (GameObject element in tableauPrefabs)
        {
            index++;
            //Debug.Log(element.name + " " + index);
            indexObjets.text = "Index du dernier objet cr�� : " + index;
        }
    }

    void InstancierLesGameObjetsDeLaListe()
    {
        Vector3 wordPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zPosition));

        Instantiate(tableauPrefabs[indexListeObjets], wordPos, Quaternion.identity);

        indexObjets.text = "Index du nouvel objet cr�� : " + indexListeObjets;

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

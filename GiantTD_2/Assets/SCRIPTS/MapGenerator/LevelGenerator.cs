using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //Tiles
    [SerializeField]
    private GameObject grassTile, mountain, tree;
    public GameObject tileParent;




    //Map length and width
    public int rows = 20;
    public int columns = 20;

    [SerializeField]
    private List<GameObject> tiles;
    private List<List<GameObject>> coordinates;


    //Mountain max size and amount
    public int mountainMaxSize = 5;
    public int mountainAmount = 5;




    // Start is called before the first frame update
    void Start()
    {
        TileSpawner();
        //MountainBuilder(MountainAssembler(mountainMaxSize));
        MountainSpawner(Mountains(mountainAmount));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TileSpawner() //First version of the spawner that spawns a simple square shaped map
    {

        Vector3 tileSpawnLoc = new Vector3(0, 0, 0);

        for (int y = 0; y < columns; y++)
        {
            for (int x = 0; x < rows; x++)
            {

                GameObject toInstantiate = grassTile;
                //Debug.Log(x);
                if (x == 0 || x == columns - 1 || y == 0 || y == rows - 1) //Block the edges with mountains
                {
                    //Debug.Log("jeeh");
                    toInstantiate = mountain;
                }
                GameObject spawnedTile = Instantiate(toInstantiate, tileSpawnLoc, toInstantiate.transform.rotation);
                spawnedTile.transform.parent = tileParent.transform;
                //spawnedTile.GetComponent<BaseTile>().setLocation(transform.position);
                tiles.Add(spawnedTile);


                //Debug.Log(spawnedTile.transform.position);
                tileSpawnLoc.x += 1;
            }
            tileSpawnLoc.z += 1;
            tileSpawnLoc.x = 0;
        }
    }

    void MountainSpawner(List <GameObject> mountainsToSpawn)
    {
        GameObject mountainsInspector = new GameObject("Mountains");
        for (int i = 0; i < mountainsToSpawn.Count; i++)
        {
            Vector3 randomLoc = new Vector3(Random.Range(0, 30), 0, (Random.Range(0, 30)));
            GameObject mtn = Instantiate(mountainsToSpawn[i], randomLoc, Quaternion.identity);
            Debug.Log("Spawning mountain number: " + i);
            mtn.transform.parent = mountainsInspector.transform;
        }
    }

    void TreeSpawner()
    {

    }

    GameObject MountainBuilder(List<Vector3> coords) //Build mountains that are placed on the level
    {
        GameObject mtnHolder = new GameObject("MountainJeah");

        for (int i = 0; i < coords.Count; i++)
        {

            GameObject mtnPart = Instantiate(mountain, coords[i], Quaternion.identity);
            mtnPart.transform.parent = mtnHolder.transform;
            Destroy(mtnHolder);
        }

        return mtnHolder;
    }

    List<Vector3> MountainAssembler(int size) //Creates a list of coordinates for the mountain parts
    {
        List<Vector3> coordinates = new List<Vector3>(); //Create a list to contain random vector3 locations

        Vector3 currentLoc = new Vector3(0, 0, 0); //First one is at 0,0,0
        Vector3 previousLoc = new Vector3(0, 0, 0); //Keep previous mountainpart location just in case
        coordinates.Add(currentLoc); //Add it to the list as the first one

        for (int i = 1; i < size; i++)
        {
            int rnd = Random.Range(1, 3); //Check if we are going to go x or z direction
            if (rnd == 1)
            {
                currentLoc.x += Random.Range(0, 2) * 2 - 1;
            }
            else
            {
                currentLoc.z += Random.Range(0, 2) * 2 - 1;
            }

            if (coordinates.Contains(currentLoc)) //Check if list already has the coordinates, if it does, don't add and roll again
            {
                currentLoc = previousLoc; //If the position was already in the list, set current location to the previous so we can roll again
                i--; //Remove one iteration so we can roll again
            }
            else
            {
                coordinates.Add(currentLoc); //Add to list
                previousLoc = currentLoc; //Set current to previous location
            }


        }

        return coordinates; 

    }

    List<GameObject> Mountains(int mountainAmount)
    {
        List<GameObject> mountains = new List<GameObject>();
        

        for (int i = 1; i < mountainAmount; i++)
        {
            mountains.Add(MountainBuilder(MountainAssembler(mountainMaxSize)));
        }

        return mountains;
    }
}
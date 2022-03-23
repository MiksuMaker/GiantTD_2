using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{

    
    //Tiles
    [SerializeField]
    private GameObject grassTile, mountain, tree, town;
    public GameObject tileParent;
    public GameObject[] jee;
    public List<List<GameObject>> tileList = new List<List<GameObject>>();
    
   
    //Tree stuff
    public int amountOfTrees = 30;
    Dictionary<Vector2, GameObject> jees = new Dictionary<Vector2, GameObject>();

    //Map length and width
    public int rows = 20;
    public int columns = 20;

    //Town variables
    public int buildingsAmount = 5;
    public int buildingLocationSize = 3;

    [SerializeField]
    //private List<GameObject> tiles;
    //private List<List<GameObject>> coordinates;


    //Mountain max size and amount
    public int mountainMaxSize = 5;
    public int mountainAmount = 5;

    public class GOArray
    {
        [SerializeField]
        private GameObject[] _gameObjects;
    }



    // Start is called before the first frame update
    void Start()
    {

        //jees = jee.ToDictionary<Vector2, GameObject>(jee);
        //tileList = jees.ToDictionary<>();

        tileList = new List<List<GameObject>>();
        TileSpawner();
        MountainSpawner(Mountains(mountainAmount));
        TreeSpawner(amountOfTrees);
        TownSpawner();

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

            List<GameObject> tempList = new List<GameObject>();

            for (int x = 0; x < rows; x++)
            {
                //WorldTile tile = new WorldTile(grassTile, false, tileSpawnLoc); // Trying to spawn the tile as a custom class with a gameObject variable, not working since it's not appearing anywhere
                GameObject toInstantiate = grassTile;
                //Debug.Log(tile.tileMesh.transform.position);
                //Debug.Log(x);
                if (x == 0 || x == columns - 1 || y == 0 || y == rows - 1) //Block the edges with mountains
                {
                    //Debug.Log("jeeh");
                    toInstantiate = mountain;
                }
                GameObject spawnedTile = Instantiate(toInstantiate, tileSpawnLoc, toInstantiate.transform.rotation);
                spawnedTile.transform.parent = tileParent.transform;

                tempList.Add(spawnedTile);
                //spawnedTile.GetComponent<BaseTile>().setLocation(transform.position);
                


                //Debug.Log(spawnedTile.transform.position);
                tileSpawnLoc.x += 1;
            }
            tileSpawnLoc.z += 1;
            tileSpawnLoc.x = 0;
            tileList.Add(tempList);
        }
    }

    void TownSpawner()
    {
        //Get the middle of the map
        int townX = rows / 2;
        int townZ = columns / 2;

        //Get a square area in the middle in which to spawn the buildings
        int townXmin = townX - buildingLocationSize;
        int townXmax = townX + buildingLocationSize;
        int townZmin = townZ - buildingLocationSize;
        int townZmax = townZ + buildingLocationSize;

        List<Vector3> coords = new List<Vector3>();

        for (int i = 0; i < buildingsAmount; i++)
        {
            //Vector3 randomSpawnLoc tähän randomaa min-max välilt x ja z
            Vector3 randomSpawnLoc = new Vector3(0, 0, 0);
            randomSpawnLoc.x = Random.Range(townXmin, townXmax);
            randomSpawnLoc.z = Random.Range(townZmin, townZmax);

            if(coords.Contains(randomSpawnLoc))
            {
                i--;
            }
            else
            {
                coords.Add(randomSpawnLoc);
            }


        }
        //Vector3 spawnLoc = new Vector3(townX, 0, townZ);
        //Instantiate(town, spawnLoc, Quaternion.identity);

        for(int i = 0; i < coords.Count; i++)
        {
           //Destroy trees under town buildings 
            //if (Physics.OverlapSphere(coords[i], 0.1f).Length > 0) //If there is something on the tile we are trying to spawn, don't spawn and do another roll
            //{
            //    Collider[] foundObjects = Physics.OverlapSphere(coords[i], 0.1f);
            //    foreach (Collider foundObj in foundObjects)
            //    {
            //        Destroy(foundObjects[i].gameObject);
            //        Debug.Log("Destroyed object found under town");
            //    }
            //}

            Instantiate(town, coords[i], Quaternion.identity);
        }
    }

    void MountainSpawner(List <GameObject> mountainsToSpawn)
    {
        GameObject mountainsInspector = new GameObject("Mountains");
        for (int i = 0; i < mountainsToSpawn.Count; i++)
        {
            Vector3 randomLoc = new Vector3(Random.Range(3, rows-3), 0, (Random.Range(3, columns-3))); //Spawn mountains within the tiles
            GameObject mtn = Instantiate(mountainsToSpawn[i], randomLoc, Quaternion.identity);
            //Debug.Log("Spawning mountain number: " + i);
            mtn.transform.parent = mountainsInspector.transform;
        }
    }

    void TreeSpawner(int treeAmount)
    {
        int treesSpawned = 0;
        GameObject treeHolder = new GameObject("Trees");
        for (int i = 0; i < treeAmount; i++)
        {
            Vector3 randomLoc = new Vector3(Random.Range(1, rows - 1), 0, (Random.Range(1, columns - 1))); //Spawn mountains within the tiles
            Vector3 sphereLoc = new Vector3(randomLoc.x, randomLoc.y + 0.5f, randomLoc.z); //add +1 to randomLoc y so it checks above ground
            if (Physics.OverlapSphere(sphereLoc, 0.1f).Length > 0) //If there is something on the tile we are trying to spawn, don't spawn and do another roll
            {
                //Debug.Log(Physics.OverlapSphere(sphereLoc, 0.1f).Length);
                i--;
            }
            else
            {
                
                GameObject treeObj = Instantiate(tree, randomLoc, Quaternion.identity);
                treeObj.transform.parent = treeHolder.transform;
                treesSpawned++;
            }
            
        }
        Debug.Log(treesSpawned);
    }

    GameObject MountainBuilder(List<Vector3> coords) //Build mountains that are placed on the level
    {
        GameObject mtnHolder = new GameObject("Mountainholder"); //Place the "parts" under this object

        for (int i = 0; i < coords.Count; i++) //Take in coordinates in which to build a mountain
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

    List<GameObject> Mountains(int mountainAmount) //Make a list of mountains to build
    {
        List<GameObject> mountains = new List<GameObject>();
        

        for (int i = 1; i < mountainAmount; i++)
        {
            mountains.Add(MountainBuilder(MountainAssembler(mountainMaxSize)));
        }

        return mountains;
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.JSONSerializeModule;
using System.IO;
using System;
using UnityEngine.UI;


public class room : utility
{

    //public Collision collision;
    bool roomAnimate;
    //walls = new List<Wall>();
    //public bool first = true;
    public GameObject objAnimate;
    public float doorHeight;
    int timer=31;
    bool start = true;
    public GameObject door;
    public float wallScale;
    public GameObject awall;
    public Wall wall_script;
    public GameObject utility;
    public float wall_height = 10;
    public Text title;
    public GameObject player;

    Painting new_paint;
    GameObject new_wall;
    public GameObject currentDoor;
    //public List<GameObject> walls;
    //public Dictionary<char,GameObject> walls;
    public Dictionary<char, GameObject> walls = new Dictionary<char, GameObject>();
    public List<GameObject> doors;
    public W wallData;
    public D doorData;
    public Painting_tab paintings;

    // /*public Room(char cardi)
    // {
    //   //  first = false;
    //     //Wall well = new Wall(cardi);
    //     //walls.Add(well);
    //     //Debug.Log("number of walls in the room :123 " + walls.Count);
    // }*/
    // //public void add_wall(Vector2 p0, Vector2 p1, char cardi)
    [Serializable]
    public struct WallInfo {

        public float p00;
        public float p01;
        public float p10;
        public float p11;
        public string card;

        public Vector2 p1;
        public Vector2 p0;
        public char cardi;
        public void transform()
        {
            p1 = new Vector2(p10, p11);
            p0 = new Vector2(p00, p01);
            //p0 /= 4;
            //p1 /= 4;
            card=card.ToLower();
            cardi = card[0];
        }

    }
    [Serializable]
    public struct W
    {
      public  WallInfo[] tab;
    }
    [Serializable]
    public struct DoorInfo 
    {
        public float p00;
        public float p01;
        public float p10;
        public float p11;
        public string name;
        public Vector2 p1;
        public Vector2 p0;
        public void transform()
        {
            p1 = new Vector2(p10, p11);
            p0 = new Vector2(p00, p01);
            //p0 /= 4;
            //p1 /= 4;
        }
    }
    [Serializable]
    public struct D
    {
        public DoorInfo[] tab;
    }
    public void AddDoor(DoorInfo info)
    {
        Vector3 pos = new Vector3(0, 0, 0);
        pos.x = info.p0.x/2 + info.p1.x/2;
        pos.z = info.p0.y / 2 + info.p1.y / 2;
        pos.y = doorHeight;

        //Debug.Log("door ? " + door + " " + currentDoor);
        currentDoor=Instantiate(door);
        currentDoor.GetComponent<Transform>().position = pos;
        currentDoor.GetComponent<Door>().info.name = info.name;
        doors.Add(currentDoor);
    }
    public void AddWall(WallInfo info)
    {
        char cardi = info.cardi;
        Vector2 p0 = info.p0*wallScale;
        Vector2 p1 = info.p1*wallScale;
        
        float wall_depth = 2;
        Vector2 p = p1 - p0;
        float length = p.magnitude;
        

        p0 = (p0 + p1) / 2;
        Vector3 middle = new Vector3(p0.x, wall_height / 2 , p0.y);//middle is the centroid of the wall
        
        Vector3 v = direction(cardi) * wall_depth;//v is the scale vector
        v.y = wall_height;
        if (v.x == 0)
        {
            v.z = length;
            v.x = wall_depth;
        }
        else
        {
            v.x = length;
            v.z = wall_depth;
        }

        new_wall = Instantiate(awall);
        new_wall.GetComponent<Wall>().wallLength = length;
        
        walls.Add(info.cardi, new_wall);
        walls[info.cardi].GetComponent<Wall>().set(middle, v, cardi);
        
    }

    public void PaintingDisplay()
    {
        for (int i = 0; i < paintings.tab.Length; i++)
            {
               
                walls[paintings.tab[i].cardi].GetComponent<Wall>().add_painting(paintings.tab[i]);
            }
        foreach (KeyValuePair<char, GameObject> entry in walls)
        {
            int key = entry.Key;
            GameObject gameObj = entry.Value;
            // Do something here
            entry.Value.GetComponent<Wall>().HangPaintings();
            
        }

    }
    public void ExtractWallData(string wallJpath)
    {
        string apath = wallJpath;
        string jstring = File.ReadAllText(apath);
        wallData = JsonUtility.FromJson<W>("{\"tab\":" + jstring + "}");
        for (int i = 0; i < wallData.tab.Length; i++)
        {
            wallData.tab[i].transform();
            AddWall(wallData.tab[i]);
        }
        PaintingDisplay();
    }
    public void ExtracDoorData(string doorJpath)
    {
        string apath = doorJpath;
        string jstring = File.ReadAllText(apath);
        doorData = JsonUtility.FromJson<D>("{\"tab\":" + jstring + "}");
        for (int i = 0; i < doorData.tab.Length; i++)
        {
            doorData.tab[i].transform();
            
            AddDoor(doorData.tab[i]);
        }

    }
    public void ExtractPaintData(string paintPath)
    {
        string apath =  paintPath;
        string jstring = File.ReadAllText(apath);
        paintings = JsonUtility.FromJson<Painting_tab>("{\"tab\":" + jstring + "}");
        for (int i = 0; i < paintings.tab.Length; i++)
            paintings.tab[i].transform();
    }
    public void Clear()
    {
        foreach (KeyValuePair<char, GameObject> entry in walls)
        {
            int key = entry.Key;
            GameObject gameObj = entry.Value;
            // Do something here
            entry.Value.GetComponent<Wall>().Destroy();
        }
        for (int i = 0; i < doors.Count; i++)
            Destroy(doors[i]);
        walls.Clear();
        doors.Clear();

    }
    public void LoadNew(string roomName, bool start)
    {
        if (!start)
            Debug.Log("loading new from collision");
        //possible reiniatialisation of the bool, have to check
        
            
            Clear();
            start = false;
            string dPath = "Assets/Json/doors_json/" + roomName + ".json",
            pPath = "Assets/Json/paintings_json/" + roomName + ".json",
            wPath = "Assets/Json/walls_json/" + roomName + ".json";

            // dPath= Assets / Json / doors_json
            ExtracDoorData(dPath);
            ExtractPaintData(pPath);
            ExtractWallData(wPath);
            SetPlayer();
        ShowRoom();    

            timer = 0;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("coliding in room");

        //myroom.GetComponent<room>().LoadNew(info.name, false);
        //Debug.Log("colision");


    }
    public void ShowRoom()
    {
        objAnimate.GetComponent<TitleFading>().DisplayTitle();
        //objAnimate=player.gameObject.
        //title.text = "Room :\n" + roomName;
    }
    public void SetPlayer()
    {
        //find algo
        player.transform.position = walls['o'].GetComponent<Transform>().position / 4 +
             walls['e'].GetComponent<Transform>().position / 4 +
             walls['s'].GetComponent<Transform>().position / 4 +
             walls['n'].GetComponent<Transform>().position / 2;
        player.transform.position -= new Vector3(0, 3.75f, 0);
        //Debug.Log("player position "+player.transform.position);


    }
    public void CanvasInitiator()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        //player = Instantiate(player);
        LoadNew("X",true);
        SetPlayer();
        
        CanvasInitiator();
       // Debug.Log("canvas name : " + canvas.name);

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 60)
        {
            timer++;
        }
        if (roomChange&&timer>=30)
        {
            roomChange = false;
            LoadNew(roomName, true);
        }
        roomChange = false;
    }
    

}

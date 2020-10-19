using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//not actually breadth first generation
public class BreadthFirstGeneration : MonoBehaviour {

    public GameObject prefab;
    public GameObject mark;

    //make the map of size 20
    public int count = 0;
    public int MAP_SIZE = 10;

    public Map initial;
    List<Map> visited = new List<Map>();
    Stack<Map> stack = new Stack<Map>();

    public int gameSize = 0;

    void Awake()
    {
        initial = new Map();
        initial.SetIndex(new Vector2(0,0));
    }

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {   
        //instantiate created maps

        stack.Push(initial);

        Instantiate(prefab, initial.GetMapIndex(), Quaternion.identity).GetComponent<MapView>().SetMap(initial);
        ////how many child rooms to create //get maximum available
        //int max = GetMaximumAvailableRooms(initial);
        //int roomsToCreate = Random.Range(0, max + 1);
        ////pick rooms that are available
        //CreateMaps(initial, roomsToCreate);

        //to generate 5 rooms
        while (gameSize < 30)
        {

            Map newMap = stack.Pop();
            visited.Add(newMap);
            //instantiate created maps
            //how many child rooms to create //get maximum available
            int max = GetMaximumAvailableRooms(newMap) + 1;
            int roomsToCreate = Random.Range(1, max);
            //pick rooms that are available
            CreateMaps(newMap, roomsToCreate);

        }

    }


    public void CreateMaps(Map parent,int count)
    {

        int i = 0;
        while (i < count)
        {

            //pick random room from parent and check if its available
            int room = Random.Range(0, 4);

            if (parent.GetNeighbor(room) == null) {

                //available
                Map child = new Map();

                //there is no room in that spot so we will create a room and we will set cross reference
                if (CrossReferenceParentChildMaps(parent, child, room))
                {
                    //add child to created list and loop again
                    visited.Add(child);
                    parent.SetNeighbor(room,child);
                    i++;
                    gameSize++;

                }
            }
        }

    }

    private bool CrossReferenceParentChildMaps(Map parent, Map child, int room)
    {

        if (room == (int)MapNeighbor.NORTH)
        {
            if (!IsAlreadyTakenIndex(new Vector2(parent.GetMapIndex().x, parent.GetMapIndex().y + 1)))
            {
                parent.SetNeighbor((int)MapNeighbor.NORTH, child);
                child.SetNeighbor((int)MapNeighbor.SOUTH, parent);
                child.SetIndex(new Vector2(parent.GetMapIndex().x, parent.GetMapIndex().y + 1));
                Instantiate(prefab, child.GetMapIndex(), Quaternion.identity).GetComponent<MapView>().SetMap(child);
                Instantiate(mark, new Vector3((child.GetMapIndex().x + parent.GetMapIndex().x)/2, (child.GetMapIndex().y + parent.GetMapIndex().y)/2,0),Quaternion.identity);
                stack.Push(child);
                return true;
            }

            return false;
        }


        else if (room == (int)MapNeighbor.SOUTH)
        {

            if (!IsAlreadyTakenIndex(new Vector2(parent.GetMapIndex().x, parent.GetMapIndex().y - 1)))
            {
                parent.SetNeighbor((int)MapNeighbor.SOUTH, child);
                child.SetNeighbor((int)MapNeighbor.NORTH, parent);
                child.SetIndex(new Vector2(parent.GetMapIndex().x, parent.GetMapIndex().y - 1));
                Instantiate(prefab, child.GetMapIndex(), Quaternion.identity).GetComponent<MapView>().SetMap(child);
                Instantiate(mark, new Vector3((child.GetMapIndex().x + parent.GetMapIndex().x) / 2, (child.GetMapIndex().y + parent.GetMapIndex().y) / 2, 0), Quaternion.identity);
                stack.Push(child);
                return true;
            }

            return false;
        }


        else if (room == (int)MapNeighbor.EAST)
        {

            if (!IsAlreadyTakenIndex(new Vector2(parent.GetMapIndex().x + 1, parent.GetMapIndex().y)))
            { 
                parent.SetNeighbor((int)MapNeighbor.EAST, child);
                child.SetNeighbor((int)MapNeighbor.WEST, parent);
                child.SetIndex(new Vector2(parent.GetMapIndex().x + 1, parent.GetMapIndex().y));
                Instantiate(prefab, child.GetMapIndex(), Quaternion.identity).GetComponent<MapView>().SetMap(child);
                Instantiate(mark, new Vector3((child.GetMapIndex().x + parent.GetMapIndex().x) / 2, (child.GetMapIndex().y + parent.GetMapIndex().y) / 2, 0), Quaternion.identity);
                stack.Push(child);
                return true;
            }

            return false;
        }


        if (room == (int)MapNeighbor.WEST)
        {

            if (!IsAlreadyTakenIndex(new Vector2(parent.GetMapIndex().x - 1, parent.GetMapIndex().y)))
            {
                parent.SetNeighbor((int)MapNeighbor.WEST, child);
                child.SetNeighbor((int)MapNeighbor.EAST, parent);
                child.SetIndex(new Vector2(parent.GetMapIndex().x - 1, parent.GetMapIndex().y));
                Instantiate(prefab, child.GetMapIndex(), Quaternion.identity).GetComponent<MapView>().SetMap(child);
                Instantiate(mark, new Vector3((child.GetMapIndex().x + parent.GetMapIndex().x) / 2, (child.GetMapIndex().y + parent.GetMapIndex().y) / 2, 0), Quaternion.identity);
                stack.Push(child);
                return true;
            }
        }

        return false;

    }

    public int GetMaximumAvailableRooms(Map parent)
    {
        int maxAvailable = 4;

        Vector2 position = parent.GetMapIndex();

        for(int i = 0; i < visited.Count; i++)
        {

            if (visited[i].GetMapIndex() == position + new Vector2(0, 1) || visited[i].GetMapIndex() == position + new Vector2(0, -1) ||
                visited[i].GetMapIndex() == position + new Vector2(1, 0) || visited[i].GetMapIndex() == position + new Vector2(-1, 0))
            {
                maxAvailable--;

            }

        }

        return maxAvailable;

    }

    public bool IsAlreadyTakenIndex(Vector2 index)
    {

        for (int i = 0; i < visited.Count; i++)
        {

            if (visited[i].GetMapIndex() == index)
            {
                return true;

            }

        }

        return false;

    }
}

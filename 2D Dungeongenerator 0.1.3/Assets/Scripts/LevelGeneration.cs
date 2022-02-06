using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using RoomType;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms; //index 0 -> LR, i 1 -> LRB, i 2 ->LRT, i 3 -> LRBT

    private int direction;
    public float moveAmount;

    private float timeBtwRoom;
    private float startTimeBtwRoom = 0.25f;

    public float minX;
    public float maxX;
    public float minY;
    private bool stopGeneration;

    public LayerMask room; // for the circle

    

    void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);
    }

    private void Update()
    {
        if (timeBtwRoom <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        } else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    public void Move()
    {
        if (direction == 1 || direction == 2) //move Right!
        {
            
            //Debug.Log(transform.position.x);
            
            if (transform.position.x <= maxX) //check for right wall
            {
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length); //random (fitting) right room
                Instantiate(rooms[rand], transform.position, Quaternion.identity); 

                direction = Random.Range(1, 6); // new direction
                if(direction == 3) { //dont move left 
                    direction = 2; //moves right (no overlapping)
                } else if (direction == 4) { //dont move up
                    direction = 5; //move down
                }
            }
            else
            {
                direction = 5; //direction down
            }

        }
        else if (direction == 3 || direction == 4) //moveleft 
        {   

            if (transform.position.x >= minX) //check for left wall
            {
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length); //random (fitting) left room
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6); //dont move right (nr. 1/2)
            }
            else {             
                direction = 5; //go down
            }
               
        } else if (direction == 5) //move down
        {
           
            if (transform.position.y > minY)
            {
                             
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    roomDetection.GetComponent<RoomType>().RoomDestruction();

                    int randBottomRoom = Random.Range(1, 4);
                    if (randBottomRoom == 2)
                    {
                        randBottomRoom = 1;
                    }
                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                }
                
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4); // rooms 2/3 have top openings
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
        
            else
            {
                //end level
                stopGeneration = true;
            }
           
        }

        //Instantiate(rooms[0], transform.position, Quaternion.identity);
        //direction = Random.Range(1, 6);

    }

   
}

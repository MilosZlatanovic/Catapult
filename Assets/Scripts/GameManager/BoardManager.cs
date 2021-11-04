using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }
    public int columns = 8;
    public float rows = 3.3f;
    // private Transform boardHolder;                                    //A variable to store a reference to the transform of our Board object.
    public List<Vector3> gridPositions = new List<Vector3>();        //A list of possible locations to place tiles.
    public GameObject[] skyEnemies = null;                           //Array of enemy prefabs.

    private void Start()
    {
        GameManager.instance.AddEnemyToList(this);
    }
    public void InitialiseList()
    {
        gridPositions.Clear();
        for (int x = -1; x <= columns; x += 3)
        {
            //With in each column, loop through y axis (rows).
            for (float y = -1; y <= rows; y += 2.1f)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
                //At each index add a new Vector3 to our list with the x and y coordinates of that position.
            }
        }
    }

   /* Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }*/
    //RandomPosition returns a random position from our list gridPositions.
    Vector3 RandomPosition()
    {
        //Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
        int randomIndex = Random.Range(0, gridPositions.Count);

        //Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
        Vector3 randomPosition = gridPositions[randomIndex];

        //Remove the entry at randomIndex from the list so that it can't be re-used.
        gridPositions.RemoveAt(randomIndex);

        //Return the randomly selected Vector3 position.
        return randomPosition;
    }


    public void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        //Choose a random number of objects to instantiate within the minimum and maximum limits
        int objectCount = Random.Range(minimum, maximum + 1);

        //Instantiate objects until the randomly chosen limit objectCount is reached
        for (int i = 0; i < objectCount; i++)
        {
            //Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
            Vector3 randomPosition = RandomPosition();

            //Choose a random tile from tileArray and assign it to tileChoice
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

            //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }
    public void SetupScene(int level)
    {
        /* if (level <= 1)
               {
                   level = 2;
               }*/
        //Reset our list of gridpositions.
        InitialiseList();

        int enemyCount = (int)Mathf.Log(level, 2f);
        //Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
        LayoutObjectAtRandom(skyEnemies, enemyCount, enemyCount);
    }
}

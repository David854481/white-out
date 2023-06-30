using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Initialization of variables
    [SerializeField]
    private float spawnCount = 10;
    [SerializeField]
    private float waveSpawnTimer = 10;
    [SerializeField]
    private string enemyId;

    private Vector3 randPosition;
    private float randX, randY;
    private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()   //random spawning outside of screen
    {   
        if(player.ColorIndex == -1) return;
        if (spawnCount > 0)
        {
            randX = Random.Range(-8, 8);
            if(randX >= -5 && randX <= 0)
            {
                randX -= 5;
            }
            else if (randX <= 5 && randX >= 0)
            {
                randX += 5;
            }

            randY = Random.Range(-10, 10);
            if (randY >= -8 && randY <= 0)
            {
                randY -= 8;
            }
            else if (randY <= 8 && randY >= 0)
            {
                randY += 8;
            }

            randPosition.Set(transform.position.x - randX, transform.position.y - randY, transform.position.z);
            Spawn();
            spawnCount--;
        }
        else
        {
            spawnCount = 0;
        }

        if(waveSpawnTimer > 0)
        {
            waveSpawnTimer -= Time.deltaTime;
        }
        else
        {
            spawnCount = 10;
            waveSpawnTimer = 10;
        }
    }

    void Spawn()    //instantiate an enemy
    {
        //Get an object from the pool
        GameObject enemy = ObjectPoolManager.Instance.GetPooledObject(enemyId);
        //Did we get an object from the pool?
        if(enemy != null)
        {
            //Position the enemy 
            enemy.transform.position = randPosition;
            enemy.SetActive(true);
        }
    }
}

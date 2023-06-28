using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.5f;
    [SerializeField]
    private Sprite orange;
    [SerializeField]
    private Sprite purple;
    [SerializeField]
    private Sprite green;

    private GameObject player;
    private Transform playerLocation;
    private int colorIndex;

    Vector2 direction;

    private void Awake()
    {
       player = GameObject.Find("Ship");
    }

    // Start is called before the first frame update
    void Start()
    {

        playerLocation = player.transform;

        //Spawns as a random color
        colorIndex = Random.Range(1,4);
        switch(colorIndex)
        {
            case 1:
                this.GetComponent<SpriteRenderer>().sprite = orange;
                break;
            case 2:
                this.GetComponent<SpriteRenderer>().sprite = purple;
                break;
            case 3:
                this.GetComponent<SpriteRenderer>().sprite = green;
                break;
            default:
                break;
        }

        facePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerLocation.position, speed * Time.deltaTime);
        facePlayer();
    }

    void facePlayer()
    {
        direction = new Vector2(
            playerLocation.position.x - transform.position.x, 
            playerLocation.position.y - transform.position.y
        );

        transform.up = -direction;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private string bulletId;
    [SerializeField]
    private float speed = 5f;
    private int colorIndex;
    public int ColorIndex { get => colorIndex;}
    SpriteRenderer spriteRenderer;

    private Vector2 direction;
    private Vector3 mousePosition;

    Color orange;
    Color purple;
    bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        ColorUtility.TryParseHtmlString("#FF7400", out orange);
        ColorUtility.TryParseHtmlString("#A500B5", out purple);

        colorIndex = -1;
        isAlive = true;

        //Gets music file
        AudioSystem.instance.PlayMusic(AudioEnum.mus_gameplay);

        //Fades in music
        LeanTween.value(gameObject, 0f, AudioSystem.instance.DefaultVolume, 1f)
        .setEaseLinear()
        .setOnUpdate(delegate (float _val)
        {
            AudioSystem.instance.MusicSource.volume = _val;
        });
        //Plays music
        AudioSystem.instance.MusicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive == true)
        {
            move();
            faceMouse();
            shoot();
            changeColor();
        }
          
    }

    private void move()
    {  
        //Basic WASD movement

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }

    private void shoot()
    {
        if (Input.GetButtonDown("Fire1") && colorIndex > -1)   //If mouse button pressed and character is not white
        {
            //Get an object from the pool
            GameObject bullet = ObjectPoolManager.Instance.GetPooledObject(bulletId);
            //Did we get an object from the pool?
            if(bullet != null)
            {
                //Position the enemy 
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
            }
            //new audioooo -- do this again for enemy destroyed and ship destroyed
            AudioSystem.instance.PlaySound(AudioEnum.snd_shoot);
        }
    }

    private void changeColor()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Space))    //press Q or Space to cycle through colors
        {
            if (colorIndex == 2)
            {
                colorIndex = 0;
            }
            else
            {
                colorIndex++;
            }
        }

        switch(colorIndex)  //0 - orange; 1 - purple; 2 - green
        {
            case 0: //orange
                spriteRenderer.color = orange;
                break;
            case 1: //purple
                spriteRenderer.color = purple;
                break;
            case 2: //green
                spriteRenderer.color = Color.green;
                break;
            default:
                break;
        }
    }

    private void faceMouse()    //player points in the direction of the mouse on screen
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        transform.up = -direction;
    }

    private void OnTriggerEnter2D(Collider2D collision) //when the player collides with the enemy, the game ends
    {
        if(collision.CompareTag("Enemy"))
        {
            isAlive = false;
            SceneManager.LoadScene("ResultsMenu");
        }
    }

    public void DisableObject(GameObject objectToDisable, float time)
    {
        StartCoroutine(DisableObjectCoroutine(objectToDisable, time));
    }

    private IEnumerator DisableObjectCoroutine(GameObject objectToDisable, float time)
    {
        yield return new WaitForSeconds(time);
        objectToDisable.SetActive(false);
    }
}



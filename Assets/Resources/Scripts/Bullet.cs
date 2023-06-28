using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Bullette
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        float speed = 10;
        private float initializationTime;

        private Sprite spr_orange;
        private Sprite spr_purple;
        private Sprite spr_green;

        [SerializeField]
        private Sprite spr_enemyOrange;
        [SerializeField]
        private Sprite spr_enemyPurple;
        [SerializeField]
        private Sprite spr_enemyGreen;

        private SpriteRenderer spriteRenderer;
        private SpriteRenderer sr;

        private ParticleSystem explosionGreen;
        private ParticleSystem explosionPurple;
        private ParticleSystem explosionOrange;

        Color orange;
        Color purple;
        Vector2 direction;
        Vector3 mousePosition;


        private void Start()
        {
            sr = GameObject.Find("Ship").GetComponent<SpriteRenderer>(); //get the player's sprite renderer

            spriteRenderer = this.GetComponent<SpriteRenderer>();

            //LOAD RESOURCES

            spr_orange = Resources.Load<Sprite>("Sprites/Orange_Bullet_Pixel_Art");
            spr_purple = Resources.Load<Sprite>("Sprites/Violet_Bullet_Pixel_Art");
            spr_green = Resources.Load<Sprite>("Sprites/Green_Bullet_Pixel_Art");

            explosionGreen = Resources.Load<ParticleSystem>("Prefabs/ParticleGreen");
            explosionPurple = Resources.Load<ParticleSystem>("Prefabs/ParticlePurple");
            explosionOrange = Resources.Load<ParticleSystem>("Prefabs/ParticleOrange");

            initializationTime = Time.timeSinceLevelLoad;

            ColorUtility.TryParseHtmlString("#FF7400", out orange);
            ColorUtility.TryParseHtmlString("#A500B5", out purple);

            if(sr.color == orange)
            {
                spriteRenderer.sprite = spr_orange;
            }
            else if (sr.color == purple)
            {
                spriteRenderer.sprite = spr_purple;
            }
            else
            {
                spriteRenderer.sprite = spr_green;
            }

            faceMouse();
        }

        public void Init()
        {
            Invoke("DestroySelf", 1.5f);
        }

        void faceMouse()
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            direction = new Vector2(
                mousePosition.x - transform.position.x,
                mousePosition.y - transform.position.y
            );

            transform.up = -direction;
        }

        void DestroySelf()
        {
            Destroy(gameObject);
        }

        void Update()
        {
            
            transform.Translate(0, Time.deltaTime * -speed, 0); //move straight

            //Destroys the bullet after 3 seconds
            Destroy(gameObject, 3); 
        }

        void OnTriggerEnter2D(Collider2D _collider)
        {
            if (_collider.CompareTag("Enemy"))
            {
                //if green bullet hits green enemy
                if (_collider.gameObject.GetComponent<SpriteRenderer>().sprite == spr_enemyGreen && spriteRenderer.sprite == spr_green)  
                {
                    Instantiate(explosionGreen, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    Destroy(_collider.gameObject);
                }
                //if purple bullet hits purple enemy
                if (_collider.gameObject.GetComponent<SpriteRenderer>().sprite == spr_enemyPurple && spriteRenderer.sprite == spr_purple)
                {
                    Instantiate(explosionPurple, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    Destroy(_collider.gameObject);
                }
                //if green orange hits orange enemy
                if (_collider.gameObject.GetComponent<SpriteRenderer>().sprite == spr_enemyOrange && spriteRenderer.sprite == spr_orange)
                {
                    Instantiate(explosionOrange, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    Destroy(_collider.gameObject);
                }
            }
        }
    }

}

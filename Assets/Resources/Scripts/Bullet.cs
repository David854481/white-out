using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Bullette
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        float speed = 10;

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

        [SerializeField] private string explosionGreenID;
        [SerializeField] private string explosionPurpleID;
        [SerializeField] private string explosionOrangeID;

        Color orange;
        Color purple;
        Vector2 direction;
        Vector3 mousePosition;
        private Player player;

        private void Awake()
        {
            player = FindObjectOfType<Player>();
            sr = player.gameObject.GetComponent<SpriteRenderer>(); //get the player's sprite renderer

            spriteRenderer = this.GetComponent<SpriteRenderer>();

            //LOAD RESOURCES

            spr_orange = Resources.Load<Sprite>("Sprites/Orange_Bullet_Pixel_Art");
            spr_purple = Resources.Load<Sprite>("Sprites/Violet_Bullet_Pixel_Art");
            spr_green = Resources.Load<Sprite>("Sprites/Green_Bullet_Pixel_Art");

            ColorUtility.TryParseHtmlString("#FF7400", out orange);
            ColorUtility.TryParseHtmlString("#A500B5", out purple);
        }

        private void OnEnable()
        {
            SetSprite();
            faceMouse();
            StartCoroutine(DestroySelf());
        }

        private void SetSprite()
        {
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

        IEnumerator DestroySelf()
        {
            yield return new WaitForSeconds(3f);
            gameObject.SetActive(false);
        }

        void Update()
        {
            transform.Translate(0, Time.deltaTime * -speed, 0); //move straight
        }

        void OnTriggerEnter2D(Collider2D _collider)
        {
            if (_collider.CompareTag("Enemy"))
            {
                //if green bullet hits green enemy
                if (_collider.gameObject.GetComponent<SpriteRenderer>().sprite == spr_enemyGreen && spriteRenderer.sprite == spr_green)  
                {
                    InstantiateExplosion(explosionGreenID);
                    HitEnemy(_collider.gameObject);
                }
                //if purple bullet hits purple enemy
                if (_collider.gameObject.GetComponent<SpriteRenderer>().sprite == spr_enemyPurple && spriteRenderer.sprite == spr_purple)
                {
                    InstantiateExplosion(explosionPurpleID);
                    HitEnemy(_collider.gameObject);
                }
                //if green orange hits orange enemy
                if (_collider.gameObject.GetComponent<SpriteRenderer>().sprite == spr_enemyOrange && spriteRenderer.sprite == spr_orange)
                {
                    InstantiateExplosion(explosionOrangeID);
                    HitEnemy(_collider.gameObject);
                }
            }
        }

        private void HitEnemy(GameObject enemy)
        {
            enemy.SetActive(false);

            StopAllCoroutines();
            gameObject.SetActive(false);
        }

        private void InstantiateExplosion(string explosionID)
        {
            //Get an object from the pool
            GameObject explosion = ObjectPoolManager.Instance.GetPooledObject(explosionID);
            //Did we get an object from the pool?
            if(explosion != null)
            {
                //Position the enemy 
                explosion.transform.position = transform.position;
                explosion.SetActive(true);
                player.DisableObject(explosion, 1f);
            }
        }
    }

}

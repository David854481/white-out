using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    //declaration of variables
    public TMPro.TextMeshProUGUI ScoreTxt;
    float score;
    [SerializeField] private float scorePerSecond;
    public int colorIndex;

    public Sprite orangeBucket;
    public Sprite purpleBucket;
    public Sprite greenBucket;

    public SpriteRenderer bucket;
    
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        //Resets game score
        UpdateScore(_resetScore: true);
        orangeBucket = Resources.Load<Sprite>("Sprites/bucket_Orange");
        purpleBucket = Resources.Load<Sprite>("Sprites/bucket_Purple");
        greenBucket = Resources.Load<Sprite>("Sprites/bucket_Green");
        bucket = GameObject.Find("CurrentColorHUD").GetComponent<SpriteRenderer>();
        colorIndex = -1;
    }

    public void UpdateScore(float _relativeScore = 0, bool _resetScore = false)
    {
        //Sets score to 0 on reset
        if (_resetScore)
            score = 0;
        else
            score += _relativeScore * Time.deltaTime;
        //Shows current score
        ScoreTxt.text = score.ToString("#.");
        //Gives last score to the game manager
        GameManager.instance.LastScore = score;
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

        switch (colorIndex)  //0 - orange; 1 - purple; 2 - green
        {
            case 0: //orange
                bucket.sprite = orangeBucket;
                break;
            case 1: //purple
                bucket.sprite = purpleBucket;
                break;
            case 2: //green
                bucket.sprite = greenBucket;
                break;
            default:
                break;
        }
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    void Update()
    {
        //Updates Score
        UpdateScore(scorePerSecond);
        changeColor();
    }
}

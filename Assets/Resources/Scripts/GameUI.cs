using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    //declaration of variables
    public TMPro.TextMeshProUGUI ScoreTxt, inputPromptText;
    float score;
    [SerializeField] private float scorePerSecond;
    public int colorIndex;

    private Sprite orangeBucket;
    private Sprite purpleBucket;
    private Sprite greenBucket;

    private Image bucket;
    
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    private Player player;

    void Start()
    {
        //Resets game score
        UpdateScore(_resetScore: true);
        orangeBucket = Resources.Load<Sprite>("Sprites/bucket_Orange");
        purpleBucket = Resources.Load<Sprite>("Sprites/bucket_Purple");
        greenBucket = Resources.Load<Sprite>("Sprites/bucket_Green");
        bucket = GameObject.Find("CurrentColorHUD").GetComponent<Image>();
        player = FindObjectOfType<Player>();

        bucket.gameObject.SetActive(false);
        colorIndex = -1;
    }

    public void UpdateScore(float _relativeScore = 0, bool _resetScore = false)
    {
        //Sets score to 0 on reset
        if (_resetScore)
            score = 0;
        else
            if(player.ColorIndex != -1) score += _relativeScore * Time.deltaTime;
        //Shows current score
        ScoreTxt.text = score.ToString("#.");
        //Gives last score to the game manager
        GameManager.instance.LastScore = score;
    }
    private void changeColor()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Space))    //press Q or Space to cycle through colors
        {
            if(!bucket.gameObject.activeSelf) bucket.gameObject.SetActive(true);
            if(inputPromptText.gameObject.activeSelf) inputPromptText.gameObject.SetActive(false);

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

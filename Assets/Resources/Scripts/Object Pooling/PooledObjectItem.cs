using UnityEngine;

public class PooledObjectItem : MonoBehaviour
{
    [SerializeField]
    private string _id;

    public string ID
    {
        get => _id;
        set => _id = value;
    }
}


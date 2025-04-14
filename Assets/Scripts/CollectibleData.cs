using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item", fileName = "ItemData")]
public class CollectibleData : ScriptableObject
{
    [SerializeField] string itemName;
    [SerializeField] float points;
    [SerializeField] ItemType itemType;

    public string ItemName { get { return itemName; } }
    public float Points { get { return points; } }
    public ItemType ItemType { get { return itemType; } }
}

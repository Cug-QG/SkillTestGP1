using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] CollectibleData data;
    public delegate void CollectItemDelegate(Player player);
    public CollectItemDelegate CollectItem;

    private void Start()
    {
        CollectItem = Heal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) { return; }
        GameManager.Instance.AddFruit();
        CollectItem.Invoke(collision.GetComponent<Player>());
        Destroy(gameObject);
    }

    private void Heal(Player player)
    {
        player.ChangeHealth(5);
    }
}
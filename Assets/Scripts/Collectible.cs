using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] CollectibleData data;
    public delegate void CollectItemDelegate(Player player);
    public CollectItemDelegate CollectItem;

    private void Start()
    {
        switch (data.ItemType)
        {
            case ItemType.Heal:
                CollectItem = Heal;
                break;
            case ItemType.Shield:
                CollectItem = Shield;
                break;
            case ItemType.DamageBoost:
                CollectItem = BoostDamage;
                break;
            default:
                break;
        }
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
        player.ChangeHealth(1);
    }

    private void Shield(Player player) 
    {
        player.SetShield(5);
    }

    private void BoostDamage(Player player)
    {
        player.SetDMGBoost(20);
    }
}
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Character", fileName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    [SerializeField] float maxHP;
    [SerializeField] float speed;
    [SerializeField] float dmg;

    public float MaxHP { get { return maxHP; } }
    public float Speed { get { return speed; } }
    public float Dmg { get { return dmg; } }
}

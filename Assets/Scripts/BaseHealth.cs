using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public int maxHP = 10;
    private int hp;

    public int CurrentHP => hp;

    void Start()
    {
        hp = maxHP;
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        Debug.Log($"[BaseHealth] Damage taken: {amount}, Remaining HP: {hp}");

        if (hp <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("[BaseHealth] GAME OVER! Base destroyed!");
        UIManager ui = FindObjectOfType<UIManager>();
        //if (ui != null) ui.ShowGameOver("Base destroyed");
    }
}
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public Transform enemy;
    public Vector3 offset;

    void Update()
    {
        if (enemy == null) return;
        slider.transform.position = Camera.main.WorldToScreenPoint(enemy.position + offset);
    }

    public void SetHealth(float health, float maxHealth)
    {
        slider.value = health / maxHealth;
    }
}

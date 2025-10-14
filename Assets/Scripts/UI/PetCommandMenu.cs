using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PetCommandMenu : MonoBehaviour
{
    public PetController petController;        // Reference to the active pet
    public PetAbilitySystem petAbilitySystem;  // For triggering special skill
    public Button followButton;
    public Button stayButton;
    public Button attackButton;
    public Button abilityButton;
    public TextMeshProUGUI statusText;

    private bool isFollowing = true;

    void Start()
    {
        // Assign button functions
        followButton.onClick.AddListener(FollowCommand);
        stayButton.onClick.AddListener(StayCommand);
        attackButton.onClick.AddListener(AttackCommand);
        abilityButton.onClick.AddListener(UseAbility);
    }

    void FollowCommand()
    {
        if (petController != null)
        {
            isFollowing = true;
            statusText.text = "🐾 Pet is following you!";
            Debug.Log("Pet following command activated.");
        }
    }

    void StayCommand()
    {
        if (petController != null)
        {
            isFollowing = false;
            petController.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
            statusText.text = "🐾 Pet is staying here.";
            Debug.Log("Pet stay command activated.");
        }
    }

    void AttackCommand()
    {
        if (petController != null)
        {
            GameObject enemy = GameObject.FindWithTag("Enemy");
            if (enemy != null)
            {
                petController.SetTarget(enemy.transform);
                statusText.text = "⚔️ Pet attacking enemy!";
                Debug.Log("Pet attack command activated.");
            }
            else
            {
                statusText.text = "No enemy found!";
            }
        }
    }

    void UseAbility()
    {
        if (petAbilitySystem != null)
        {
            StartCoroutine(petAbilitySystem.GetType()
                .GetMethod("ActivateAbility", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.Invoke(petAbilitySystem, null) as System.Collections.IEnumerator);
            statusText.text = "✨ Pet used special ability!";
        }
    }

    void Update()
    {
        // Handle following mode
        if (isFollowing && petController != null)
        {
            petController.FollowPlayer();
        }
    }
}

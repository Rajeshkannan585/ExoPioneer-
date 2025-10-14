using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    public string storyID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StoryManager.Instance.UnlockStory(storyID);
            StoryManager.Instance.ShowCurrentStory();
            Destroy(gameObject);
        }
    }
}

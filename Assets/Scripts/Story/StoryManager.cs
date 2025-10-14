using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance;

    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public AudioSource voiceSource;
    public List<StoryData> storySegments;
    private int currentStoryIndex = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadProgress();
        ShowCurrentStory();
    }

    public void ShowCurrentStory()
    {
        if (currentStoryIndex < storySegments.Count)
        {
            var story = storySegments[currentStoryIndex];
            if (!story.isUnlocked) return;

            titleText.text = story.title;
            descriptionText.text = story.description;
            if (story.voiceClip)
            {
                voiceSource.clip = story.voiceClip;
                voiceSource.Play();
            }
            story.isPlayed = true;
            SaveProgress();
        }
    }

    public void NextStory()
    {
        currentStoryIndex++;
        if (currentStoryIndex >= storySegments.Count)
            currentStoryIndex = storySegments.Count - 1;
        ShowCurrentStory();
    }

    public void UnlockStory(string id)
    {
        foreach (var s in storySegments)
        {
            if (s.storyID == id)
            {
                s.isUnlocked = true;
                SaveProgress();
                break;
            }
        }
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt("StoryIndex", currentStoryIndex);
        PlayerPrefs.Save();
    }

    public void LoadProgress()
    {
        currentStoryIndex = PlayerPrefs.GetInt("StoryIndex", 0);
    }
}

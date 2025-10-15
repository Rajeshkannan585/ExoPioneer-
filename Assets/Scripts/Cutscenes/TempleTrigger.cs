using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TempleTrigger : MonoBehaviour
{
    public enum Mode { Intro, Finale }
    public Mode mode;
    public TempleCutsceneDirector director;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        switch (mode)
        {
            case Mode.Intro:
                director?.PlayIntro();
                break;
            case Mode.Finale:
                director?.PlayFinale();
                break;
        }
        // Prevent re-trigger
        GetComponent<Collider>().enabled = false;
    }
}

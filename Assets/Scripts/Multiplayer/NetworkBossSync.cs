using UnityEngine;
using Unity.Netcode;   // use Unity Netcode for GameObjects

public class NetworkBossSync : NetworkBehaviour
{
    public SkyBossController boss;
    public BossMusicSystem bossMusic;
    public BossIntroCinematic bossIntro;

    private NetworkVariable<float> sharedHealth = new NetworkVariable<float>(5000f, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    private NetworkVariable<int> sharedPhase = new NetworkVariable<int>(1, 
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    void Start()
    {
        if (boss == null) boss = FindObjectOfType<SkyBossController>();
        if (IsServer)
        {
            sharedHealth.Value = boss.maxHealth;
        }
    }

    void Update()
    {
        if (!boss) return;

        // Server updates boss health
        if (IsServer)
        {
            sharedHealth.Value = boss.GetHealthPercent();
            sharedPhase.Value = boss.CurrentPhase;
        }
        else
        {
            // Clients mirror the server's state
            boss.SetRemoteHealth(sharedHealth.Value);
            boss.UpdateRemotePhase(sharedPhase.Value);
        }

        // Music sync for everyone
        if (bossMusic && IsServer)
        {
            if (boss.CurrentPhase == 2)
                UpdateMusicClientRpc(2);
            else if (boss.CurrentPhase == 3)
                UpdateMusicClientRpc(3);
        }
    }

    [ClientRpc]
    void UpdateMusicClientRpc(int phase)
    {
        if (bossMusic == null) return;
        if (phase == 2)
            bossMusic.SwitchToPhase(2);
        else if (phase == 3)
            bossMusic.SwitchToPhase(3);
    }

    [ClientRpc]
    public void TriggerBossIntroClientRpc(string name, string subtitle)
    {
        if (bossIntro != null)
            bossIntro.PlayIntro(name, subtitle);
    }

    [ClientRpc]
    public void TriggerBossDefeatClientRpc()
    {
        if (bossMusic != null)
            bossMusic.PlayVictoryMusic();
    }
}

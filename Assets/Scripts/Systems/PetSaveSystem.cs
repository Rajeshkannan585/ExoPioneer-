using UnityEngine;

public static class PetSaveSystem
{
    public static void SavePetData(PetLevelSystem petLevel, PetAbilitySystem petAbility, PetEvolution petEvolution)
    {
        PlayerPrefs.SetInt("PetLevel", petLevel.currentLevel);
        PlayerPrefs.SetInt("PetXP", petLevel.currentXP);
        PlayerPrefs.SetInt("PetAbility", (int)petAbility.petAbility);
        PlayerPrefs.SetInt("PetEvolved", petEvolution != null && petEvolution.enabled ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log("Pet data saved!");
    }

    public static void LoadPetData(PetLevelSystem petLevel, PetAbilitySystem petAbility, PetEvolution petEvolution)
    {
        petLevel.currentLevel = PlayerPrefs.GetInt("PetLevel", 1);
        petLevel.currentXP = PlayerPrefs.GetInt("PetXP", 0);
        petAbility.petAbility = (PetAbilitySystem.AbilityType)PlayerPrefs.GetInt("PetAbility", 0);

        bool evolved = PlayerPrefs.GetInt("PetEvolved", 0) == 1;
        if (evolved && petEvolution != null)
        {
            petEvolution.enabled = true;
        }

        Debug.Log("Pet data loaded!");
    }
}

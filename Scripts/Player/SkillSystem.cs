// ==================================
// SkillSystem.cs
// Handles XP, Leveling, and Skill Unlocks
// ==================================

using UnityEngine;
using System.Collections.Generic;

namespace ExoPioneer.PlayerSystem
{
    [System.Serializable]
    public class Skill
    {
        public string skillName;
        public string description;
        public int requiredLevel;
        public bool unlocked = false;
    }

    public class SkillSystem : MonoBehaviour
    {
        [Header("Level & XP")]
        public int currentLevel = 1;
        public int currentXP = 0;
        public int xpToNextLevel = 100;
        public int skillPoints = 0;

        [Header("Skills")]
        public List<Skill> skills = new List<Skill>();

        void Update()
        {
            // Debug test: press K to gain XP
            if (Input.GetKeyDown(KeyCode.K))
            {
                AddXP(50);
            }
        }

        public void AddXP(int amount)
        {
            currentXP += amount;
            Debug.Log("Gained " + amount + " XP. Total: " + currentXP);

            if (currentXP >= xpToNextLevel)
            {
                LevelUp();
            }
        }

        void LevelUp()
        {
            currentLevel++;
            currentXP -= xpToNextLevel;
            xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.2f);
            skillPoints++;

            Debug.Log("Level Up! Now Level " + currentLevel + ". Skill Points: " + skillPoints);
        }

        public void UnlockSkill(string skillName)
        {
            Skill skill = skills.Find(s => s.skillName == skillName);
            if (skill == null)
            {
                Debug.Log("No such skill: " + skillName);
                return;
            }

            if (skill.unlocked)
            {
                Debug.Log(skillName + " already unlocked!");
                return;
            }

            if (currentLevel >= skill.requiredLevel && skillPoints > 0)
            {
                skill.unlocked = true;
                skillPoints--;
                Debug.Log("Unlocked skill: " + skill.skillName);
            }
            else
            {
                Debug.Log("Cannot unlock skill: " + skill.skillName);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainerUI : MonoBehaviour
{
    [System.Serializable]
    public class Mission
    {
        public int id;
        public List<string> startDialogues; // Hội thoại khi bắt đầu nhiệm vụ
        public List<string> endDialogues;   // Hội thoại khi kết thúc nhiệm vụ
    }

    public GameObject trainer;
    public List<Mission> missions;
    public TMP_Text dialogueText;
    private int currentMissionIndex = -1;
    private int currentDialogueIndex = 0;
    private bool _isMissionStarting = true;

    private int _currentMissionId = -1;

    private void Start()
    {
        trainer.SetActive(false);
    }

    public void UpdateCurrentMission(int currentMissionId,bool isMissionStarting)
    {
        _isMissionStarting = isMissionStarting;
        bool isDialogContent = false; 
        currentMissionIndex = missions.FindIndex(m => m.id == currentMissionId);
        if (isMissionStarting)
        {
            //isMissionStarting = true;
            if (currentMissionIndex != -1)
            {
                isDialogContent = missions[currentMissionIndex].startDialogues != null && missions[currentMissionIndex].startDialogues.Count > 0;
            }
            
        }
        else
        {
            //isMissionStarting = false;
            // Check if there is any dialogue content at the end of the mission
            if (currentMissionIndex != -1)
            {
                isDialogContent = missions[currentMissionIndex].endDialogues != null && missions[currentMissionIndex].endDialogues.Count > 0;
                GameManager.Instance.isRun = false;
            }
                
        }
        _currentMissionId = currentMissionId;
        if (currentMissionIndex != -1 && isDialogContent)
        {
            currentDialogueIndex = 0;
            trainer.SetActive(true);
            UpdateDialogueText();
        }
        else//không có hội thoại 
        {
            if (isMissionStarting) return;
            
            TrainingMissionManager.Instance.WeaponTraining.CompleteMission(_currentMissionId);
        }
    }

    public void NextContent()
    {
        List<string> dialogues = _isMissionStarting ? missions[currentMissionIndex].startDialogues : missions[currentMissionIndex].endDialogues;

        if (currentDialogueIndex < dialogues.Count - 1)
        {
            // Move to the next dialogue
            currentDialogueIndex++;
            UpdateDialogueText();
        }
        else
        {
            // End of dialogues for the current phase
            if (_isMissionStarting)
            {
                // If it was the start phase, we might need to auto-start the end dialogues or wait for event
                currentDialogueIndex = 0;
                trainer.SetActive(false);
                GameManager.Instance.isRun = true;
            }
            else
            {
                // Mission ending phase
                TrainingMissionManager.Instance.WeaponTraining.CompleteMission(_currentMissionId);
                trainer.SetActive(false);
                _currentMissionId = -1; // Reset mission ID
                GameManager.Instance.isRun = true;

            }
        }
    }

    private void UpdateDialogueText()
    {
        List<string> dialogues = _isMissionStarting ? missions[currentMissionIndex].startDialogues : missions[currentMissionIndex].endDialogues;
        if (dialogues.Count > 0 && currentDialogueIndex < dialogues.Count)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
        }
        else
        {
            // Handle no dialogues or invalid index
            Debug.LogError("No dialogues available or index out of range.");
            trainer.SetActive(false);
        }
    }
}

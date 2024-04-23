using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public string dialogueChapter = "1";
    public string dialogueStart = "s1";
    public InputActionAsset inputActionAsset;

    public TextMeshProUGUI nameText;
    public Image portraitImage;
    public TextMeshProUGUI dialogueText;
    public GameObject choicesParent;
    public TextAsset textAsset;
    
    public TextAsset textAssetYes;
    
    public TextAsset textAssetNo;
    private Dialogue dialogue;
    private Queue<string> sentences = new Queue<string>();
    private int selectedIndex = 0;
    
    bool isDelay = false;

    void Start()
    {
        if (dialogueChapter == "2" && SaveManager.Instance.CurrentSave.newgameplus > 0)
        {
            dialogueChapter = "2alt";
        }
        loadDialogue(dialogueStart);
    }


    public void loadDialogue(string name) {

        choicesParent.SetActive(false);
        TextAsset jsonAsset = Resources.Load<TextAsset>("Dialogues/"+dialogueChapter+"/"+name); 

        string json = jsonAsset.text;
        dialogue = JsonUtility.FromJson<Dialogue>(json);
        choicesParent.SetActive(false);
        StartDialogue();
    }

    public void StartDialogue()
    {
        portraitImage.material = Resources.Load<Material>("Characters/"+dialogue.name+" Material");
        nameText.text = dialogue.name;
        // portraitImage.sprite = dialogue.icon;

        sentences.Clear();

        foreach (string sentence in dialogue.text)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Peek();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(0.01f);  
        }
    }

    void EndDialogue()
    {
        if (dialogue.choices != null)  {
            showChoices();
        }
        else if (dialogue.next != null)
        {
            loadDialogue(dialogue.next);
        }
        else {
            this.gameObject.SetActive(false);
        }
    }

    void showChoices()
    {
        choicesParent.SetActive(true);
        for (int i = 0; i < choicesParent.transform.childCount; i++)
        {
            Transform child = choicesParent.transform.GetChild(i);
            TextMeshProUGUI choiceText = child.GetComponent<TextMeshProUGUI>();
            if (i >= dialogue.choices.Length) {
                choiceText.text = "";
            }
            else {
                choiceText.text = dialogue.choices[i].text;
            }
        }
        highlightText(selectedIndex);
    }

    void highlightText(int index) {
        if (dialogue.choices != null) {
            for (int i = 0; i < choicesParent.transform.childCount; i++)
            {
                Transform child = choicesParent.transform.GetChild(i);
                TextMeshProUGUI choiceText = child.GetComponent<TextMeshProUGUI>();
                if (choiceText.text == dialogue.choices[index].text) {
                    choiceText.color = Color.red;
                }
                else {
                    choiceText.color = Color.white;
                }
            }
        }
    }

    void makeChoice(Dialogue.Choice choice) {
        // Debug.Log("Made choice : " + choice.text);

        if (choice.text == "To serve you.")
        {
            GameController controller = GameObject.Find("GameController").GetComponent<GameController>();
            controller.nextStageUnlock = "Stage 3-A";
        }
        else if (choice.text == "To stop you.")
        {
            GameController controller = GameObject.Find("GameController").GetComponent<GameController>();
            controller.nextStageUnlock = "Stage 3-B";
        }
        else if (choice.text == "Neither.")
        {
            GameController controller = GameObject.Find("GameController").GetComponent<GameController>();
            controller.nextStageUnlock = "Stage 4-C";
        }
    }

    void Update()
    {
        if (choicesParent.activeSelf)
        {
            if (!isDelay) 
            {
                if ( inputActionAsset["Movement"].ReadValue<Vector3>().x < 0 || inputActionAsset["Movement"].ReadValue<Vector3>().z < 0 )
                {
                    selectedIndex -= 1;
                    selectedIndex = selectedIndex % dialogue.choices.Length;
                    if (selectedIndex == -1) selectedIndex = dialogue.choices.Length - 1;
                    highlightText(selectedIndex); 
                    isDelay = true;
                    StartCoroutine(DelayAction()); 

                }
                else if ( inputActionAsset["Movement"].ReadValue<Vector3>().x > 0 || inputActionAsset["Movement"].ReadValue<Vector3>().z > 0 )
                {
                    selectedIndex += 1;
                    selectedIndex = selectedIndex % dialogue.choices.Length;
                    highlightText(selectedIndex); 
                    isDelay = true;
                    StartCoroutine(DelayAction()); 
                }
            }
            

            if (inputActionAsset["Fire"].triggered)
            {
                makeChoice(dialogue.choices[selectedIndex]);
                loadDialogue(dialogue.choices[selectedIndex].next);
            }

        }
        else {
            if (inputActionAsset["Fire"].triggered)
            {
                if (dialogueText.text == sentences.Peek())
                {
                    sentences.Dequeue();
                    DisplayNextSentence();
                }
                else
                {
                    StopAllCoroutines();
                    dialogueText.text = sentences.Peek();
                }
            }
        }
    }

    IEnumerator DelayAction()
    {
        yield return new WaitForSeconds(0.25f); 
        isDelay = false;
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Hangman : MonoBehaviour
{
    public TextMeshProUGUI displayedWord; 
    public Image[] hangmanImages; 
    public TMP_InputField inputField; 
    public TextMeshProUGUI outputText; 

    private string[] wordList = { "BÝLGÝSAYAR", "KABLO", "DART", "DEÐER", "GELÝÞTÝRME" }; 
    private string targetWord; 
    private char[] displayedLetters; 
    private int wrongGuessCount = 0; 
    private int maxWrongGuesses; 
    private HashSet<char> guessedLetters; 

    void Start()
    {
        SelectRandomWord(); 
        maxWrongGuesses = hangmanImages.Length; 
        displayedLetters = new char[targetWord.Length];
        for (int i = 0; i < displayedLetters.Length; i++)
        {
            displayedLetters[i] = '_'; 
        }
        UpdateDisplayedWord();

        
        foreach (Image img in hangmanImages)
        {
            img.enabled = false;
        }

        
        guessedLetters = new HashSet<char>();

        
        inputField.onEndEdit.AddListener(OnInputFieldSubmit);
    }

    
    void SelectRandomWord()
    {
        int randomIndex = Random.Range(0, wordList.Length); 
        targetWord = wordList[randomIndex]; 
    }

    
    public void OnInputFieldSubmit(string input)
    {
        
        if (!string.IsNullOrEmpty(input) && input.Length == 1)
        {
            char guessedLetter = char.ToUpper(input[0]); 

            
            outputText.text = "Girdiðiniz harf: " + guessedLetter;

            
            if (!guessedLetters.Contains(guessedLetter))
            {
                guessedLetters.Add(guessedLetter); 

                bool correctGuess = false; 

                
                for (int i = 0; i < targetWord.Length; i++)
                {
                    if (char.ToUpper(targetWord[i]) == guessedLetter) 
                    {
                        displayedLetters[i] = targetWord[i]; 
                        correctGuess = true; 
                    }
                }

                
                if (!correctGuess)
                {
                    wrongGuessCount++;
                    if (wrongGuessCount <= maxWrongGuesses)
                    {
                        hangmanImages[wrongGuessCount - 1].enabled = true; 
                    }

                    
                    if (wrongGuessCount == maxWrongGuesses)
                    {
                        Debug.Log("Kaybettin");
                    }
                }

                UpdateDisplayedWord(); 
            }
            else
            {
                outputText.text = "Bu harfi daha önce tahmin ettiniz."; 
            }

            inputField.text = ""; 
            inputField.ActivateInputField(); 
        }
    }




    void UpdateDisplayedWord()
    {
        displayedWord.text = new string(displayedLetters); // Ekranda kelimeyi güncelle
    }
}

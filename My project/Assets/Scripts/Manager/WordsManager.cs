using UnityEngine;
using System.Collections.Generic;

public static class WordsManager
{
    static List<TextDataBox> textDataList = new List<TextDataBox>();
    
    public static void AddWords(int wordsID,int wordsCount)
    {
        if(wordsID < 1 || wordsCount <= 0)
        {
            return;
        }
        TextDataBox textData = textDataList.Find(t => t.whichWords == wordsID);
        if (textData != null)
        {
            textData.wordsCount += wordsCount;
        }
        else
        {
            TextDataBox newDataBox = new TextDataBox();
            newDataBox.wordsCount = wordsCount;
            newDataBox.whichWords = wordsID;
            textDataList.Add(newDataBox);
        }
    }

    public static List<TextDataBox> AllGetWords()
    {
        List<TextDataBox> saveWords = new List<TextDataBox>(textDataList);
        return saveWords;
    }

    public static void LoadWords(List<TextDataBox> wordsList)
    {
        if (wordsList == null)
        {
            return;
        }
        textDataList.Clear();
        foreach (TextDataBox t in wordsList)
        {
            if(t == null)
            {
                continue;
            }
            AddWords(t.whichWords,t.wordsCount);
        }
    }

    public static bool UseWords(int wordsID)
    {
        if(wordsID < 1)
        {
            return false;
        }
        TextDataBox dataBox = textDataList.Find(t =>t.whichWords == wordsID);
        if (dataBox == null || dataBox.wordsCount < 1)
        {
            return false;
        }
        dataBox.wordsCount--;
        if(dataBox.wordsCount <= 0)
        {
            textDataList.Remove(dataBox);
        }
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.DataPersistence;
using DefaultNamespace.DataPersistence.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookUI : MonoBehaviour
{
    [SerializeField] private Image bookImage;
    [SerializeField] private TextMeshProUGUI bookNameText;
    [SerializeField] private TextMeshProUGUI bookAuthorText;
    
    public void SetBookProperties(string bookName, string bookAuthor, Sprite bookIcon)
    {
        bookNameText.text = bookName;
        bookAuthorText.text = bookAuthor;
        bookImage.sprite = bookIcon;
    }

}

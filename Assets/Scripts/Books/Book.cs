using UnityEngine;

[System.Serializable]
public class Book
{
    public string title;
    public string author;
    public Sprite coverImage; 
    
    public Book(string title, string author, Sprite coverImage)
    {
        this.title = title;
        this.author = author;
        this.coverImage = coverImage;
    }
}


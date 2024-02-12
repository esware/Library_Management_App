using UnityEngine;

[System.Serializable]
public class Book
{
    public int CopyCount { get; set; }
    public int BorrowedCopies { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public Sprite icon { get; set; } 
    
    public Book(string title, string author, Sprite icon)
    {
        this.Title = title;
        this.Author = author;
        this.icon = icon;
    }
}


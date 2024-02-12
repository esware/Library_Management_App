using System;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace.DataPersistence;
using DefaultNamespace.DataPersistence.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Library:MonoBehaviour,IDataPersistence
{
    private static Library _instance;
    public static Library Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject libraryObject = new GameObject("Library");
                _instance = libraryObject.AddComponent<Library>();
            }
            return _instance;
        }
    }
    
    public List<Book> books = new List<Book>();

    [SerializeField] private BookUI bookPrefab;
    [SerializeField] private Transform contentBookTransform;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void AddBook(Book book)
    {
        books.Add(book);
        var bookUI = Instantiate(bookPrefab,contentBookTransform);
        bookUI.SetBookProperties(book.Title,book.Author,book.icon);
        Console.WriteLine("Kitap başarıyla eklendi.");
    }

    public void ViewAllBooks()
    {
        foreach (var book in books)
        {
            
        }
    }

    public void SearchBook(string key)
    {
        var bulunanKitaplar = books.FindAll(k => k.Title.Contains(key) || k.Author.Contains(key));
        if (bulunanKitaplar.Count > 0)
        {
            Console.WriteLine("Arama Sonuçları:");
            foreach (var kitap in bulunanKitaplar)
            {
                Console.WriteLine($"Başlık: {kitap.Title}," +
                                  $" Yazar: {kitap.Author}, " +
                                  $"ISBN: {kitap.ISBN}," +
                                  $" Kopya Sayısı: {kitap.CopyCount}," +
                                  $" Ödünç Alınan Kopyalar: {kitap.BorrowedCopies}");
            }
        }
        else
        {
            Console.WriteLine("Aranan kriterlere uygun kitap bulunamadı.");
        }
    }

    public void BorrowBook(string isbn)
    {
        var kitap = books.Find(k => k.ISBN == isbn);
        if (kitap != null && kitap.CopyCount > kitap.BorrowedCopies)
        {
            kitap.BorrowedCopies++;
            Console.WriteLine("Kitap ödünç alındı.");
        }
        else
        {
            Console.WriteLine("Kitap ödünç alınamadı. Tüm kopyalar ödünçte veya kitap bulunamadı.");
        }
    }

    public void ReturnBook(string isbn)
    {
        var kitap = books.Find(k => k.ISBN == isbn);
        if (kitap != null && kitap.BorrowedCopies > 0)
        {
            kitap.BorrowedCopies--;
            Console.WriteLine("Kitap iade edildi.");
        }
        else
        {
            Console.WriteLine("Kitap iade edilemedi. Ödünç alınmış kopya bulunamadı veya kitap bulunamadı.");
        }
    }

    public void ViewPastBooks()
    {
        var gecmisKitaplar = books.FindAll(k => k.BorrowedCopies > 0);
        if (gecmisKitaplar.Count > 0)
        {
            foreach (var kitap in gecmisKitaplar)
            {
                Console.WriteLine($"Başlık: {kitap.Title}," +
                                  $" Yazar: {kitap.Author}, " +
                                  $"ISBN: {kitap.ISBN}," +
                                  $" Kopya Sayısı: {kitap.CopyCount}," +
                                  $" Ödünç Alınan Kopyalar: {kitap.BorrowedCopies}");
            }
        }
        else
        {
            Console.WriteLine("Ödünç alınmış kitap bulunmamaktadır.");
        }
    }

    public void LoadData(LibraryData data)
    {
        Debug.Log("Loaded book");

        foreach (var book in data.books)
        {
            var bookUI = Instantiate(bookPrefab,contentBookTransform);
            var value = book.Value;
            bookUI.SetBookProperties(value.bookName,value.bookAuthor,value.bookIcon);
        }
    }

    public void SaveData(ref LibraryData data)
    {
        Debug.Log("Saved Book");
        foreach (var localBook in books)
        {
            BookData bookData = new BookData();
            data.books.TryGetValue(localBook.ISBN, out bookData);
            if (bookData!=null)
            {
                data.books.Remove(localBook.ISBN);
            }
            else
            {
                
                bookData.bookName = localBook.Title;
                bookData.bookAuthor = localBook.Author;
                bookData.bookIcon = localBook.icon;
                bookData.ISBN = localBook.ISBN;
                data.books.Add(bookData.ISBN, new BookData());
            }
        }
    }
}

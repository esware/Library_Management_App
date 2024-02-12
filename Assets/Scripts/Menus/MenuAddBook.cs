using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Menus
{
    public class MenuAddBook : MonoBehaviour
    {
        [SerializeField] public InputField bookTitle;
        [SerializeField] private InputField bookAuthor;
        [SerializeField] private Button bookImageButton;
        [SerializeField] private Button addBookButton;

        private string _bookTitle;
        private string _bookAuthor;
        private Sprite _bookImage;
        
        private void Awake()
        {
            bookImageButton.onClick.AddListener(OpenFileSelection);
            addBookButton.onClick.AddListener(AddBook);
            bookTitle.onEndEdit.AddListener(GetBookTitle);
            bookAuthor.onEndEdit.AddListener(GetBookAuthor);
        }

        private void GetBookTitle(string input)
        {
            Debug.Log("Girilen metin: " + input);
            _bookTitle = input;
        }
        private void GetBookAuthor(string input)
        {
            Debug.Log("Girilen metin: " + input);
            _bookAuthor = input;
        }
        
        public void OpenFileSelection()
        {
#if UNITY_EDITOR
            var path = EditorUtility.OpenFilePanel("Select File", "", "");

            if (!string.IsNullOrEmpty(path))
            {
                byte[] fileData = File.ReadAllBytes(path);
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(fileData);

                if (texture != null)
                {
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                    
                    _bookImage = sprite;

                    Debug.Log("Selected Image: " + path);
                }
                else
                {
                    Debug.LogError("Failed to load image.");
                }
            }
            else
            {
                Debug.Log("File selection cancelled.");
            }
#endif
        }

        private void AddBook()
        {
            if (_bookImage!=null && bookTitle!=null && bookAuthor!=null)
            {
                Library.Instance.AddBook(new Book(_bookTitle,_bookAuthor,_bookImage));
                Debug.Log($"The book has been added to the library.");
            }
            else
            {
                Debug.Log($"Please enter all information about the book!");
            }
            
            bookTitle.text = "Enter the title of the book";
            bookAuthor.text = "Enter the author of the book";
        }
    }
}
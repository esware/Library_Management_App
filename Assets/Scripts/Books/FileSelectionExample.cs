using UnityEngine;
using UnityEditor;

public class FileSelectionExample : MonoBehaviour
{
    private void OnGUI()
    {
        if (GUILayout.Button("Open File Selection"))
        {
            OpenFileSelection();
        }
    }

    public void OpenFileSelection()
    {
        string path = EditorUtility.OpenFilePanel("Select File", "", "");
        
        if (!string.IsNullOrEmpty(path))
        {
            Debug.Log("Selected File: " + path);
            
        }
        else
        {
            Debug.Log("File selection cancelled.");
        }
    }
}


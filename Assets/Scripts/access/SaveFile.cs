using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveFile : MonoBehaviour {
    public SaveControl saveControl;
	// Use this for initialization
	void Start () {
		
	}

    private void OnMouseDown()
    {
        IFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream("Assets/Save/"+MoveBorder.currentIndex+".bin", 
            FileMode.Create, FileAccess.Write);
        SaveModel save = new SaveModel();
        formatter.Serialize(stream, save);
        saveControl.SetPaneInfo(saveControl.numberPaneReflect[MoveBorder.currentIndex], save);
        stream.Close();
    }
}
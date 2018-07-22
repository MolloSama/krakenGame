using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMusicControl : MonoBehaviour
{

    public GameObject globalUIMusic;
    private static GameObject preMusic;
    GameObject MyUIMusic;

    private void Start()
    {
        
        if (preMusic == null)
        {
            MyUIMusic = (GameObject)Instantiate(globalUIMusic);
            preMusic = MyUIMusic;
        }
        print(preMusic.name.Replace("(Clone)", "") + " " + globalUIMusic.name);
        if (!preMusic.name.Replace("(Clone)", "").Equals(globalUIMusic.name))
        {
            Destroy(preMusic);
            MyUIMusic = (GameObject)Instantiate(globalUIMusic);
            preMusic = MyUIMusic;
        }
    }
}

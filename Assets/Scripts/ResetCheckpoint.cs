using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetCheckpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    
    void TaskOnClick()
    {
        PlayerPrefs.SetInt("checkpoint", 0);
        RestartScript.DestroyAllObjects();
        SceneManager.LoadScene("SampleScene");
    }
}

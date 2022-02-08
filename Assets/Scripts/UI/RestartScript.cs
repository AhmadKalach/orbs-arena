using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
   
    void TaskOnClick()
    {
        DestroyAllObjects();
        SceneManager.LoadScene("SampleScene");
    }

    public static void DestroyAllObjects()
    {

        CreateOnDestroy[] enemies = FindObjectsOfType<CreateOnDestroy>();
        foreach (CreateOnDestroy go in enemies)
        {
            Destroy(go);
        }

        ProjectileBehavior[] projectiles = FindObjectsOfType<ProjectileBehavior>();
        foreach (ProjectileBehavior go in projectiles)
        {
            Destroy(go);
        }
    }
}

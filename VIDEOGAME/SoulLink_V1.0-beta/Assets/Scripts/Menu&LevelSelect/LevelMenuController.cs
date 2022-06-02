/*
Code that instantiates uploaded levels so they can be accesed in game. It does
so by using the scrolldown TMPro menu.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum PrefabType { Text, Button }

public class LevelMenuController : MonoBehaviour
{
    [SerializeField] PrefabType type;
    [SerializeField] GameObject textPrefab;
    [SerializeField] Transform contentTransform;
    [SerializeField] TextMeshProUGUI greetField;

    public void LoadNames(LevelList allLevels)
    {
        ClearContents();
        for (int i=0; i<allLevels.levels.Count; i++) {
            // Create new text objects
            GameObject uiItem = Instantiate(textPrefab);
            // Add them to the ScollView content
            uiItem.transform.SetParent(contentTransform);

            // Set the position of each element
            RectTransform rectTransform = uiItem.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2 (0, -50 * i);

            // Extract the text from the argument object
            Level lv = allLevels.levels[i];
            //Debug.Log("ID: " + us.id_users + " | " + us.name + " " + us.surname);

            if (type == PrefabType.Button) {
                // Set the text
                TextMeshProUGUI field = uiItem.GetComponentInChildren<TextMeshProUGUI>();
                field.text = "ID: " + lv.levelID + " | " + lv.levelName;
                // Set the callback
                Button btn = uiItem.GetComponent<Button>();
		        btn.onClick.AddListener(delegate {LoadLevel(lv.roomLayout, lv.enemyLayout, lv.objectLayout); });
            } else if (type == PrefabType.Text) {
                TextMeshProUGUI field = uiItem.GetComponent<TextMeshProUGUI>();
                field.text = "ID: " + lv.levelID + " | " + lv.levelName;
            }
        }
    }

    // Delete any child objects
    void ClearContents()
    {
        foreach (Transform child in contentTransform) {
            Destroy(child.gameObject);
        }
    }

    void LoadLevel(string rooms, string enemies, string objects)
    {
        LevelInformation.levelRooms = rooms;
        LevelInformation.levelEnemies = enemies;
        LevelInformation.levelObstacles = objects;
        SceneManager.LoadScene("Level");

    }
}

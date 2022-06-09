/*
Code that instantiates uploaded levels so they can be accesed in game. It does
so by using the scrolldown TMPro menu.

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum PrefabType { Text, Button }

public class LevelMenuController : MonoBehaviour
{
    // [SerializeField] PrefabType type;
    // [SerializeField] GameObject textPrefab;
    // [SerializeField] Transform contentTransform;
    // [SerializeField] TextMeshProUGUI greetField;


    public GameObject levelHolder;
    public Button levelIcon;
    public GameObject thisCanvas;
    public int numberOfLevels;
    public Vector2 iconSpacing;
    private Rect panelDimensions;
    private Rect iconDimensions;
    private int amountPerPage;
    private int currentLevelCount;

    private void Start() {
        panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
        iconDimensions = levelIcon.GetComponent<RectTransform>().rect;
        int maxInARow = Mathf.FloorToInt((panelDimensions.width + iconSpacing.x) / (iconDimensions.width + iconSpacing.x));
        int maxInACol = Mathf.FloorToInt((panelDimensions.height + iconSpacing.y) / (iconDimensions.height + iconSpacing.y));
        amountPerPage = maxInARow * maxInACol;
    }

    public void LoadNames(LevelList allLevels)
    {
        Destroy(levelHolder.GetComponent<PageSwiper>());
        currentLevelCount = 0;
        GameObject[] pages = GameObject.FindGameObjectsWithTag("Page");
        if (pages != null)
        {
            for (int i = 0; i < pages.Length; i++)
            {
                Destroy(pages[i]);
            }
        }

        numberOfLevels = allLevels.levels.Count;
        int totalPages = Mathf.CeilToInt((float)numberOfLevels / amountPerPage);
        GameObject panelClone = Instantiate(levelHolder) as GameObject;
        PageSwiper swiper = levelHolder.AddComponent<PageSwiper>();
        swiper.totalPages = totalPages;

        for(int i = 1; i <= totalPages; i++){
            GameObject panel = Instantiate(panelClone) as GameObject;
            panel.transform.SetParent(thisCanvas.transform, false);
            panel.transform.SetParent(levelHolder.transform);
            foreach (Transform child in panel.transform)
            {
                Destroy(child.gameObject);
            }
            panel.name = "Page-" + i;
            panel.tag = "Page";
            panel.GetComponent<RectTransform>().localPosition = new Vector2(panelDimensions.width * (i-1), 0);
            SetUpGrid(panel);
            int numberOfIcons = i == totalPages ? numberOfLevels - currentLevelCount : amountPerPage;
            Debug.Log(numberOfIcons);
            LoadIcons(currentLevelCount, numberOfIcons + currentLevelCount, panel, allLevels);
        }
        Destroy(panelClone);

        // for (int i=0; i<allLevels.levels.Count; i++) {
        //     // Create new text objects
        //     GameObject uiItem = Instantiate(textPrefab);
        //     // Add them to the ScollView content
        //     uiItem.transform.SetParent(contentTransform);

        //     // Set the position of each element
        //     RectTransform rectTransform = uiItem.GetComponent<RectTransform>();
        //     rectTransform.anchoredPosition = new Vector2 (0, -50 * i);

        //     // Extract the text from the argument object
        //     Level lv = allLevels.levels[i];
        //     //Debug.Log("ID: " + us.id_users + " | " + us.name + " " + us.surname);

        //     if (type == PrefabType.Button) {
        //         // Set the text
        //         TextMeshProUGUI field = uiItem.GetComponentInChildren<TextMeshProUGUI>();
        //         field.text = "ID: " + lv.levelID + " | " + lv.levelName;
        //         // Set the callback
        //         Button btn = uiItem.GetComponent<Button>();
		//         btn.onClick.AddListener(delegate {LoadLevel(lv.roomLayout, lv.enemyLayout, lv.objectLayout); });
        //     } else if (type == PrefabType.Text) {
        //         TextMeshProUGUI field = uiItem.GetComponent<TextMeshProUGUI>();
        //         field.text = "ID: " + lv.levelID + " | " + lv.levelName;
        //     }
        // }
    }

    // Delete any child objects
    // void ClearContents()
    // {
    //     foreach (Transform child in contentTransform) {
    //         Destroy(child.gameObject);
    //     }
    // }

    void LoadLevel(string rooms, string enemies, string objects)
    {
        LevelInformation.levelRooms = rooms;
        LevelInformation.levelEnemies = enemies;
        LevelInformation.levelObstacles = objects;
        SceneManager.LoadScene("Level");

    }

    void SetUpGrid(GameObject panel){
        GridLayoutGroup grid = panel.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(iconDimensions.width, iconDimensions.height);
        grid.childAlignment = TextAnchor.MiddleCenter;
        grid.spacing = iconSpacing;
    }

    void LoadIcons(int start, int numberOfIcons, GameObject parentObject, LevelList lv){
        for(int i = start; i < numberOfIcons; i++){
            Level level = lv.levels[i];
            currentLevelCount++;
            Button icon = Instantiate(levelIcon) as Button;
            icon.transform.SetParent(thisCanvas.transform, false);
            icon.transform.SetParent(parentObject.transform);
            icon.name = "ID: " + level.levelID;
            icon.GetComponentInChildren<TextMeshProUGUI>().SetText(level.levelName);
            icon.onClick.AddListener(delegate {LoadLevel(level.roomLayout, level.enemyLayout, level.objectLayout);});
        }
    }
}

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

// Prefab of buttons that will be displayed on level select menu
public enum PrefabType { Text, Button }

public class LevelMenuController : MonoBehaviour
{
    // Variable that stores panel where levels will appear
    public GameObject levelHolder;
    // Variable that stores button
    public Button levelIcon;
    // Game object that stores canvas of menu
    public GameObject thisCanvas;
    // Variable that stores number of levels available
    public int numberOfLevels;
    // Vector that handles level button spacing
    public Vector2 iconSpacing;
    // Variable that stores panel dimensions
    private Rect panelDimensions;
    // Variable that stores level button dimensions
    private Rect iconDimensions;
    // Variable that stores the amount of levels that can be put in one panel
    private int amountPerPage;
    // Variable that stores current level count
    private int currentLevelCount;

    private void Start() 
    {
        // Dimensions of panel and buttons are called on start
        panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
        iconDimensions = levelIcon.GetComponent<RectTransform>().rect;
        // Number of buttons that can be placed in a panel is calculated
        int maxInARow = Mathf.FloorToInt((panelDimensions.width + iconSpacing.x) / (iconDimensions.width + iconSpacing.x));
        int maxInACol = Mathf.FloorToInt((panelDimensions.height + iconSpacing.y) / (iconDimensions.height + iconSpacing.y));
        amountPerPage = maxInARow * maxInACol;
    }

    // Function that loads levels per panel according to amount per page
    public void LoadNames(LevelList allLevels)
    {
        // Checks if panel already has page swiper and returns position to start
        // in case of level search.
        if (levelHolder.GetComponent<PageSwiper>() != null)
        {
            PageSwiper oldSwiper = levelHolder.GetComponent<PageSwiper>();
            oldSwiper.StartCoroutine(oldSwiper.SmoothMove(oldSwiper.transform.position, oldSwiper.startingPos, 0));
        }
        // If swiper already exists, it is deleted
        Destroy(levelHolder.GetComponent<PageSwiper>());
        // Current level count is reset
        currentLevelCount = 0;
        // Gets all previous pages and, in case of level search, destroys all
        // old pages
        GameObject[] pages = GameObject.FindGameObjectsWithTag("Page");
        if (pages != null)
        {
            for (int i = 0; i < pages.Length; i++)
            {
                Destroy(pages[i]);
            }
        }

        // Number of levels is gotten
        numberOfLevels = allLevels.levels.Count;
        // Total pages are calcculated
        int totalPages = Mathf.CeilToInt((float)numberOfLevels / amountPerPage);
        // Panel clone is instantiated
        GameObject panelClone = Instantiate(levelHolder) as GameObject;
        // Page swiper component is added
        PageSwiper swiper = levelHolder.AddComponent<PageSwiper>();
        // Total pages are set on swiper
        swiper.totalPages = totalPages;

        for(int i = 1; i <= totalPages; i++){
            // Panel is instantiated
            GameObject panel = Instantiate(panelClone) as GameObject;
            // Panel parent is set to level holder
            panel.transform.SetParent(thisCanvas.transform, false);
            panel.transform.SetParent(levelHolder.transform);
            // All previous objects, in case of level search, are deleted.
            foreach (Transform child in panel.transform)
            {
                Destroy(child.gameObject);
            }
            // Panel name is set
            panel.name = "Page-" + i;
            // Panel tag is set
            panel.tag = "Page";
            // Panel transform pos is set according to number of page
            panel.GetComponent<RectTransform>().localPosition = new Vector2(panelDimensions.width * (i-1), 0);
            // Grid is set up inside panel
            SetUpGrid(panel);
            // Number of icons is updated depending on current page
            int numberOfIcons = i == totalPages ? numberOfLevels - currentLevelCount : amountPerPage;
            // Level buttons are loaded
            LoadIcons(currentLevelCount, numberOfIcons + currentLevelCount, panel, allLevels);
        }
        // Panel clone is destroyed
        Destroy(panelClone);
    }

    // Load level function that is added to each button with the corresponding
    // level data
    void LoadLevel(string rooms, string enemies, string objects)
    {
        LevelInformation.levelRooms = rooms;
        LevelInformation.levelEnemies = enemies;
        LevelInformation.levelObstacles = objects;
        SceneManager.LoadScene("Level");

    }

    // Function that sets up grid inside panel
    void SetUpGrid(GameObject panel){
        // Grd is set up according to button dimensions
        GridLayoutGroup grid = panel.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(iconDimensions.width, iconDimensions.height);
        grid.childAlignment = TextAnchor.MiddleCenter;
        grid.spacing = iconSpacing;
    }

    // Function that loads buttons according to database stored levels
    void LoadIcons(int start, int numberOfIcons, GameObject parentObject, LevelList lv){
        // Buttons are loaded with load level function pre set inside them
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

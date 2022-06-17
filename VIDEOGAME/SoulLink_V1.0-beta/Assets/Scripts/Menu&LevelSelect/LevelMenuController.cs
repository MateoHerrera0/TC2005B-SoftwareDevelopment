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
    // Gam object that stores canvas of menu
    public GameObject thisCanvas;
    public int numberOfLevels;
    public Vector2 iconSpacing;
    private Rect panelDimensions;
    private Rect iconDimensions;
    private int amountPerPage;
    private int currentLevelCount;

    private void Start() 
    {
        panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
        iconDimensions = levelIcon.GetComponent<RectTransform>().rect;
        int maxInARow = Mathf.FloorToInt((panelDimensions.width + iconSpacing.x) / (iconDimensions.width + iconSpacing.x));
        int maxInACol = Mathf.FloorToInt((panelDimensions.height + iconSpacing.y) / (iconDimensions.height + iconSpacing.y));
        amountPerPage = maxInARow * maxInACol;
    }

    public void LoadNames(LevelList allLevels)
    {
        if (levelHolder.GetComponent<PageSwiper>() != null)
        {
            PageSwiper oldSwiper = levelHolder.GetComponent<PageSwiper>();
            oldSwiper.StartCoroutine(oldSwiper.SmoothMove(oldSwiper.transform.position, oldSwiper.startingPos, 0));
        }
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
    }

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

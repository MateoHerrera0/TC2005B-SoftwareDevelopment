/*
Code that controls page swipe mechanic in level select menu

Ana Paula Katsuda, Mateo Herrera & Gerardo Gutiérrez
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// This class uses IDragHandler and IEndDragHandler to capture mouse movement.
public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler{
    // Position of of main panel
    private Vector3 panelLocation;
    // Staring position of main panel
    public Vector3 startingPos;
    // Threshhold until page is swiped
    public float percentThreshold = 0.2f;
    // Speed in which page is changed
    public float easing = 0.5f;
    // Number of total pages, default is 1
    public int totalPages = 1;
    // Number of current page
    private int currentPage = 1;

    // Start is called before the first frame update
    void Start(){
        panelLocation = transform.position;
        startingPos = transform.position;
    }

    // Function that changes position while mouse is being dragged
    public void OnDrag(PointerEventData data){
        float difference = data.pressPosition.x - data.position.x;
        transform.position = panelLocation - new Vector3(difference, 0, 0);
    }

    // Function that calculates if page is above threshold when mouse stops
    //  being dragged. If it is above threshold current page is changed to next
    // and page is changed.
    public void OnEndDrag(PointerEventData data){
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        if(Mathf.Abs(percentage) >= percentThreshold){
            Vector3 newLocation = panelLocation;
            if(percentage > 0 && currentPage < totalPages){
                currentPage++;
                newLocation += new Vector3(-Screen.width, 0, 0);
            }else if(percentage < 0 && currentPage > 1){
                currentPage--;
                newLocation += new Vector3(Screen.width, 0, 0);
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
        }else{
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }
    }
    
    // This couroutine helps with the smooth transition of pages.
    public IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds){
        float t = 0f;
        while(t <= 1.0){
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}
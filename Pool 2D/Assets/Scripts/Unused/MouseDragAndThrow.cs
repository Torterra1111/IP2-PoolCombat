using UnityEngine;
using System.Collections;

public class MouseDragAndThrow : MonoBehaviour
{

    public SpringJoint2D spring;
    public bool IsActive = true;

    void Awake()
    {

        spring = this.gameObject.GetComponent<SpringJoint2D>(); //"spring" is the SpringJoint2D component that I added to my object

        spring.connectedAnchor = gameObject.transform.position;//i want the anchor position to start at the object's position

    }


    void OnMouseDown()
    {
        if (IsActive == true)
        {
            spring.enabled = true;//I'm reactivating the SpringJoint2D component each time I'm clicking on my object becouse I'm disabling it after I'm releasing the mouse click so it will fly in the direction i was moving my mouse
        }
    }


    void OnMouseDrag()
    {

        if (spring.enabled == true)
        {

            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//getting cursor position

            spring.connectedAnchor = cursorPosition;//the anchor get's cursor's position


        }
    }


    void OnMouseUp()
    {
        spring.enabled = false;//disabling the spring component
        //IsActive = false;
    }

}

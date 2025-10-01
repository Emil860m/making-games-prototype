using UnityEngine;

public class WorldDrag : MonoBehaviour
{

    public Camera m_Camera;



    public Transform DragToWorld(string InteractionName)
    {
        Vector2 pos = Input.mousePosition;
        var worldRay = m_Camera.ScreenPointToRay(pos);

        RaycastHit hit;
        if (Physics.Raycast(worldRay, out hit))
        {
            if (hit.transform.name == InteractionName)
            {

                return hit.transform;
            }
        }
        return null;
    }
}

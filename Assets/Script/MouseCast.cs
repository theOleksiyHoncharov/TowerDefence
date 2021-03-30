using UnityEngine;
using UnityEngine.Events;

public class MouseCast : MonoBehaviour
{
    Camera camera;

    public event UnityAction<int> targetingTower;

    public void Initialize()
    {
        enabled = false;
        camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 300))
            {
                if (hit.transform.tag == "Tower")
                    targetingTower(hit.transform.gameObject.GetInstanceID());
            }
        }

    }

    public void Enable()
    {
        enabled = true;
    }
    public void Disable()
    {
        enabled = false;
    }
}

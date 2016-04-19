using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchReco : MonoBehaviour
{
	public LayerMask touchInputMask;

	private List<GameObject> touchList = new List<GameObject> ();
	private GameObject[] touchesOld = null;
	private RaycastHit hit;
	private bool isTouched = false;

	void Start ()
	{
		this.isTouched = false;
	}

	#if UNITY_EDITOR //if we're in editor without tactile

	// Update is called once per frame
	void Update ()
	{

		this.isTouched = false;

		if (Input.GetMouseButton (0) || Input.GetMouseButtonDown (0) || Input.GetMouseButtonUp (0)) {
			touchesOld = new GameObject[touchList.Count];

			touchList.CopyTo (touchesOld);
			touchList.Clear ();

			Ray ray = GetComponent<Camera> ().ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out hit, touchInputMask)) {
				GameObject recipient = hit.transform.gameObject;
				if (recipient.name != "TouchZone")
					return;
				touchList.Add (recipient);

				if (Input.GetMouseButtonDown (0))
				{
					this.isTouched = true;
					recipient.SendMessage ("onTouchBegan", hit.point, SendMessageOptions.DontRequireReceiver);
				}
				if (Input.GetMouseButtonUp (0))
				{
					this.isTouched = false;
					recipient.SendMessage ("onTouchEnded", hit.point, SendMessageOptions.DontRequireReceiver);
				}
				if (Input.GetMouseButton (0))
				{
					this.isTouched = true;
					recipient.SendMessage ("onTouchStationary", hit.point, SendMessageOptions.DontRequireReceiver);
				}
				if (Input.GetMouseButton (0) && (Input.acceleration.x + Input.acceleration.y) != 0.0)
				{
					this.isTouched = true;
					recipient.SendMessage ("onTouchMoved", hit.point, SendMessageOptions.DontRequireReceiver);
				}
			}
		}

		if (touchesOld != null)
			foreach (GameObject obj in touchesOld) {
				if (!touchList.Contains (obj))
				{
					this.isTouched = false;
					obj.SendMessage ("onTouchCanceled", hit.point, SendMessageOptions.DontRequireReceiver);
				}
			}

	}
	#else
	// Update is called once per frame
	void Update ()
	{
		this.isTouched = false;

		if (Input.touchCount > 0)
        {
            touchesOld = new GameObject[touchList.Count];

            touchList.CopyTo(touchesOld);
            touchList.Clear();

            foreach (Touch touch in Input.touches)
            {
                Ray ray = GetComponent<Camera>().ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit, touchInputMask))
                {
                    GameObject recipient = hit.transform.gameObject;
					if (recipient.name != "TouchZone")
						return;
                    touchList.Add(recipient);

                    if (touch.phase == TouchPhase.Began)
					{
						this.isTouched = true;
						recipient.SendMessage ("onTouchBegan", hit.point, SendMessageOptions.DontRequireReceiver);
					}
                    if (touch.phase == TouchPhase.Ended)
					{
						this.isTouched = false;
                        recipient.SendMessage("onTouchEnded", hit.point, SendMessageOptions.DontRequireReceiver);
					}
                    if (touch.phase == TouchPhase.Stationary)
					{
						this.isTouched = true;
                        recipient.SendMessage("onTouchStationary", hit.point, SendMessageOptions.DontRequireReceiver);
					}
                    if (touch.phase == TouchPhase.Moved)
					{
						this.isTouched = true;
                        recipient.SendMessage("onTouchMoved", hit.point, SendMessageOptions.DontRequireReceiver);
					}
                    if (touch.phase == TouchPhase.Canceled)
					{
						this.isTouched = false;
                        recipient.SendMessage("onTouchCanceled", hit.point, SendMessageOptions.DontRequireReceiver);
					}

                }
            }
			if (touchesOld != null)
	            foreach (GameObject obj in touchesOld)
	            {
					if (!touchList.Contains (obj))
					{
						this.isTouched = false;
						obj.SendMessage ("onTouchCanceled", hit.point, SendMessageOptions.DontRequireReceiver);
					}
        	    }
        }
	}
	#endif

	public bool IsTouched()
	{
		return this.isTouched;
	}
}

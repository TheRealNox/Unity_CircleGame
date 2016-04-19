using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
    public Color defaultColor;
    public Color touchedColor;
    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
		mat.color = defaultColor;
    }

    void onTouchBegan()
    {
        mat.color = touchedColor;
    }
    void onTouchEnded()
    {
        mat.color = defaultColor;
    }
    void onTouchStationary()
    {
        mat.color = touchedColor;
    }
    void onTouchMoved()
    {
		mat.color = touchedColor;
    }
	void onTouchCanceled()
    {
        mat.color = defaultColor;
    }
}

using UnityEngine;
using System.Collections;

public class IsSafe : MonoBehaviour
{
	public Color okColor;
	public Color koColor;
	private TextMesh text;
	public TouchReco safeArea;

	// Use this for initialization
	void Start ()
	{
		this.text = transform.GetComponent<TextMesh>();
		this.safeArea = FindObjectOfType<TouchReco> ();
		this.text.font.material.color = koColor;
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.text = transform.GetComponent<TextMesh>();

		if (safeArea == null)
		{
			this.text.font.material.color = koColor;
			this.text.text = "KO";
		}
		else
		{
			if (safeArea.IsTouched ())
			{
				this.text.font.material.color = okColor;
				this.text.text = "OK";
			}
			else
			{
				this.text.font.material.color = koColor;
				this.text.text = "KO";
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.Events;

public class SplineDecorator : MonoBehaviour {

	public BezierSpline spline;

	public int frequency;
	public float spawnDotsDelay = 0.06f;
	public bool lookForward;
	[Header("Audio")]
	[SerializeField] private AudioSource effectAS;
	[SerializeField] private AudioClip popAC;
	public Transform[] items;
	private Vector3 temporalScale;
	public UnityEvent dotsSpawned;
	

	private void OnEnable () 
	{
		temporalScale = transform.localScale;

		if (frequency <= 0 || items == null || items.Length == 0)
		{
			return;
		}
		float stepSize = frequency * items.Length;
		if (spline.Loop || stepSize == 1) 
		{
			stepSize = 1f / stepSize;
		}
		else 
		{
			stepSize = 1f / (stepSize - 1);
		}
		StartCoroutine(InstantiatePoints(stepSize));
	}
    private void Start()
    {
		//temporalScale = transform.localScale;
		GetComponent<RectTransform>().anchoredPosition3D = new Vector3(GetComponent<RectTransform>().anchoredPosition3D.x,
			GetComponent<RectTransform>().anchoredPosition3D.y, -5);
    }
   //public void GeneratePoints()
   //{
   //
   //}
    IEnumerator InstantiatePoints(float stepSize)
    {
		for (int p = 0, f = 0; f < frequency; f++)
		{
			yield return new WaitForSeconds(spawnDotsDelay);
			for (int i = 0; i < items.Length; i++, p++)
			{
				Transform item = Instantiate(items[i]) as Transform;
				Vector3 position = spline.GetPoint(p * stepSize);
				item.localScale = Vector3.zero;
				item.DOScale(temporalScale, 0.5f);				
				AudioManager.audioManager.PlayOneShotVoice(popAC);
				item.transform.localPosition = position;
				if (lookForward)
				{
					item.transform.LookAt(position + spline.GetDirection(p * stepSize));
				}
				item.transform.SetParent(transform);
			}
		}
		dotsSpawned?.Invoke();
	}
}
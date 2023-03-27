using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeatChangeDoor : MonoBehaviour
{
    public HeartbeatBehaviour hbb;

    public GameObject targetMarker;
    public GameObject heartMarker;
    public TextMeshPro targetText;
    public TextMeshPro rangeText;
    public Vector3[] markerMins;
    public Vector3[] markerMaxes;

    private bool inRange = false;
    private float target;
    public float targetBottom;
    public float targetTop;
    public float range;
    public float choiceTime;
    public float changeRate;
    public float lockLength;
    private float lockTimer = 0;
    private float timer = 10;
    private int dir;
    private string pl = "Player";

    // Start is called before the first frame update
    void Start()
    {
        hbb = GameObject.Find("HeartbeatController").GetComponent<HeartbeatBehaviour>();
        targetMarker = transform.GetChild(0).gameObject;
        targetText = targetMarker.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        targetText.text = "";
        rangeText = targetMarker.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>();
        rangeText.text = "+-" + range.ToString();
        heartMarker = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            timer += Time.deltaTime;

            if (timer >= choiceTime)
            {
                timer = 0;

                dir = Random.Range(-1, 2);
            }

            target += dir * changeRate * Time.deltaTime;
            targetText.text = ((int)target).ToString();

            targetMarker.transform.localPosition = Vector3.Lerp(markerMins[0], markerMaxes[0], target / 100);
            heartMarker.transform.localPosition = Vector3.Lerp(markerMins[1], markerMaxes[1], (float)hbb.heartRate / 100);

            if (hbb.heartRate >= target - range && hbb.heartRate <= target + range && lockTimer < lockLength)
            {
                lockTimer += Time.deltaTime;
            }
            else if (lockTimer >= lockLength)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(pl))
        {
            inRange = true;

            target = Random.Range(targetBottom, targetTop);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(pl))
        {
            inRange = false;

            lockTimer = 0;
            targetText.text = "";
        }
    }
}

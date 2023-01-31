using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StopTimer : MonoBehaviour
{
    public float timeStart;
    public TextMeshProUGUI textBox;

    private void Start()
    {
        textBox.text = timeStart.ToString("F2");
    }


    private void Update()
    {
        timeStart += Time.deltaTime;
        textBox.text = timeStart.ToString("F2");

        
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BillboardController : MonoBehaviour
{
    private enum BillboardSide
    {
        None = 0,
        Left = 1,
        Right = 2
    }

    [SerializeField]
    private BillboardSide side = BillboardSide.None;

    [SerializeField]
    private TextMeshProUGUI textBox;

    // Start is called before the first frame update
    void Start()
    {
        //Init list
        List<string> lines;

        //Grabs lines based on side
        switch (side)
        {
            case BillboardSide.Left:
                lines = Singleton.Instance.GetComponent<AllPairedTextHolder>().getFirstCharaLines();
                break;
            case BillboardSide.Right:
                lines = Singleton.Instance.GetComponent<AllPairedTextHolder>().getSecondCharaLines();
                break;
            case BillboardSide.None:
            default:
                Debug.LogError("BillboardController Error: Billboard side not set.");
                return;
        }

        string outputString = "";

        foreach (string sentence in lines)
        {
            outputString += ">" + sentence + "\n";
        }

        textBox.text = outputString;
    }
}

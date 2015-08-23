using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsScreen_Getter : MonoBehaviour {

    public enum Info {
        girlCrying,
        combos,
        doubt,
        timeBonus,
        total
    }

    public Info stat;

    void Awake() {
        switch (stat) {
            case Info.girlCrying:
                GetComponent<Text>().text = GameManager.instance.scoreCrying.ToString();
                break;
            case Info.combos:
                GetComponent<Text>().text = GameManager.instance.scoreCombo.ToString();
                break;
            case Info.doubt:
                GetComponent<Text>().text = GameManager.instance.scoreDoubt.ToString();
                break;
            case Info.timeBonus:
                GetComponent<Text>().text = GameManager.instance.scoreTime.ToString();
                break;
            case Info.total:
                GetComponent<Text>().text = (GameManager.instance.scoreTime 
                                             + GameManager.instance.scoreCombo 
                                             + GameManager.instance.scoreDoubt 
                                             + GameManager.instance.scoreTime).ToString();
                break;
        }
    }
}

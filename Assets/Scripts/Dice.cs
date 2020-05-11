using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dice: MonoBehaviour{

    public string type;
    public int maxRoll;
    public int minRoll;

    public string getType() {
        return type;
    }

    public void setType(string type) {
        this.type = type;
    }

    public int roll() {
        return Random.Range(minRoll, maxRoll+1);
    }

    private void upgradeMax() {
        maxRoll = maxRoll+1;
    }

    private void upgradeMin() {
        //TODO: sanity check this so min is never higher than max later
        minRoll = minRoll+1;
    }
}

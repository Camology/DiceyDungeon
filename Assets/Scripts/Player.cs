using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public string pName;

    public GameObject starterDice;
    private int level;
    [SerializeField] private int health;

    private int cash;

    private int armor;

    private List<GameObject> diceCollection = new List<GameObject>();

    private int poisonTurn;
    private int poisonAmount;

    public bool isDead = false;



    void Start() {
        this.health = 100;
        cash = Random.Range(1,100);
        starterDice = Instantiate(starterDice, Vector3.zero, Quaternion.identity);
        starterDice.transform.SetParent(transform, false);
        diceCollection.Add(starterDice);
    }

    public void setArmor(int val) => this.armor = val;

    public int getHealth() => this.health;

    public void setHealth(int val) {
        this.health = val;
        if (this.health <= 0) {
            this.isDead = true;
        }
    }

    public void damage(int val) => this.setHealth(this.health - val);

    public void poisonDamage() {
        damage(this.poisonAmount);
        this.poisonAmount--;
        this.poisonTurn--;
    }

    public void addDice(GameObject item) => diceCollection.Add(item);

    public int getCash() => cash;

    public void setCash(int val) => cash = val;

    public void setPoisonTurn(int val) => this.poisonTurn = val;
    public int getPoisonTurn() => this.poisonTurn;
    public void setPoisonAmount(int val) => this.poisonAmount = val;
    public int getPoisonAmount() => this.poisonAmount;

    public List<GameObject> getDiceCollection() => this.diceCollection;


}

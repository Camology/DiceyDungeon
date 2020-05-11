using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour {
    public GameObject player;
    public List<GameObject> enemies;

    bool playerTurn = false;

    
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");

        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
            enemies.Add(enemy);
        }
    }

    public void playersTurn() {
        if (player.GetComponent<Player>().getPoisonTurn() > 0) player.GetComponent<Player>().poisonDamage();
        //List<Dice> playersChoice = new List<Dice>();
        List<GameObject> playersChoice = player.GetComponent<Player>().getDiceCollection();
        Dictionary<string, int> turnDict = new Dictionary<string, int>();
        //TODO: prompt player for up to 3 dice per round to play
        foreach(GameObject dice in playersChoice) {
            turnDict.Add(dice.GetComponent<Dice>().getType(), dice.GetComponent<Dice>().roll());
        }
        processTurn(turnDict, player, enemies);
        playerTurn = false;
    }

    public Dictionary<string, int> rollMyDice(GameObject player) {
        Dictionary<string, int> turnDict = new Dictionary<string, int>();
        foreach(GameObject dice in player.GetComponent<Player>().getDiceCollection()) {
            turnDict.Add(dice.GetComponent<Dice>().getType(), dice.GetComponent<Dice>().roll());
        }
        return turnDict;
    }

    public void processTurn(Dictionary<string, int> turnDict, GameObject player, List<GameObject> enemies) {
        GameObject target;
        foreach (KeyValuePair<string, int> turn in turnDict) {
            switch(turn.Key) {
                case "Basic":
                    target = chooseTarget(enemies);
                    target.GetComponent<Player>().damage(turn.Value);
                    printAttack(player.GetComponent<Player>().pName, turn.Key, target.GetComponent<Player>().pName, turn.Value);
                    break;
                // case "Fire":
                //     if (turnDict.ContainsKey("Basic"))
                //     //mult basic damage to a character
                //     break;
                case "Armor":
                    player.GetComponent<Player>().setArmor(turn.Value);
                    break;
                case "Heal":
                    player.GetComponent<Player>().setHealth(player.GetComponent<Player>().getHealth() + turn.Value);
                    break;
                case "Electric":
                    foreach (GameObject enemy in enemies) {
                        enemy.GetComponent<Player>().damage(turn.Value);
                        printAttack(player.GetComponent<Player>().pName, turn.Key, enemy.GetComponent<Player>().pName, turn.Value);
                    }
                    break;
                case "Poison":
                    target = chooseTarget(enemies);
                    target.GetComponent<Player>().setPoisonAmount(target.GetComponent<Player>().getPoisonAmount() +1);
                    target.GetComponent<Player>().setPoisonTurn(target.GetComponent<Player>().getPoisonTurn() + turn.Value);
                    printAttack(player.GetComponent<Player>().pName, turn.Key, target.GetComponent<Player>().pName, turn.Value);
                    break;
                case "Chaos":
                    //Don't ask about this one yet
                    break;
                default: break;
            }
            
        }
    }

    public void printAttack(string player, string type, string target, int damage){
        Debug.Log(player + " hits with " + type + " for " + damage + " on " + target);
    }

    public GameObject chooseTarget(List<GameObject> targets) {
        if (playerTurn){
            //TODO: Actually choose target
            return targets[Random.Range(0,targets.Count)];
        }
        else
            return targets[0];
    }

    public void enemyTurn() {
        foreach (GameObject enemy in enemies) {
            if (enemy.GetComponent<Player>().getPoisonTurn() > 0) enemy.GetComponent<Player>().poisonDamage();
            Dictionary<string, int> enemyRoll = rollMyDice(enemy);
            List<GameObject> playerList = new List<GameObject>();
            foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
                playerList.Add(player);
            }
            processTurn(enemyRoll, enemy, playerList);
        }
        playerTurn = true;
    }

    // Update is called once per frame
    void Update() {
        if (player.GetComponent<Player>().isDead) {
            Debug.Log("Player loses!");
            
        }
        else if (enemies.Count < 0) {
            Debug.Log("Player wins!");
        }
        else {
            if (playerTurn) {
                playersTurn();
            }
            else {
                enemyTurn();
            }
        }
        
    }
}

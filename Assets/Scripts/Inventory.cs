using System.Collections;
using System.Collections.Generic;

public class Inventory {

    private List<Dice> diceCollection = new List<Dice>();
    private int cash;

    public List<Dice> getDice() => diceCollection;

    public void addDice(Dice item) => diceCollection.Add(item);

    public int getCash() => cash;

    public void setCash(int val) => cash = val;
}

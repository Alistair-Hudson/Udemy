package Generics;

public class Main {
    
    public static void main(String[] args) {
        FootballPlayer joe = new FootballPlayer("Joe");
        BaseballPlayer pat = new BaseballPlayer("Pat");
        RugbyPlayer lomu = new RugbyPlayer("Lomu");
        RugbyPlayer mccaw = new RugbyPlayer("McCaw");
        RugbyPlayer fiztgerald = new RugbyPlayer("Fiztgerald");

        Team<RugbyPlayer> ibix = new Team<RugbyPlayer>("Ibix");
        // ibix.addPlayer(joe);
        // ibix.addPlayer(pat);
        ibix.addPlayer(lomu);
        ibix.addPlayer(mccaw);
        ibix.addPlayer(fiztgerald);

        System.out.println(ibix.numPlayers());
    }
}

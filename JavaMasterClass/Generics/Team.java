package Generics;

import java.util.ArrayList;

public class Team<T extends Player> { //the extends protects against using any type
                                    // use extends A & B & C to have mutiple bounds
    
    private String name;
    int played = 0;
    int won = 0;
    int lost = 0;
    int tied = 0;

    private ArrayList<T> members = new ArrayList<T>();

    public Team(String name) {
        this.name = name;
    }

    public String getName() {
        return name;
    }

    public boolean addPlayer(T player) {
        if (members.contains(player)) {
            System.out.println(player.getName() + " is already a member");
            return false;
        }
        members.add(player);
        System.out.println(player.getName() + " picked for team " + this.getName());
        return true;
    }

    public int numPlayers() {
        return members.size();
    }

    public void matchResult(Team<T> opponent, int ourScore, int theirScore) {//the adding of T here protects against any team being used
        if (ourScore > theirScore) {
            ++won;
        }
        else if (ourScore < theirScore) {
            ++lost;
        }
        else {
            ++tied;
        }
        ++played;

        if (opponent != null) {
                opponent.matchResult(null, ourScore, theirScore);
        }
    }
    
    public int ranking() {
        return (won * 2) + tied;
    }
}

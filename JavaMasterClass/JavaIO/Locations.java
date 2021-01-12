//package JavaIO;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.EOFException;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.util.Collection;
import java.util.HashMap;
import java.util.LinkedHashMap;
import java.util.Map;
import java.util.Scanner;
import java.util.Set;

public class Locations implements Map<Integer, Location> {

    private static Map<Integer, Location> locations = new LinkedHashMap<Integer, Location>();

    public static void main(String[] args) throws IOException {
        // Using File Writer
        // FileWriter locFile = null;
        // try{
        // locFile = new FileWriter("locations.txt");
        // for (Location location : locations.values()) {
        // locFile.write(location.getLocationID() + "," + location.getDescription() +
        // "\n");
        // }
        // } finally {
        // if (null != locFile){
        // locFile.close();
        // }
        // }
        
        // Using FileWriter and try resource
        // try (BufferedWriter locFile = new BufferedWriter(new FileWriter("locations.txt"));
        //         BufferedWriter dirFile = new BufferedWriter(new FileWriter("directions.txt"))) {
        //     for (Location location : locations.values()) {
        //         locFile.write((location.getLocationID() + "," + location.getDescription() + "\n"));
        //         for (String direction : location.getExits().keySet()) {
        //             if (!direction.equals("Q")){
        //                 dirFile.write(location.getLocationID() + "," + direction + "," + location.getExits().get(direction)
        //                                 + "\n");
        //             }
        //         }
        //     }
        // }

        // Using Byte Stream
        // try (DataOutputStream locFile = new DataOutputStream(new BufferedOutputStream((new FileOutputStream("location.dat"))))){
        //     for (Location location : locations.values()) {
        //         locFile.writeInt(location.getLocationID());
        //         locFile.writeUTF(location.getDescription());
        //         locFile.writeInt((location.getExits().size()-1));
        //         for (String direction : location.getExits().keySet()){
        //             if(!direction.equals("Q")){
        //                 locFile.writeUTF(direction);
        //                 locFile.writeInt(location.getExits().get(direction));
        //             }
        //         }
        //     }
        // }

        // Using Object Serialization
        // try (ObjectOutputStream locFile = new ObjectOutputStream(new BufferedOutputStream((new FileOutputStream("location.dat"))))){
        //     for (Location location : locations.values()){
        //         locFile.writeObject(location);
        //     }
        // }
    }

    static {
        // Using FileReader
        // try (Scanner scanner = new Scanner(new BufferedReader(new FileReader("locations_big.txt")))){
        //     scanner.useDelimiter(",");
        //     while(scanner.hasNext()) {
        //         int loc = scanner.nextInt();
        //         scanner.skip(scanner.delimiter());
        //         String description = scanner.nextLine();
        //         System.out.println("Imported loc: " + loc + ": " + description);
        //         Map<String, Integer> tempExit = new HashMap<String, Integer>();
        //         locations.put(loc, new Location(loc, description, tempExit));
        //     }
        // }catch (FileNotFoundException e){
        // }
     
        // try (Scanner scanner = new Scanner(new BufferedReader((new FileReader("directions_big.txt"))))) {  
        //     scanner.useDelimiter(",");
        //     while (scanner.hasNext()) {
        //         String input = scanner.nextLine();
        //         String[] data = input.split(",");
        //         int loc = Integer.parseInt(data[0]);
        //         String direction = data[1];
        //         int dest = Integer.parseInt(data[2]);
        //         Location location = locations.get(loc);
        //         location.addExit(direction, dest);
        //     }
        // }catch(FileNotFoundException e) {
        // }

        //Using DataStream
        // try (DataInputStream locFile = new DataInputStream(new BufferedInputStream(new FileInputStream("location.dat")))){
        //     boolean eof = false;
        //     while(!eof) {
        //         try{
        //             Map<String, Integer> exits = new LinkedHashMap<>();
        //             int locID = locFile.readInt();
        //             String desc = locFile.readUTF();
        //             int numExits = locFile.readInt();
        //             for ( int i=0; i<numExits; ++i){
        //                 String dir = locFile.readUTF();
        //                 int dest = locFile.readInt();
        //                 exits.put(dir, dest);
        //             }
        //             locations.put(locID, new Location(locID, desc, exits));
        //         }catch(EOFException e){
        //             eof = true;
        //         }

        //     }
        // }catch (IOException e){

        // }

        // Using Object Serialization
        try (ObjectInputStream locFile = new ObjectInputStream(new BufferedInputStream(new FileInputStream("location.dat")))){
            boolean eof = false;
            while (!eof){
                try{
                    Location location = (Location) locFile.readObject();
                    locations.put(location.getLocationID(), location);
                }catch (EOFException e){
                    eof = true;
                }
            }
        } catch (IOException ioe){

        } catch (ClassNotFoundException ce){

        }

    }

    @Override
    public int size() {
        return locations.size();
    }

    @Override
    public boolean isEmpty() {
        return locations.isEmpty();
    }

    @Override
    public boolean containsKey(Object key) {
        return locations.containsKey(key);
    }

    @Override
    public boolean containsValue(Object value) {
        return locations.containsValue(value);
    }

    @Override
    public Location get(Object key) {
        return locations.get(key);
    }

    @Override
    public Location put(Integer key, Location value) {
        return locations.put(key, value);
    }

    @Override
    public Location remove(Object key) {
        return locations.remove(key);
    }

    @Override
    public void putAll(Map<? extends Integer, ? extends Location> m) {
        locations.putAll(m);
    }

    @Override
    public void clear() {
        locations.clear();
    }

    @Override
    public Set<Integer> keySet() {
       return locations.keySet();
    }

    @Override
    public Collection<Location> values() {
        return locations.values();
    }

    @Override
    public Set<Entry<Integer, Location>> entrySet() {
        return locations.entrySet();
    }


}

package Basics;

public class ParsingString {

    public static void main(String[] args) {
        
        String numAsString = "2018";

        int num = Integer.parseInt(numAsString);
        double d = Double.parseDouble(numAsString);

        numAsString += 1;
        num += 1;

        System.out.println("numAsString = " + numAsString);
        System.out.println("num = " + num);

    }
    
}

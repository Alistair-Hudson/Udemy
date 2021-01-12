package Exceptions;

import java.util.NoSuchElementException;

public class Exceptions {
    
    public static void main(String[] args) {
     
        int x = 98;
        int y = 0;
        System.out.println(divideLBYL(x, y));
        try {
            System.out.println(divideEAFP(x, y));
        } catch (ArithmeticException | NoSuchElementException e) {
            System.out.println(e);
        }

    }

    private static int divideLBYL(int x, int y) {
        if (0 != y) {
            return x/y;
        }
        return 0;
    }

    private static int divideEAFP(int x, int y) {
        try {
            return x/y;
        }
        catch(ArithmeticException e){
            throw new ArithmeticException("Attempt to divide by 0");
        }
    }

}

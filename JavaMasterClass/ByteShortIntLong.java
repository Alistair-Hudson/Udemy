//package academy.learnprogramming;

public class ByteShortIntLong {
    
    public static void main(String[] args) {
        
        int myValue = 10000;

        int minInt = Integer.MIN_VALUE;
        int maxInt = Integer.MAX_VALUE;
        System.out.println("MIN int = " + minInt);
        System.out.println("MAX int = " + maxInt);
        System.out.println("Busted MAX= " + (maxInt + 1));

        byte minByte = Byte.MIN_VALUE;
        byte maxByte = Byte.MAX_VALUE;
        System.out.println("MIN byte = " + minByte);
        System.out.println("MAX byte = " + maxByte);

        short minShort = Short.MIN_VALUE;
        short maxShort = Short.MAX_VALUE;
        System.out.println("MIN short = " + minShort);
        System.out.println("MAX short = " + maxShort);

        long minLong = Long.MIN_VALUE;
        long maxLong = Long.MAX_VALUE;
        System.out.println("MIN long = " + minLong);
        System.out.println("MAX long = " + maxLong);

        //Casting
        byte newByte = (byte)(maxByte/2);
        
    }
}

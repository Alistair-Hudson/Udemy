import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

import java.util.Scanner;

public class UserInput {
    
    public static void main(String[] args) {
        
        Scanner scanner = new Scanner(System.in);
       
        System.out.println("Enter Name: ");
        String name = scanner.nextLine();
        System.out.println("Hello " + name);

        System.out.println("What is your birthyear: ");
        boolean hasNextInt = scanner.hasNextInt();
        if (hasNextInt){

            int birthYear = scanner.nextInt(); //Note that negfative numbers can be input
            scanner.nextLine();//This is so that you can use nextLine() to retrieve strings
            System.out.println("So you are " + (2020 - birthYear));

        }
        else{
            System.err.println("Invaild Year of Birth");
        }


        scanner.close();

        

    }
}

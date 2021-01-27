package EchoClient;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.PrintWriter;
import java.net.Socket;
import java.net.SocketException;
import java.net.SocketTimeoutException;
import java.util.Scanner;
import java.io.InputStreamReader;

public class Main {
    
    public static void main(String[] args) {
        
        try(Socket socket = new Socket("localhost", 5000)){
            socket.setSoTimeout(5000);
            BufferedReader echoes = new BufferedReader(
                                        new InputStreamReader(socket.getInputStream()));
            PrintWriter stringecho = new PrintWriter(socket.getOutputStream(), true);

            Scanner scanner = new Scanner(System.in);
            String echoString;
            String response;

            do{
                System.out.println("Enter a sttring: ");
                echoString = scanner.nextLine();
                stringecho.println(echoString);
                if(!echoString.equals("exit")){
                    response = echoes.readLine();
                    System.out.println(response);
                }
            }while(!echoString.equals("exit"));

        }catch(SocketTimeoutException e){
            System.out.println("Socket timedout");
        }catch (IOException e) {
            e.getStackTrace();
        }
    }
}

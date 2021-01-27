package EchoServer;

import java.io.BufferedReader;
import java.net.Socket;
import java.io.InputStreamReader;
import java.io.PrintWriter;

public class Echoer extends Thread {

    private Socket socket;

    public Echoer(Socket socket){
        this.socket = socket;
    }
    
    @Override
    public void run(){
        try {
            BufferedReader input = new BufferedReader(
                                        new InputStreamReader(socket.getInputStream()));
            PrintWriter output = new PrintWriter(socket.getOutputStream());

            while(true){
                String echoString = input.readLine();
                System.out.println("Recieved Cleint input: " + echoString);
                if(echoString.equals("exit")){
                    break;
                }
                // try {
                //     Thread.sleep(5000);
                // } catch (Exception e) {
                //     System.out.println("Thread interrupted");
                // }

                output.println(echoString);
            }
            
        } catch (Exception e) {
            System.out.println("Oops" + e.getMessage());
        }finally{
            try{
                socket.close();
            }catch(Exception e){

            }
        }
    }
}

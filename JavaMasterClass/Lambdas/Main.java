
public class Main {

    public static void main(String[] args) {
        new Thread(()-> {
                            System.out.println("this is from a runnnable");
                            System.out.println("another line");
                            System.out.println("and another line");
                        }).start();
    }
}
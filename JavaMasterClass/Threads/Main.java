public class Main {

    public static void main(String[] args) {
        
        concurrency();
    }

    private static void concurrency() {
        Countdown countdown = new Countdown();

        RunCountdown t1 = new RunCountdown(countdown);
        RunCountdown t2 = new RunCountdown(countdown);

        t1.run();
        t2.run();

    }

    private static void basicThread() {
        System.out.println("This is in the main thread");
        
        Thread anotherThread = new AnotherThread();
        anotherThread.start();

        Thread myRunThread = new Thread(new MyRunnable());
        myRunThread.start();

        //anotherThread.interrupt();//for interrupting

        //For joining
        try{
            anotherThread.join();
        }catch (InterruptedException e){
            System.out.println("Interrupted before I could join");
        }

        System.out.println("from the main again");
    }
}

class RunCountdown extends Thread {

    Countdown countdown;

    public RunCountdown(Countdown countdown){
        this.countdown = countdown;
    }

    @Override
    public void run() {
        countdown.countdown();
    }
}
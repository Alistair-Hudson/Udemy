
public class Countdown {

    public void countdown(){
        
        int i;

        synchronized(this){
            for(i=10; i>0; --i){
                System.out.println(i);
            }
        }
    }

}

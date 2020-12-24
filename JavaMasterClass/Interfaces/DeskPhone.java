package Interfaces;

public class DeskPhone implements ITelephone{

    private int myNumber;
    private boolean isRinging;

    @Override
    public void powerOn(){
        System.out.println("Desk phone has no power button");
    }

    @Override
    public void dial(int phoneNumber) {
        
        
    }

    @Override
    public void answer(){
        isRinging = false;
    }

    @Override
    public boolean callPhone(int phoneNumber) {
        if (phoneNumber == myNumber)
        {
            isRinging = true;
        }
        else
        {
            isRinging = false;
        }
        return isRinging;
    }

    @Override
    public boolean isRinging() {
        return isRinging;
    }
    
}

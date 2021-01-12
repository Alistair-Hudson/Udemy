
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.nio.ByteBuffer;
import java.nio.channels.FileChannel;
import java.nio.channels.Pipe;
import java.nio.file.FileSystems;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.StandardOpenOption;
import java.util.List;

public class Main{

    public static void main(String[] args) {
        
        
        usingPipes();
        
    }
    
    
    private static void usingPipes() {

        try{
            Pipe pipe = Pipe.open();

            Runnable writer = new Runnable() {
                @Override
                public void run(){
                    try {
                        Pipe.SinkChannel sinkChannel = pipe.sink();
                        ByteBuffer buffer = ByteBuffer.allocate(100);

                        for (int i = 0; i < 10; ++i){
                            String currentTime = "Time is " + System.currentTimeMillis();

                            buffer.put(currentTime.getBytes());
                            buffer.flip();

                            while (buffer.hasRemaining()){
                                sinkChannel.write(buffer);
                            }
                            buffer.flip();
                            Thread.sleep(100);
                        }
                    }catch (Exception e){
                        e.getStackTrace();
                    }
                }
            };

            Runnable reader = new Runnable(){
                @Override
                public void run(){
                    try{

                        Pipe.SourceChannel sourceChannel = pipe.source();
                        ByteBuffer buffer = ByteBuffer.allocate(100);

                        for (int i = 0; i<10; ++i){
                            int bytesRead = sourceChannel.read(buffer);
                            byte[] timeString = new byte[bytesRead];
                            buffer.flip();
                            buffer.get(timeString);
                            System.out.println("Reading " + new String(timeString));
                            buffer.flip();
                            Thread.sleep(100);
                        }

                    }catch(Exception e){
                        e.getStackTrace();
                    }
                }
            };

            new Thread(writer).start();
            new Thread(reader).start();

        }catch (IOException e){
            e.getStackTrace();
        }
    }

    private static void copyFile2File() {
        
        try{
            RandomAccessFile ra = new RandomAccessFile("data.dat", "rwd"); 
            FileChannel mainChannel = ra.getChannel();
            RandomAccessFile copyFile = new RandomAccessFile("datacopy.dat", "rwd");
            FileChannel copyChannel = copyFile.getChannel();
            long numTransfered = copyChannel.transferFrom(mainChannel, 0, mainChannel.size()); //note that position is releative not abosolute
            //long numTransfered = mainChannel.transferTo(0, mainChannel.size(), copyChannel);

            System.out.println(numTransfered + " Bytes transfered");

            ra.close();
            copyFile.close();
            copyChannel.close();

        }catch(IOException e){
            e.getStackTrace();
        }
    }

    private static void writeSequentially() {

        try{
            RandomAccessFile ra = new RandomAccessFile("data.dat", "rwd");
            FileChannel channel = ra.getChannel();
            byte[] outStr = "Hello World!".getBytes();
            long strPos = 0;
            long int1Pos = outStr.length;
            long int2Pos = int1Pos + Integer.BYTES;

            ByteBuffer intBuffer = ByteBuffer.allocate(Integer.BYTES);
            intBuffer.putInt(245);
            intBuffer.flip();
            channel.position(int1Pos);
            channel.write(intBuffer);

            intBuffer.flip();
            intBuffer.putInt(-98765);
            intBuffer.flip();
            channel.position(int2Pos);
            channel.write(intBuffer);

            channel.position(strPos);
            channel.write(ByteBuffer.wrap(outStr));

            ra.close();

        }catch(IOException e){
            e.getStackTrace();
        }
    }

    private static void readSequentially() {
    
        try {

            ByteBuffer buffer = ByteBuffer.allocate(Integer.BYTES);
            RandomAccessFile ra = new RandomAccessFile("data.dat", "rwd");
            FileChannel channel = ra.getChannel();

            channel.position(19 - Integer.BYTES);
            channel.read(buffer);
            buffer.flip();
            System.out.println(buffer.getInt());

            buffer.flip();
            channel.position(19 -(Integer.BYTES*2));
            channel.read(buffer);
            buffer.flip();
            System.out.println(buffer.getInt());

            ra.close();

        } catch (IOException e)
        {
            e.getStackTrace();
        }

    }

    private static void readFromFileBinary() {
        try{
            
            ByteBuffer buffer = ByteBuffer.allocate(100);
            
            RandomAccessFile ra = new RandomAccessFile("data.dat", "rwd");
            FileChannel channel = ra.getChannel();
            long numBytes = channel.read(buffer);
            byte[] inputString = new byte[11];

            buffer.flip();
            buffer.get(inputString);
            System.out.println(numBytes + " read; " + new String(inputString));
            System.out.println(numBytes + " read; " + buffer.getInt());
            System.out.println(numBytes + " read; " + buffer.getInt());

            ra.close();

        }catch(IOException e){
            e.getStackTrace();
        }
    }

    private static void writeToFileBinary() {
        try (FileOutputStream binFile = new FileOutputStream("data.dat");
        FileChannel binChannel = binFile.getChannel()){
            
            ByteBuffer buffer = ByteBuffer.allocate(100);
            byte[] outputBytes = "Hello World".getBytes();
            buffer.put(outputBytes);
            buffer.putInt(245);
            buffer.putInt(-98765);
            int numBytes = binChannel.write(buffer.flip());
            System.out.println(numBytes + " written");            

        }catch(IOException e){
            e.printStackTrace();
        }
        
    }

    private static void readWriteFileStandard() {
        try{
            // FileInputStream file = new FileInputStream("data.txt");
            // FileChannel channel = file.getChannel();

            Path dataPath = FileSystems.getDefault().getPath("data.txt");
            Files.write(dataPath, "\nLine 6".getBytes(), StandardOpenOption.APPEND);
            List<String> lines = Files.readAllLines(dataPath);
            for(String line : lines){
                System.out.println(line);
            }

        }catch(IOException e){
            e.printStackTrace();
        }
    }
}
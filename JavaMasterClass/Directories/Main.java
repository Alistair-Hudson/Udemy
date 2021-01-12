import java.io.File;
import java.io.IOException;
import java.nio.file.DirectoryStream;
import java.nio.file.FileStore;
import java.nio.file.FileSystems;
import java.nio.file.Files;
import java.nio.file.Path;

public class Main{

    public static void main(String[] args) {

        DirectoryStream.Filter<Path> filter = 
                    new DirectoryStream.Filter<Path>(){
                        public boolean accept(Path path) throws IOException{
                            return Files.isRegularFile(path);
                        }
                    };
        
        //Lambda expression
        //DirectoryStream.Filter<Path> filter = p -> Files.isRegularFile(p);
        
        //Path directory = FileSystems.getDefault().getPath("FileTree\\dir1"); 
        Path directory = FileSystems.getDefault().getPath("FileTree" + File.separator + "dir1"); 
        try(DirectoryStream<Path> contents = Files.newDirectoryStream(directory, filter)){

            for (Path file: contents){
                System.out.println(file.getFileName());
            }

        }catch (Exception e){
            e.getStackTrace();
        }

        String separator = File.separator;
        System.out.println(separator);
        separator = FileSystems.getDefault().getSeparator();
        System.out.println(separator);

        try{
            Path tempFile = Files.createTempFile("myapp", ".appext");
            System.out.println(tempFile.toAbsolutePath());

        }catch(Exception e){
            e.getStackTrace();
        }

        Iterable<FileStore> stores = FileSystems.getDefault().getFileStores();
        for(FileStore store : stores){
            System.out.println(store +" "+store.name());
        }
    }


}
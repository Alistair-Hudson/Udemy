import java.io.BufferedReader;
import java.io.IOException;
import java.nio.file.FileSystems;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.nio.file.StandardCopyOption;



public class Main {
    
    public static void main(String[] args) {
        
        //Path path = FileSystems.getDefault().getPath("files", "subdir.txt");
        //Path path = Paths.get("C:\\...");
        //Path path = Paths.get(".\\files\\subdir.txt");
        deleteFiles();
    }

    private static void deleteFiles() {
        try{

            Path file2Delete = FileSystems.getDefault().getPath("files", "wrkdir.txt");

            Files.deleteIfExists(file2Delete);//inorder to delte directories make sure they are empty

        }catch(Exception e){
            e.getStackTrace();
        }
    }

    private static void renameFiles() {
        try{

            Path file2Move = FileSystems.getDefault().getPath("files", "wrkdir_copy.txt");
            Path dest = FileSystems.getDefault().getPath("files", "wrkdir.txt");

            Files.move(file2Move, dest, StandardCopyOption.REPLACE_EXISTING);//only use the third part if you intend to replace

        }catch(Exception e){
            e.getStackTrace();
        }
    }

    private static void moveFiles() {
        try{

            Path file2Move = FileSystems.getDefault().getPath("wrkdir_copy.txt");
            Path dest = FileSystems.getDefault().getPath("files", "wrkdir_copy.txt");

            Files.move(file2Move, dest, StandardCopyOption.REPLACE_EXISTING);//only use the third part if you intend to replace

        }catch(Exception e){
            e.getStackTrace();
        }
    }

    private static void copyFiles() {//this can be used to copy directories but won't copy files in it

        try{

            Path source = FileSystems.getDefault().getPath("wrkdir.txt");
            Path dest = FileSystems.getDefault().getPath("wrkdir_copy.txt");

            Files.copy(source, dest, StandardCopyOption.REPLACE_EXISTING);//only use the third part if you intend to replace

        }catch(Exception e){
            e.getStackTrace();
        }
    }

    private static void usingPaths(Path path) {

        try (BufferedReader fileReader = Files.newBufferedReader(path)){
            String line;
            while((line = fileReader.readLine()) != null){
                System.out.println(line);
            }
        }catch (IOException e){
            e.getStackTrace();
        }
    }

    

}
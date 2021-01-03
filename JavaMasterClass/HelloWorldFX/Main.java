import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;

public class Main extends Application {

    @Override
    public void start(Stage primaryStage) throws Exception{
        //With FMXLLoader
        Parent root = FXMLLoader.load(getClass().getResource("sample.fxml"));

        //Without FXMLLoader
        // GridPane root = new GridPane();
        // root.setAlignment(Pos.CENTER);
        // root.setVgap(10);
        // root.setHgap(10);

        // Label greeting = new Label("welcome to JavaFX");
        // greeting.setTextFill(Color.GREEN);
        // greeting.setFont(Font.font("Times New Roman"), FontWeight.BOLD, 70);
        // root.getChildren().add(greeting);


        primaryStage.setTitle("Hello World");
        primaryStage.setScene(new Scene(root, 700, 275));
        primaryStage.show();
    }

    public static void main(String[] args) {
        launch(args);
    }
}

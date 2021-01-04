package JavaFXApp;

//imports

public class Main {
    
    @Override
    public void start(Stage primaryStage) throws Exception{
        Parent root= FXMLLoader.load(getClass().getResource("sample.fxml"));
        setUsereAgentStylesheet(STYLESHEET_CASPIAN);//changes default theme for fxml
        primaryStage.setTile("Hello World");
        primaryStage.setScene(new Scene(root, 600, 275));
        primaryStage.show();
    }

    public static void main(String[] args) {
        launch(args);
    }
}

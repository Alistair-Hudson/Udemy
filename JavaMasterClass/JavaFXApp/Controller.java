package JavaFXApp;

public class Controller {
    
    @FXML
    private Label label;
    @FXML
    private Button button4;
    @FXML
    private GridPane gridPane;

    public void initialize() {
        button4.setEffect(new DropShadow());
    }

    @FXML
    public void handleMouseEnter(){
        label.setScaleX(2.0);
        label.setScaleY(2.0);
    }

    @FXML
    public void handleMouseExit(){
        label.setScaleX(1.0);
        label.setScaleY(1.0);
    }

    @FXML
    public void handleOnClick() {
        FileChooser chooser = new FileChooser();
        chooser.setTitle("Svae File");
        chooser.getExtensions().addAll(
            new FileChooser.ExtensionFilter("Text", "*.txt")
        );
        //File file = chooser.showOpenDialog(gridPane.getScene().getWindow());
        File file = chooser.showSaveDialog(gridPane.getScene().getWindow());

        // DirectoryChooser chooser = new DirectoryChooser();
        // File file = chooser.showDialog(gridPane.getScene().getWindow());
        if (null != file) {
            System.out.println(file.getPath());
        } else {
            System.out.println("Chooser was canceled");
        }

        
    }
}

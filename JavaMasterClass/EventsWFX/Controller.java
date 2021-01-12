// package EventsWFX;

// import javafx.fxml.FXML;
// import javafx.event.ActionEvent;
// import javafx.scene.control.TextField;
// import javafx.scene.control.Button;
// import javafx.scene.control.CheckBox;

// public class Controller {
    
//     @FXML
//     private TextFeild nameFeild;
//     @FXML
//     private Button helloButton;
//     @FXML
//     private Button byeButton;
//     @FXML
//     private CheckBox ourCheckBox;

//     @FXML
//     public void initialize() {
//         helloButton.setDisable(true);
//         byeButton.setDisable(true);
//     }

//     @FXML
//     public void onButtonClicked(ActionEvent e) {
//         if(e.getSource().equals(helloButton)){
//             System.out.println("Hello, " + nameFeild.getText());
//         }
//         else if (e.getSource().equals(byeButton)) {
//             System.out.println("Bye, " + nameFeild.getText());
//         }
//         if(ourCheckBox.isSelected())
//         {
//             nameFeild.clear();
//             initialize();
//         }
//     }

//     @FXML
//     public void handleKeyRelease(){
//         String text = nameFeild.getText();
//         boolean disableButtons = text.isEmpty() || text.trim().isEmpty();
//         helloButton.setDisable(disableButtons);
//         byeButton.setDisable(disableButtons);
//     }

//     @FXML
//     public void handleChange(){
//         System.out.println("Checkbox is " + (ourCheckBox.isSelected() ? "Checked" : "Not Checked"));
//     }
// }

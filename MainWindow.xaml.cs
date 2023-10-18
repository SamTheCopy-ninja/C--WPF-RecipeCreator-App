using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_PersonalRecipeCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Generic List collection to store recipes, ingredients and steps
        public List<recipeDetails> ingredientsList = new List<recipeDetails>();

        //Dictionary to store Key-Value pairs for the calories of each ingredient and the food group
        public Dictionary<double, string> caloriesAndFoodGroup = new Dictionary<double, string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            display.Content = "";

            foreach (recipeDetails recipe in ingredientsList) //foreach loop to iterate through recipe details and display a recipe selected by the user
            {
                // Check if the selected recipe name matches the current recipe in the loop
                

                if (list.SelectedItem != null && recipe.recipeName == list.SelectedItem.ToString())
                {
                    // Display the recipe information
                    string recipeInfo =
                                        $"Ingredient: {recipe.itemName}\n" +
                                        $"Measurement: {recipe.itemMeasurement}\n" +
                                        $"Quantity: {recipe.itemQuantity}\n";

                    display.Content += $"{recipeInfo} \nSteps to prepare recipe:\n{recipe.recipeDescription}\n"; //Output recipe info and recipe steps

                    
                }

            }
            
        }


        //Delegate used to notify user when calories exceed 300
        public delegate void CalorieCountDelegate(string noticeMsg);

        public void showCalorieCount(string mesage)
        {
            MessageBox.Show(mesage); //Displays pop-up message based on how many calories are in the recipe
        }
        public void addCalories(CalorieCountDelegate calorieCount) //public method for adding calories for each ingredient
        {

            foreach (recipeDetails ingredient in ingredientsList)
            {
                //A while loop to capture calories and link them to specific ingredients
                int count = 0;
                var i = ingredient.itemName; //To count ingredients starting from number 1
                while (true)
                {
                    //Ask the user for calories
                    string cal = $"Enter CALORIES for {i} \n\n(press Enter to CONFIRM AND SAVE \nor CLICK 'Cancel' if your are finished):";

                    //Pop-up dialog box to enter calories
                    string recipeInfo = Microsoft.VisualBasic.Interaction.InputBox(cal, "Calories");

                    //Check if user has entered calories, or pressed enter to save info to Dictionary
                    if (string.IsNullOrEmpty(recipeInfo))
                    {
                        break;
                    }

                    //Ask the user for food group, for each ingredient
                    string food = $"Enter FOOD GROUP for {i} \n\n(press Enter to CONFIRM AND SAVE \nor CLICK 'Cancel' if your are finished):";

                    //Pop-up dialog box to enter each food group and link it to a specific ingredient
                    string group = Microsoft.VisualBasic.Interaction.InputBox(food, "Calories");

                    //Check if user has entered food groups, or pressed enter to save info to Dictionary
                    if (string.IsNullOrEmpty(group))
                    {
                        break;
                    }

                    //Add calories and food groups to Dictionary
                    try
                    {
                        caloriesAndFoodGroup.Add(double.Parse(recipeInfo), group);
                    }
                    catch (Exception ex)
                    {

                    }


                    //Count each entry
                    count++;


                    foreach (recipeDetails ing in ingredientsList) //foreach loop to iterate through List and link calories and food groups (from Dictionary) to ingredients
                    {
                        if (caloriesAndFoodGroup.TryGetValue(double.Parse(recipeInfo), out group))
                        {
                            ingredient.itemName = $"{i}  \nFood Group: {group} \nCalories: {recipeInfo}"; //Saves ingredient name, calories, and food groups as one entry

                        }
                    }

                    int t = 1;
                    //Initialize a few variables
                    string calc = "";
                    double total = 0;

                    foreach (KeyValuePair<double, string> pair in caloriesAndFoodGroup) //foreach loop to output calories and food groups
                    {
                        //Output formatting for displaying the user selected recipe
                        calc += $"\nCalories for ingredient {t} - {pair.Key} \nFood Group - {pair.Value}\n";
                        t++;

                        total += pair.Key; //Adding total calories for each recipe, using Dictionary keys

                    }

                }

            }

            double totalCalories = 0; //variable to check calorie totals

            foreach (double key in caloriesAndFoodGroup.Keys) //foreach loop to check for calories of each ingredient
            {
                totalCalories += key; //Sum of keys in the Dictionary
            }

            if (totalCalories >= 300) //if statement to notify user (with help from the Delegate) if their recipe calories exceed 300
            {
                showCalorieCount($"Recipe has exceeded 300 calories.\nCalorie Total = {totalCalories}");
            }
            else if (totalCalories < 300) //if statement to notify user (with help from the Delegate) if their recipe calories are less than 300
            {
                showCalorieCount($"Calorie Total For Your Recipe: {totalCalories}");
            }
        }

        private void foodGroup_Click(object sender, RoutedEventArgs e)
        {
            addCalories(showCalorieCount); //addCalories method with associated details
        }

        private void confirmationMethod()
        {
            //Method to confirm if array lists contain data

            //Boolean values to check if generic collection contains no elements
            bool empty = ingredientsList.Count == 0;


            if (empty) //If-statement to check if generic collection is empty, returns error message if List contains zero elements
            {
                //Error message if Lists is empty
                display.Content = "No recipe information," +
                    "\n please enter ingredients and steps";
            }
            else
            {
                display.Content = "Select a recipe to be scaled\nfrom the Recipe List"; //Default display message

            }
        }

        private void clearTextBoxes()
        {
            //Method to clear all textboxes

            ingredient.Clear();
            measure.Clear();
            qty.Clear();
            rname.Clear();
            textb.Document = new FlowDocument();
        }
        private void clearEverything()
        {
            //Method to clear all data and reset
            ingredient.Clear();
            measure.Clear();
            qty.Clear();
            rname.Clear();
            textb.Document = new FlowDocument();


            ingredientsList.Clear(); //Clear recipe info

            list.SelectedItem = null;
            list.ItemsSource = null;
            list.Items.Clear(); //Remove the recipes displayed in listBox

            display.Content = "Select A Recipe From Your List"; //Default display message


        }
        private void radio1_Checked(object sender, RoutedEventArgs e)
        {
            //Radio button to scale recipe by 0.5

            //Boolean values to check if generic List contains no elements
            bool empty = ingredientsList.Count == 0;

            if (radio1.IsChecked == true) //If-statement when option is selected
            {
                foreach (recipeDetails increase in ingredientsList) //Foreach loop to scale ingredient quantity
                {
                    increase.recipeScaling(0.5);
                }

                confirmationMethod(); //Method called to check if generic list contains data
                radio1.IsChecked = false; //Undo radio button check
            }
        }

        private void radio2_Checked(object sender, RoutedEventArgs e)
        {
            //Radio button to double the recipe
            bool empty = ingredientsList.Count == 0;

            if (radio2.IsChecked == true) //If-statement when option is selected
            {
                foreach (recipeDetails increase in ingredientsList) //Foreach loop to scale ingredient quantity
                {
                    increase.recipeScaling(2);
                }

                confirmationMethod(); //Method called to check if List generic contains data
                radio2.IsChecked = false; //Undo radio button check
            }
        }

        private void radio3_Checked(object sender, RoutedEventArgs e)
        {
            //Radio button to triple the recipe

            bool empty = ingredientsList.Count == 0;

            if (radio3.IsChecked == true) //If-statement when option is selected
            {
                foreach (recipeDetails increase in ingredientsList) //Foreach loop to scale ingredient quantity
                {
                    increase.recipeScaling(3);
                }

                confirmationMethod(); //Method called to check if List generic contains data
                radio3.IsChecked = false; //Undo radio button check
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Button to reset each quantity

            foreach (recipeDetails reset in ingredientsList) //Foreach loop to revert ingredient quantity to orginal value
            {
                reset.resetRecipe(); //Method called to reset each quantity
            }
            confirmationMethod(); //Method to confirm if generic List contains data

            //Un-check each radio button after reverting quantity
            radio1.IsChecked = false;
            radio2.IsChecked = false;
            radio3.IsChecked = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Button to clear all

            //Pop-up dialog box to ask the user if they wish to clear recipe data
            MessageBoxResult delete = MessageBox.Show("Clear Data?", "RESET APP", MessageBoxButton.OKCancel);

            if (delete == MessageBoxResult.OK) //If-statement when user clicks on 'OK'
            {
                clearEverything(); //Method called to clear all data
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Button to exit app

            clearEverything(); //Method called to clear all data

            this.Close(); //Closes and exits the app
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            confirmationMethod(); //Method called to check if the List is empty, and return an error message
        }

        private void addRecipe_Click(object sender, RoutedEventArgs e)
        {
            {
                //Button to confirm ingredient details and add them to the generic List collection

                //Try-catch to handle exception if one or all ingredient text boxes are empty
                try
                {

                    //Variables to grab values from textboxes
                    string name = ingredient.Text;
                    string measurement = measure.Text;
                    double quantity = (double.Parse(qty.Text));
                    string recipeName = rname.Text;
                    string recipeDescription = new TextRange(textb.Document.ContentStart, textb.Document.ContentEnd).Text; ;

                    //Creating an object of the List collection
                    recipeDetails recipeDetails = new recipeDetails(name, measurement, quantity, recipeName, recipeDescription);
                    ingredientsList.Add(recipeDetails); //Adding data from textboxes to the generic collection

                    clearTextBoxes(); //Method called to clear textboxes

                    //Dialog box to ask if user wants to add another ingredient to the current recipe
                    MessageBoxResult ingredients = MessageBox.Show("Enter more ingredients?", "Ingredients", MessageBoxButton.YesNo);

                    if (ingredients == MessageBoxResult.Yes) //if statements to check which option the user selects
                    {

                        rname.Text = recipeName;
                    }
                    else if (ingredients == MessageBoxResult.No)
                    {
                        //if the user picks the "No" option, add the recipe name to the list box and save the recipe info
                        list.Items.Add(recipeDetails.recipeName);
                        // list.Sorted = true;

                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show("RECIPE INFORMATION IS INCOMPLETE, TRY AGAIN"); //Error message if one or more textboxes are left empty
                }
            }
        }
        public class recipeDetails
        {
            //Public class to store recipe details

            //Variables to store ingredient info
            public string itemName;
            public string itemMeasurement;
            public double itemQuantity;
            public string recipeName;
            public string recipeDescription;

            private readonly double originalQuantity;

            public recipeDetails(string itemName, string itemMeasurement, double itemQuantity, string recipeName, string recipeDescription)
            {
                //Constructor for ingredients
                this.itemName = itemName;
                this.itemMeasurement = itemMeasurement;
                this.itemQuantity = itemQuantity;
                this.recipeName = recipeName;
                this.recipeDescription = recipeDescription;

                originalQuantity = itemQuantity;
            }

            public void recipeScaling(double scaleBy)
            {
                //Method to scale recipe quantity
                itemQuantity = originalQuantity * scaleBy;
            }

            public void resetRecipe()
            {
                //Method to reset quantity
                itemQuantity = originalQuantity;
            }

        }

        private void filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void check_Click(object sender, RoutedEventArgs e)
        {
            //Button to filter recipe list by specific ingredient 

            //Textbox for ingredient
                string ingredient = filter.Text;

            //variable to check the list and repopulate the list after filtering
                var filteredRecipes = ingredientsList
                    .Where(recipe => recipe.itemName.Contains(ingredient))
                    .Select(recipe => recipe.recipeName);

            //Setting items source to null to prevent crashing if users filters while an item is selected
                list.ItemsSource = null;
                list.Items.Clear(); //Clearing the original list
                list.ItemsSource = filteredRecipes; //Repopulating the list with filtered recipe
            
        }

    }
}

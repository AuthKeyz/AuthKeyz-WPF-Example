using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AuthKeyz;

namespace AuthKeyz_WPF_Example
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        /// 
        public static api AuthKeyzApp = new api(
         name: "", // App name
         ownerid: "", // Account ID
         version: "" // Application version. Used for automatic downloads see video here https://www.youtube.com/watch?v=kW195PLCBKs
                      //path: @"Your_Path_Here" // (OPTIONAL) see tutorial here https://www.youtube.com/watch?v=I9rxt821gMk&t=1s
     );
        // VIEW https://authkeyz.bond/docs to see all of the functions that you can run.

        public Login()
        {
            InitializeComponent();
        }

        private async void Login_Loaded(object sender, RoutedEventArgs e)
        {
            await AuthKeyzApp.init();
        } 

        private async void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            await AuthKeyzApp.login(usernameField.Text, passwordField.Text, tfaField.Text);
            if (AuthKeyzApp.response.success)
            {
                Main main = new Main();
                main.Show();
                this.Close();

                await AuthKeyzApp.log(usernameField.Text + " Logged In"); // this will send a log to the logs channel https://authkeyz.bond/app/?page=logs OR to your Discord server if you enabled Discord logs.
            }
            else
            {
                MessageBox.Show(AuthKeyzApp.response.message, "AuthKeyz Response");
            }
        }

        private async void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            string? email = emailField.Text;
            if (!email.Contains("@"))
            {
                email = null;
            }

            await AuthKeyzApp.register(usernameField.Text, passwordField.Text, email);
            if (AuthKeyzApp.response.success)
            {
                Main main = new Main();
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(AuthKeyzApp.response.message, "AuthKeyz Response");
            }
        }

        private async void upgradeBtn_Click(object sender, RoutedEventArgs e)
        {
            await AuthKeyzApp.upgrade(usernameField.Text, licenseField.Text);
            MessageBox.Show(AuthKeyzApp.response.message, "AuthKeyz Response");
        }

        private async void licenseBtn_Click(object sender, RoutedEventArgs e)
        {
            await AuthKeyzApp.license(licenseField.Text, tfaField.Text);
            if (AuthKeyzApp.response.success)
            {
                Main main = new Main();
                main.Show();
                this.Close();

                await AuthKeyzApp.log(licenseField.Text + " Logged In");
            }
            else
            {
                MessageBox.Show(AuthKeyzApp.response.message, "AuthKeyz Response");
            }
        }

        private async void forgotPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            await AuthKeyzApp.forgot(usernameField.Text, emailField.Text);
            MessageBox.Show(AuthKeyzApp.response.message);
        }
    }
}

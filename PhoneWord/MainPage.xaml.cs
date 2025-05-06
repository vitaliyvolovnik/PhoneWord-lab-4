namespace PhoneWord
{
    public partial class MainPage : ContentPage
    {
        string? translatedNumber;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnTranslate(object sender, EventArgs e)

        {

            string enteredNumber = PhoneNumberText.Text;
            translatedNumber = PhonewordTranslator.ToNumber(enteredNumber);

            if (!string.IsNullOrEmpty(translatedNumber))
            {
                CallButton.IsEnabled = true;
                CallButton.Text = "Call " + translatedNumber;

            }

            else
            {
                CallButton.IsEnabled = false;
                CallButton.Text = "Call";

            }

        }

        async void OnCall(object sender, System.EventArgs e)
        {
            bool confirm = await DisplayAlert(
               "Dial a Number",
               $"Would you like to call {translatedNumber}?",
               "Yes",
               "No");

            if (confirm && !string.IsNullOrEmpty(translatedNumber))
            {
                try
                {
                    if (PhoneDialer.Default.IsSupported && !string.IsNullOrWhiteSpace(translatedNumber))
                        PhoneDialer.Default.Open(translatedNumber);
                }

                catch (ArgumentNullException)
                {
                    await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");

                }
                catch (Exception)

                {

                    // Other error has occurred.
                    await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");

                }
            }


        }

    }
}

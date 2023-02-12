using System;
using System.Text;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Email;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Services.Store;
using Windows.System.Profile;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace MorseCode.UWP.UserControls
{
    public sealed partial class AboutUserControl : UserControl
    {
        private readonly StoreContext _storeContext;
        public AboutUserControl()
        {
            InitializeComponent();
            _storeContext = StoreContext.GetDefault();
            Loaded += AboutUserControl_Loaded;
        }

        private void AboutUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Package package = Package.Current;
            PackageVersion version = package.Id.Version;
            PackageVersion.Text = string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
            PackageName.Text = package.DisplayName;
            PackageUser.Text = package.PublisherDisplayName;

            if (Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.IsSupported())
            {
                FeedBack.Visibility = Visibility.Visible;
            }
        }

        private async void HyperlinkButton_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            HyperlinkButton button = sender as HyperlinkButton;
            switch (int.Parse(button.Tag.ToString()))
            {
                case 0:
                    await Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.GetDefault().LaunchAsync();
                    break;

                case 1:
                    Package package = Package.Current;
                    EmailMessage email = new EmailMessage()
                    {
                        Subject = "Questions-" + package.DisplayName
                    };
                    email.To.Add(new EmailRecipient("avkapps@outlook.com"));

                    // Taken from http://www.kunal-chowdhury.com/2016/01/uwp-device-information.html#k2KOzTtTYgOyIMmm.97
                    StringBuilder _stringBuilder = new StringBuilder();
                    _stringBuilder.Append(Environment.NewLine).Append(Environment.NewLine).Append("=============Device Information=============").Append(Environment.NewLine);

                    _stringBuilder.Append("Device Family: " + AnalyticsInfo.VersionInfo.DeviceFamily).Append(Environment.NewLine);

                    // get the system version number
                    var deviceFamilyVersion = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
                    var version = ulong.Parse(deviceFamilyVersion);
                    var majorVersion = (version & 0xFFFF000000000000L) >> 48;
                    var minorVersion = (version & 0x0000FFFF00000000L) >> 32;
                    var buildVersion = (version & 0x00000000FFFF0000L) >> 16;
                    var revisionVersion = (version & 0x000000000000FFFFL);
                    var systemVersion = $"{majorVersion}.{minorVersion}.{buildVersion}.{revisionVersion}";
                    _stringBuilder.Append("Device Version: " + systemVersion).Append(Environment.NewLine);

                    // get the device manufacturer, model name, OS details etc.
                    var clientDeviceInformation = new EasClientDeviceInformation();
                    _stringBuilder.Append("Device Manufacturer: " + clientDeviceInformation.SystemManufacturer).Append(Environment.NewLine);
                    _stringBuilder.Append("Device Model: " + clientDeviceInformation.SystemProductName).Append(Environment.NewLine);

                    _stringBuilder.Append("==========================================");

                    email.Body = _stringBuilder.ToString();

                    await EmailManager.ShowComposeNewEmailAsync(email);
                    break;

                case 2:
                    StoreRateAndReviewResult result = await _storeContext.RequestRateAndReviewAppAsync();
                    string storeMessage;
                    switch (result.Status)
                    {
                        case StoreRateAndReviewStatus.Succeeded:
                            if (result.WasUpdated)
                            {
                                storeMessage = "Review updated.";
                            }
                            else
                            {
                                storeMessage = "Review added.";
                            }
                            // TODO: Dont Prompt again.
                            break;

                        case StoreRateAndReviewStatus.CanceledByUser:
                            storeMessage = "Review cancelled.";
                            // TODO: Dont Prompt again.
                            break;

                        case StoreRateAndReviewStatus.NetworkError:
                            storeMessage = "Network Issue";
                            break;

                        case StoreRateAndReviewStatus.Error:
                        default:
                            storeMessage = "Unexpected Error";
                            //LogError(result.ExtendedError, result.ExtendedJsonData); // pseudo-code
                            break;
                    }
                    if (!string.IsNullOrEmpty(storeMessage))
                    {
                        await new MessageDialog(storeMessage).ShowAsync();
                    }
                    break;
            }
        }
    }
}

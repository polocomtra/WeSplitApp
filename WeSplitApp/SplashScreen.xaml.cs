using WeSplitApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WeSplitApp
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public Random _rng = new Random();
        System.Timers.Timer timer;
        int count = 0;
        int target = 5;
        public SplashScreen()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var value = ConfigurationManager.AppSettings["ShowSplashScreen"];
            var showSplash = bool.Parse(value);
            Debug.WriteLine(value);

            if (showSplash == false)
            {
                CloseSplashScreen();
            }
            else
            {
                timer = new System.Timers.Timer();
                timer.Elapsed += Timer_Elapsed;
                timer.Interval = 1000;

                int index = _rng.Next(InfomationList.Length);
                var infomation = InfomationList[index];
                FoodInformation.Text = infomation;

                SplashOKBtn.Content = $"OK ({target})";
                timer.Start();
            }
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            count++;
            Dispatcher.Invoke(() =>
            {
                SplashOKBtn.Content = $"OK ({target - count})";
            });
            if (count == target)
            {
                timer.Stop();

                Dispatcher.Invoke(() =>
                {
                    CloseSplashScreen();
                });
            }
        }

        private void DontShowThisAgain(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "false";
            config.Save(ConfigurationSaveMode.Minimal);
        }

        private void ShowThisAgain(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "true";
            config.Save(ConfigurationSaveMode.Minimal);
        }

        string[] InfomationList =
        {
            "Cáp treo Bà Nà là cáp treo dài nhất thế giới có ở Việt Nam. Đó là tuyến Cáp treo nối từ chân núi Bà Nà đến đỉnh Vọng Nguyệt (thuộc Khu du lịch Bà Nà – Suối Mơ).",
            "Đà Lạt được mệnh danh là thành phố của ngàn hoa, là nơi có khí hậu mát mẻ quanh năm, nơi đây được quy hoạch thành khu nghỉ mát từ thời Pháp thuộc và ghi dấu ấn tốt đẹp trong lòng du khách muôn phương.",
            "Vịnh Hạ Long đã được UNESCO công nhận là di sản thiên nhiên của nhân loại và hiện đang được bảo tồn tích cực. Vịnh Hạ Long trong tiếng Việt có nghĩa là nơi rồng đáp xuống.",
            "Bãi biển Việt Nam là một trong những bãi biển đẹp nhất trên hành tinh. Các bãi biển nổi tiếng như Lăng Cô, Mỹ Khê, biển Nha Trang… Angeline Jolie đã từng ca ngợi vẻ đẹp tuyệt vời của bờ biển Việt Nam trong khoảng thời gian lưu lại tại đây.",
            "Phú Quốc là hòn đảo lớn nhất Việt Nam, tại Phú Quốc có Bãi Dài nằm ở Bắc Đảo được đài truyền hình ABC News Australia bình chọn là 1 trong 10 bãi biển hoang sơ và đẹp nhất thế giới.",
            "Hang Sơn Đoòng rộng 150 m, cao hơn 200 m, kéo dài gần 9km. Với kích thước như thế, hang Sơn Đoòng đã vượt qua hang Deer ở Vườn quốc gia Gunung Mulu - Malaysia (với chiều cao 100 m, rộng 90 m, dài 2km) để chiếm vị trí là hang động tự nhiên lớn nhất thế giới.",
            "Tới Sapa, du khách có thể cảm nhận được không khí “4 trong 1”, tức là bốn mùa trong một ngày. Buổi sáng ở Sapa có khí hậu của xuân, buổi trưa lại nóng như mùa hè, xế chiều có nắng nhưng hơi se lạnh của mùa thu thì tối khuya lại là thời tiết của mùa đông.",
            "Là một trong những thành phố đẹp nhất dọc theo đồng bằng sông Cửu Long, Cần Thơ là nơi có vô số cảnh quan đẹp. Từ cánh đồng lúa đến rừng ngập mặn, hay chợ nổi đầy mầu sắc chuyên trao đổi, mua bán nông sản, hàng hóa, thực phẩm, ăn uống… trên sông.",
            "Cáp treo Vinpearl Nha Trang là tuyến cáp treo vượt biển vịnh Nha Trang dài 3.320 m, nối Nha Trang với khu du lịch Hòn Ngọc Việt trên đảo Hòn Tre. Đây là tuyến cáp treo vượt biển dài nhất thế giới với sức chứa tám người trên một cabin.",
            "Vẻ đẹp của Phú Quốc chính là những bãi biển cát trắng trải dài. Bên cạnh đó, du khách có thể khám phá rừng nhiệt đới, lặn biển ngắm san hô, tham quan vườn thú Vinpearl Safari Phú Quốc...",
            "Ghềnh Đá Đĩa được hình thành khi núi lửa phun trào dung nham xuống biển, khi gặp nước lạnh cùng nhiều hiện tượng tự nhiên khác tạo nên cảnh quan kỳ thú như hôm nay. Bãi đá với hàng nghìn những phiến đá, óng lên màu đen nổi bật giữa nước biển xanh và những con sóng vỗ trắng xóa.",
            "Nhà hát lớn Hà Nội được người Pháp khởi công xây dựng năm 1901 và hoàn thành vào năm 1911, theo mẫu Nhà hát Opéra Garnier (Paris) nhưng mang tầm vóc nhỏ hơn và sử dụng các vật liệu phù hợp với điều kiện khí hậu địa phương.",
            "Côn Đảo thu hút du khách bởi những bãi biển tuyệt đẹp và quần thể sinh vật biển phong phú, đặc biệt là san hô. Đây còn là điểm đến lý tưởng để tận hưởng một cuộc sống yên bình cho những ai đang cảm thấy ngột ngạt với nhịp sống hối hả của thành thị.",
            "Nhắc đến Bản Giốc là nhắc đến một trong những thác hùng vĩ và đẹp nhất Việt Nam, không những thế còn được xếp hạng vào top những thác thiên đường tuyệt đẹp trên thế giới. Một chuyến du lịch đến thác Bản Giốc sẽ cho du khách những trải nghiệm thú vị và tươi mới.",
            "Nhà thờ Đức Bà (Sài Gòn) là một trong những công trình kiến trúc độc đáo nhất tại Sài Gòn, luôn thu hút sự quan tâm của du khách trong và ngoài nước. Toà thánh đường có chu vi 91 x 35,5 m, cao 21 m. Đây là một công trình kiến trúc thật sự có giá trị rất lớn về mặt lịch sử và nghệ thuật kiến trúc xây dựng.",
            "Đà Lạt được ví như một Tiểu Paris, Đà Lạt từng mộng mơ và nên thơ nhờ cái lạnh cao nguyên ban đêm, sương mù buổi sớm và những dải rừng thông bao quanh thành phố. Thiên nhiên ưu đãi cho xứ sở Đà Lạt khí hậu ôn hòa, là thiên đường của rất nhiều loài hoa.",
            "Huyện đảo Cát Bà là quần đảo có tới 367 đảo lớn nhỏ. Cát Bà là tên hòn đảo chính rộng khoảng 100 km2, cách cảng Hải Phòng 30 hải lý, nằm ở phía nam Vịnh Hạ Long, tạo nên một quần thể đảo và hang động trên biển làm mê hồn du khách.",
            "Ngô Đồng là tên dòng sông nhỏ chảy qua địa phận xã Ninh Hải, huyện Hoa Lư, tỉnh Ninh Bình. Vắt ngang mình giữa hệ thống núi đá vôi xanh rờn. Sông Ngô Đồng là đường thủy duy nhất đưa du khách vào tham quan Tam Cốc - một vùng non nước từng được ví như “Hạ Long trên cạn”.",
            "Vườn quốc gia Phong Nha - Kẻ Bàng là vườn quốc gia tại huyện Bố Trạch, và Minh Hóa (tỉnh Quảng Bình), cách thành phố Đồng Hới khoảng 50 km về phía tây bắc, cách thủ đô Hà Nội khoảng 500 km về phía nam.",
            "Vườn quốc gia Cúc Phương nằm trên địa phận tỉnh Ninh Bình, cách Hà Nội 120 km về phía tây nam, cách thành phố Ninh Bình 45 km về phía tây bắc. Cúc Phương có diện tích 25 nghìn héc-ta, tiếp giáp ba tỉnh Ninh Bình, Hòa Bình và Thanh Hóa."
        };

        private void CloseSplashScreen()
        {
            var screen = new MainWindow();
            this.Close();
            screen.Show();
        }

        private void CloseSplashScreenBtn(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            CloseSplashScreen();
        }
    }
}

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.System.Diagnostics;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NicoleToolbox.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        private DispatcherTimer? _timer;
        private bool _hardwareLoaded;
        private PerformanceCounter? _cpuCounter;
        private int _minuteCounter = 0;

        public HomePage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_timer == null)
            {
                _timer = new DispatcherTimer();
                _timer.Interval = TimeSpan.FromSeconds(1);
                _timer.Tick += Timer_Tick;
            }
            _ = Task.Run(() =>
            {
                try
                {
                    var counter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                    counter.NextValue();

                    DispatcherQueue.TryEnqueue(() =>
                    {
                        _cpuCounter?.Dispose();
                        _cpuCounter = counter;
                    });
                }
                catch { }
            });
            _timer.Start();
            UpdateTime();
            DiskCInfo();
            GreetingTextLoad();

            if (!_hardwareLoaded)
            {
                _hardwareLoaded = true;

                await Task.Delay(1);

                WindowsVersionLoad();
                MotherBoardLoad();
                CPULoad();
                MemoryLoad();
                GPULoad();
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (_timer != null)
            {
                _timer.Stop();
            }
            _cpuCounter?.Dispose();
            _cpuCounter = null;
        }

        private string GetGreeting()
        {
            var now = DateTime.Now;
            int hour = now.Hour;
            string userName = Environment.UserName;

            return hour switch
            {
                >= 5 and < 9 => $"早上好，{userName}。",
                >= 9 and < 12 => $"上午好，{userName}。",
                >= 12 and < 14 => $"中午好，{userName}。",
                >= 14 and < 18 => $"下午好，{userName}。",
                >= 18 and < 23 => $"晚上好，{userName}。",
                _ => $"夜深了，{userName}，请注意休息。"
            };
        }

        private void GreetingTextLoad()
        {
            GreetingText.Text = GetGreeting();
        }

        private void Timer_Tick(object? sender, object e)
        {
            UpdateTime();
            if (_cpuCounter != null)
            {
                float cpuPercent = _cpuCounter.NextValue();
                CPUProgress.Value = cpuPercent;
                CPUPercentage.Text = $"{cpuPercent:F1}%";
            }
            UpdateUsage();
            _minuteCounter++;
            if (_minuteCounter >= 60)
            {
                _minuteCounter = 0;
                GreetingTextLoad();
                DiskCInfo();
            }
        }

        private void UpdateTime()
        {
            var now = DateTime.Now;
            TimeText.Text = now.ToString("HH:mm:ss");
            DateText.Text = now.ToString("yyyy年MM月dd日 dddd", new CultureInfo("zh-CN"));
        }

        private void UpdateUsage()
        {
            var reportram = SystemDiagnosticInfo.GetForCurrentSystem().MemoryUsage.GetReport();
            ulong total = reportram.TotalPhysicalSizeInBytes;
            ulong available = reportram.AvailableSizeInBytes;
            if (total == 0)
            {
                return;
            }
            ulong used = total - available;
            double rampercentage = ((double)used / total) * 100;
            RAMProgress.Value = rampercentage;
            RAMPercentage.Text = $"{rampercentage:F1}%";
            double totalGB = (double)total / 1024 / 1024 / 1024;
            double availableGB = (double)available / 1024 / 1024 / 1024;
            double usedGB = (double)used / 1024 / 1024 / 1024;
            RAMUsage.Text = $"{usedGB:F1} GB / {totalGB:F1} GB";
        }

        private void DiskCInfo()
        {
            DriveInfo cDrive = new DriveInfo("C");
            long totalBytes = cDrive.TotalSize;
            long freeBytes = cDrive.AvailableFreeSpace;
            long usedBytes = totalBytes - freeBytes;
            double totalGB = totalBytes / 1024.0 / 1024.0 / 1024.0;
            double freeGB = freeBytes / 1024.0 / 1024.0 / 1024.0;
            double usedGB = usedBytes / 1024.0 / 1024.0 / 1024.0;
            CTotal.Text = $"{totalGB:F1} GB";
            CUsed.Text = $"{usedGB:F1} GB";
            CFree.Text = $"{freeGB:F1} GB";
            double CDiskProgressValue = (usedBytes * 1.0 / totalBytes) * 100;
            CDiskProgress.Value = CDiskProgressValue;
            CDiskPercentage.Text = $"{CDiskProgressValue:F1}%";
        }

        private void WindowsVersionLoad()
        {
            // Get Windows Version
            int windowsbuild = 0;
            string windowsversion = "未知";
            try
            {
                windowsbuild = Environment.OSVersion.Version.Build;
            }
            catch (Exception)
            {

            }
            if (windowsbuild >= 22000)
            {
                windowsversion = "Windows 11";
            }
            else if (windowsbuild >= 10240)
            {
                windowsversion = "Windows 10";
            }
            else if (windowsbuild >= 9600)
            {
                windowsversion = "Windows 8.1";
            }
            else if (windowsbuild >= 9200)
            {
                windowsversion = "Windows 8";
            }
            else if (windowsbuild >= 7600)
            {
                windowsversion = "Windows 7";
            }
            else if (windowsbuild == 0)
            {
                windowsversion = "检测失败";
            }
            else
            {
                windowsversion = "更早的 Windows";
            }

            // Get Windows Edition
            string editionId = "未知";
            try
            {
                editionId = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "EditionID", null) as string ?? "未知";
            }
            catch (Exception)
            {
                editionId = "检测失败";
            }
            string edition = editionId switch
            {
                "Core" => "家庭版",
                "CoreCountrySpecific" => "家庭中文版",
                "CoreSingleLanguage" => "家庭单语言版",
                "Professional" => "专业版",
                "ProfessionalWorkstation" => "工作站版",
                "Education" => "教育版",
                "Enterprise" => "企业版",
                "EnterpriseS" => "LTSC 长期服务版",
                _ => editionId
            };

            // Edit UI Text
            WindowsVersion.Text = windowsversion + " " + edition;
        }

        private void MotherBoardLoad()
        {
            string motherboardModel = "未知";
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT Product FROM Win32_BaseBoard"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        motherboardModel = obj["Product"]?.ToString() ?? "未知";
                        break;
                    }
                }
            }
            catch (Exception)
            {
                motherboardModel = "检测失败";
            }
            MotherBoard.Text = motherboardModel;
        }

        private void CPULoad()
        {
            string cpuModel = "未知";
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT Name FROM Win32_Processor"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        cpuModel = obj["Name"]?.ToString() ?? "未知";
                        break;
                    }
                }
            }
            catch (Exception)
            {
                cpuModel = "检测失败";
            }
            CPU.Text = cpuModel;
        }

        private void MemoryLoad()
        {
            int stickCount = 0;
            string ddrType = "未知";
            ulong singleCapacityGB = 0;

            try
            {
                var capacities = new List<ulong>();
                var types = new List<string>();

                using (var searcher = new ManagementObjectSearcher(
                    "SELECT SMBIOSMemoryType, Capacity FROM Win32_PhysicalMemory"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        stickCount++;

                        object rawType = obj["SMBIOSMemoryType"];
                        int typeCode = (rawType is null || rawType == DBNull.Value)
                            ? 0
                            : Convert.ToInt32(rawType);

                        object rawCap = obj["Capacity"];
                        ulong capBytes = (rawCap is null || rawCap == DBNull.Value)
                            ? 0
                            : Convert.ToUInt64(rawCap);

                        string currentType = typeCode switch
                        {
                            18 => "DDR",
                            19 => "DDR2",
                            24 => "DDR3",
                            26 => "DDR4",
                            27 => "LPDDR",
                            28 => "LPDDR2",
                            29 => "LPDDR3",
                            30 => "LPDDR4",
                            34 => "DDR5",
                            35 => "LPDDR5",
                            _ => "未知"
                        };
                        types.Add(currentType);

                        ulong capGB = capBytes / 1073741824;
                        if (capGB > 0)
                        {
                            capacities.Add(capGB);
                            if (singleCapacityGB == 0)
                            {
                                singleCapacityGB = capGB;
                            }
                        }
                    }
                }

                var knownTypes = types.Where(t => t != "未知").Distinct().ToList();
                if (knownTypes.Count == 1)
                {
                    ddrType = knownTypes[0];
                }
                else if (knownTypes.Count > 1)
                {
                    ddrType = string.Join(" + ", knownTypes);
                }

                string memoryDisplay;
                if (stickCount == 0)
                {
                    memoryDisplay = "未检测到内存";
                }
                else if (capacities.Count == 0)
                {
                    memoryDisplay = $"{ddrType} 容量未知 x {stickCount}";
                }
                else if (capacities.Distinct().Count() == 1)
                {
                    memoryDisplay = $"{ddrType} {singleCapacityGB}G x {capacities.Count}";
                }
                else
                {
                    ulong totalCapacityGB = capacities.Aggregate(0UL, (sum, c) => sum + c);
                    memoryDisplay = $"{ddrType} {totalCapacityGB}G ({string.Join(" + ", capacities.OrderBy(c => c).Select(c => $"{c}G"))})";
                }

                Memory.Text = memoryDisplay;
            }
            catch (Exception)
            {
                Memory.Text = "检测失败";
            }
        }

        private void GPULoad()
        {
            string gpuModel = "未知";
            var gpuList = new List<string>();
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT Name FROM Win32_VideoController"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        string? name = obj["Name"]?.ToString();
                        if (!string.IsNullOrEmpty(name))
                        {
                            gpuList.Add(name);
                        }
                    }
                }
            }
            catch (Exception)
            {
                gpuList.Clear();
                gpuList.Add("检测失败");
            }

            if (gpuList.Count > 0)
            {
                gpuModel = string.Join(", ", gpuList);
            }
            GPU.Text = gpuModel;
        }

        private async void OpenSystemSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            bool success = await Launcher.LaunchUriAsync(new Uri("ms-settings:"));
            if (!success)
            {
                await ShowError("打开失败，可能您的系统不支持此操作。", "错误");
            }
        }

        private async void OpenStorageSettingButton_Click(object sender, RoutedEventArgs e)
        {
            bool success = await Launcher.LaunchUriAsync(new Uri("ms-settings:storagesense"));
            if (!success)
            {
                await ShowError("打开失败，可能您的系统不支持此操作。", "错误");
            }
        }

        private async void OpenTaskmgrButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "taskmgr", UseShellExecute = true });
            }
            catch (Exception ex)
            {
                await ShowError("打开失败，可能您的系统不支持此操作。\n错误信息：" + ex.Message, "错误");
            }
        }

        private async Task ShowError(string message, string title)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "确定",
                XamlRoot = this.Content.XamlRoot,
                Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style
            };
            if (this.Content is FrameworkElement rootElement)
            {
                dialog.RequestedTheme = rootElement.RequestedTheme;
            }
            await dialog.ShowAsync();
        }

        private void OpenClockWindow_Click(object sender, RoutedEventArgs e)
        {
            new WidgetWindow.ClockWindow().Activate();
        }

        private async void OpenSystemInfoButton_Click(object sender, RoutedEventArgs e)
        {
            bool success = await Launcher.LaunchUriAsync(new Uri("ms-settings:about"));
            if (!success)
            {
                await ShowError("打开失败，可能您的系统不支持此操作。", "错误");
            }
        }
    }
}

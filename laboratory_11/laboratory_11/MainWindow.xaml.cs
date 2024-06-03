using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace laboratory_11
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        delegate int CalculateNumeratorDelegate(int n, int k);
        delegate int CalculateDenominatorDelegate(int k);
        
        private async void ButtonNewton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Task 1:\n");
            int N = 5;
            int K = 2;
            
            // task 1
            Task<int> numeratorTask = Task.Run(() => CalculateNumerator(N, K));
            Task<int> denominatorTask = Task.Run(() => CalculateDenominator(K));
        
            Task.WaitAll(numeratorTask, denominatorTask);

            int binomialCoefficient1 = numeratorTask.Result / denominatorTask.Result;
            
            Console.WriteLine("Implementation 1:");
            Console.WriteLine($"Newton's binomial theorem for N={N} and K={K} is {binomialCoefficient1}");
            
            // task 2
            
            CalculateNumeratorDelegate numeratorDelegate = new CalculateNumeratorDelegate(CalculateNumerator);
            CalculateDenominatorDelegate denominatorDelegate = new CalculateDenominatorDelegate(CalculateDenominator);

            IAsyncResult numeratorResult = numeratorDelegate.BeginInvoke(N, K, null, null);
            IAsyncResult denominatorResult = denominatorDelegate.BeginInvoke(K, null, null);

            int numerator = numeratorDelegate.EndInvoke(numeratorResult);
            int denominator = denominatorDelegate.EndInvoke(denominatorResult);

            int binomialCoefficient2 = numerator / denominator;

            Console.WriteLine("Implementation 2:");
            Console.WriteLine($"Newton's binomial theorem for N={N} and K={K} is {binomialCoefficient2}");
            
            // task 3
            
            int numerator2 = await CalculateNumeratorAsync(N, K);
            int denominator2 = await CalculateDenominatorAsync(K);

            int binomialCoefficient3 = numerator2 / denominator2;

            Console.WriteLine("Implementation 3:");
            Console.WriteLine($"Newton's binomial theorem for N={N} and K={K} is {binomialCoefficient3}");
        }

        static int CalculateNumerator(int n, int k)
        {
            int result = 1;
            for (int i = 0; i < k; i++)
            {
                result *= (n - i);
            }
            return result;
        }

        static int CalculateDenominator(int k)
        {
            int result = 1;
            for (int i = 1; i <= k; i++)
            {
                result *= i;
            }
            return result;
        }
        
        static Task<int> CalculateNumeratorAsync(int n, int k)
        {
            return Task.Run(() => 
            {
                int result = 1;
                for (int i = 0; i < k; i++)
                {
                    result *= (n - i);
                }
                return result;
            });
        }

        static Task<int> CalculateDenominatorAsync(int k)
        {
            return Task.Run(() =>
            {
                int result = 1;
                for (int i = 1; i <= k; i++)
                {
                    result *= i;
                }
                return result;
            });
        }

        private void ButtonFibonacci_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int n = 10; // Przykładowe n
            int a = 0;
            int b = 1;

            for (int i = 0; i < n; i++)
            {
                int temp = a;
                a = b;
                b = temp + b;
                Thread.Sleep(5); // Symulacja obliczeń
                (sender as BackgroundWorker).ReportProgress((i + 1) * 100 / n, b); // Aktualizacja progress baru
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine($"Progress: {e.ProgressPercentage}%, current element: {e.UserState}");
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Obliczenia zakończone.");
        }

        private async void ButtonCompress_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Wybierz katalog"
            };

            if (dialog.ShowDialog() == true)
            {
                string folderPath = Path.GetDirectoryName(dialog.FileName);
                await CompressFilesAsync(folderPath);
                MessageBox.Show("Kompresja zakończona.");
            }
        }

        private async Task CompressFilesAsync(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath);
            foreach (string file in files)
            {
                await Task.Run(() =>
                {
                    using (FileStream sourceStream = new FileStream(file, FileMode.Open))
                    {
                        using (FileStream targetStream = File.Create(file + ".gz"))
                        {
                            using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                            {
                                sourceStream.CopyTo(compressionStream);
                            }
                        }
                    }
                });
                Dispatcher.Invoke(() => TextBlockResult.Text = $"Skompresowano plik: {file}");
            }
        }
    }
}

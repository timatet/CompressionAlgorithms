using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using AlgorithmsLibrary;

namespace AlgorithmsWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // создать булевскй массив для определения алгоритма
        List<string> ArrayNameOfAlgorithm = new List<string>() {"Кодирование Хаффмана", "Коды Фано-Шеннона",
            "Арифметическое кодирование", "Алгоритм RLE и преобразование Барроуза-Уилера",
            "Словарный метод сжатия LZ77", "Код Хемминга", "Линейный код тима (5,2)" };
        int IndexOfCurrentAlgorithm = 0;
        List<Border> Borders = new List<Border>();
        public MainWindow()
        {
            InitializeComponent();
            Borders.Add(Huf_Fano_Arith_Border);
            Borders.Add(RLE_LZ77_Border);
            Borders.Add(Ham_Border);
        }

        private void HuffmanClicButton(object sender, RoutedEventArgs e)
        {
            IndexOfCurrentAlgorithm = 0;
            foreach (var item in Borders)
                item.Visibility = Visibility.Hidden;
            NameOfAlgorithm.Text = ArrayNameOfAlgorithm[0];
            ClearHuf_Fano_Arith_Border();
            Huf_Fano_Arith_Border.Visibility = Visibility.Visible;
        }

        private void ShannonFanoClicButton(object sender, RoutedEventArgs e)
        {
            IndexOfCurrentAlgorithm = 1;
            foreach (var item in Borders)
                item.Visibility = Visibility.Hidden;
            NameOfAlgorithm.Text = ArrayNameOfAlgorithm[1];
            ClearHuf_Fano_Arith_Border();
            Huf_Fano_Arith_Border.Visibility = Visibility.Visible;
        }

        private void ArithmeticCodingClicButton(object sender, RoutedEventArgs e)
        {
            IndexOfCurrentAlgorithm = 2;
            foreach (var item in Borders)
                item.Visibility = Visibility.Hidden;
            NameOfAlgorithm.Text = ArrayNameOfAlgorithm[2];
            ClearHuf_Fano_Arith_Border();
            Huf_Fano_Arith_Border.Visibility = Visibility.Visible;
        }

        private void RLEClicButton(object sender, RoutedEventArgs e)
        {
            IndexOfCurrentAlgorithm = 3;
            foreach (var item in Borders)
                item.Visibility = Visibility.Hidden;
            NameOfAlgorithm.Text = ArrayNameOfAlgorithm[3];
            ClearRLE_LZ77_Border();
            RLE_LZ77_Border.Visibility = Visibility.Visible;
        }

        private void LZ77ClicButton(object sender, RoutedEventArgs e)
        {
            IndexOfCurrentAlgorithm = 4;
            foreach (var item in Borders)
                item.Visibility = Visibility.Hidden;
            NameOfAlgorithm.Text = ArrayNameOfAlgorithm[4];
            ClearRLE_LZ77_Border();
            RLE_LZ77_Border.Visibility = Visibility.Visible;
        }

        private void HammingClicButton(object sender, RoutedEventArgs e)
        {
            IndexOfCurrentAlgorithm = 5;
            foreach (var item in Borders)
                item.Visibility = Visibility.Hidden;
            NameOfAlgorithm.Text = ArrayNameOfAlgorithm[5];
            ClearHam_Border();
            Ham_Border.Visibility = Visibility.Visible;
        }

        private void LinearCodesType52ClicButton(object sender, RoutedEventArgs e)
        {
            IndexOfCurrentAlgorithm = 6;
            foreach (var item in Borders)
                item.Visibility = Visibility.Hidden;
            NameOfAlgorithm.Text = ArrayNameOfAlgorithm[6];
            //Clear();
            //_Border.Visibility = Visibility.Visible;
        }

        private void EncodeClicButton(object sender, RoutedEventArgs e)
        {
            if (TextForEncoding.Text == "")
            {
                //MessageWindow
            }
            switch (IndexOfCurrentAlgorithm)
            {
                case 0:
                    var Huf = HuffmanAlgm.Encode(TextForEncoding.Text);
                    EncodedText.Text = Huf.GetAnswer();
                    foreach (var i in Huf.GetData())
                        FriqDictionary.Text += i.Key.ToString() + " " + i.Value.ToString()+'\n';
                    CompressionRatio.Text = HuffmanAlgm.CalculateCompressionRatio(TextForEncoding.Text, EncodedText.Text).ToString();
                    break;
                case 1:
                    var Sha = ShannonFanoAlgm.Encode(TextForEncoding.Text);
                    EncodedText.Text = Sha.GetAnswer();
                    foreach (var i in Sha.GetData())
                        FriqDictionary.Text += i.Key.ToString() + " " + i.Value.ToString()+'\n';
                    CompressionRatio.Text = ShannonFanoAlgm.CalculateCompressionRatio(TextForEncoding.Text, EncodedText.Text).ToString();
                    break;
                case 2:
                    var Ari = ArithmeticCodingAlgm.Encode(TextForEncoding.Text);
                    EncodedText.Text = Ari.GetAnswer();
                    foreach (var i in Ari.GetData().GetData())
                        FriqDictionary.Text += i.Key.ToString() + " " + i.Value.ToString()+'\n';
                    CompressionRatio.Text = ArithmeticCodingAlgm.CalculateCompressionRatio(TextForEncoding.Text, EncodedText.Text).ToString();
                    break;
                case 3:
                    var Rle = RLEAlgm.Encode(Text1ForEncoding.Text);
                    foreach (var i in Rle.GetAnswer())
                        Encoded1Text.Text += i.ToString(); 
                    CompressionRatio.Text = RLEAlgm.CalculateCompressionRatio(Text1ForEncoding.Text, Encoded1Text.Text).ToString();
                    break;
                case 4:
                    var Lz = LZ77Algm.Encode(Text1ForEncoding.Text);
                    foreach (var i in Lz.GetAnswer())
                        Encoded1Text.Text += i.ToString();
                    CompressionRatio.Text = LZ77Algm.CalculateCompressionRatio(Text1ForEncoding.Text, Encoded1Text.Text).ToString();
                    break;
                case 5:
                    var Ham = HammingAlgm.Encode(Text1ForEncoding.Text);
                    Encoded2Text.Text = Ham.GetAnswer();
                    break;
                case 6:
                    break;
            }
        }

        private void DecodeClicButton(object sender, RoutedEventArgs e)
        {

            // в зависимости от алгоритма запустить декодировку
            // проверка на пустоту полей для исходого текста
        }

        private void EncodeFromFileClicButton(object sender, RoutedEventArgs e)
        {
            // диалоговое окно
        }

        private void DecodeFromFileClicButton(object sender, RoutedEventArgs e)
        {
            //диалоговое окно
        }

        private void ClearAllClicButton(object sender, RoutedEventArgs e)
        {
            //в зависимости от алгоритма очистить поля
        }

        private void EnterDown_Dictionary(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                FriqDictionary.Text += '\n';
                FriqDictionary.SelectionStart = FriqDictionary.Text.Length;
            }
        }

        void ClearHuf_Fano_Arith_Border()
        {
            TextForEncoding.Text = string.Empty;
            EncodedText.Text = string.Empty;
            TextAfterDecoding.Text = string.Empty;
            CompressionRatio.Text = string.Empty;
            FriqDictionary.Text = string.Empty;
        }
        void ClearRLE_LZ77_Border()
        {
            Text1ForEncoding.Text = string.Empty;
            Encoded1Text.Text = string.Empty;
            Text1AfterDecoding.Text = string.Empty;
            CompressionRatio1.Text = string.Empty;
        }
        void ClearHam_Border()
        {
            Text2ForEncoding.Text = string.Empty;
            Encoded2Text.Text = string.Empty;
            Text2AfterDecoding.Text = string.Empty;

        }
    }
}

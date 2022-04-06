using AlgorithmsLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AlgorithmsWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            string str = "";
            bool NoTextForEncoding = false;
            switch (IndexOfCurrentAlgorithm)
            {
                case 0:
                    TextAfterDecoding.Text = string.Empty;
                    if (TextForEncoding.Text.Length == 0) { NoTextForEncoding = true; break; }

                    var Huf = HuffmanAlgm.Encode(TextForEncoding.Text);
                    EncodedText.Text = Huf.GetAnswer();
                    foreach (var i in Huf.GetData())
                        str += i.Key.ToString() + " " + i.Value.ToString() + '\n';
                    FriqDictionary.Text = str;
                    CompressionRatio.Text = Huf.GetCompressionRatio().ToString();
                    break;
                case 1:
                    TextAfterDecoding.Text = string.Empty;
                    if (TextForEncoding.Text.Length == 0) { NoTextForEncoding = true; break; }

                    var Sha = ShannonFanoAlgm.Encode(TextForEncoding.Text);
                    EncodedText.Text = Sha.GetAnswer();
                    foreach (var i in Sha.GetData())
                        str += i.Key.ToString() + " " + i.Value.ToString() + '\n';
                    FriqDictionary.Text = str;
                    CompressionRatio.Text = Sha.GetCompressionRatio().ToString();
                    break;
                case 2:
                    TextAfterDecoding.Text = string.Empty;
                    if (TextForEncoding.Text.Length == 0) { NoTextForEncoding = true; break; }

                    var Ari = ArithmeticCodingAlgm.Encode(TextForEncoding.Text);
                    EncodedText.Text = Ari.GetAnswer();
                    foreach (var i in Ari.GetData().GetData())
                        str += i.Key.ToString() + " " + i.Value.ToString() + '\n';
                    FriqDictionary.Text = str;
                    CompressionRatio.Text = Ari.GetCompressionRatio().ToString();
                    break;
                case 3:
                    Text1AfterDecoding.Text = string.Empty;
                    if (Text1ForEncoding.Text.Length == 0) { NoTextForEncoding = true; break; }

                    var Rle = RLEAlgm.Encode(Text1ForEncoding.Text);
                    foreach (var i in Rle.GetAnswer())
                        str += i.ToString();
                    Encoded1Text.Text = str;
                    CompressionRatio1.Text = Rle.GetCompressionRatio().ToString();
                    break;
                case 4:
                    Text1AfterDecoding.Text = string.Empty;
                    if (Text1ForEncoding.Text.Length == 0) { NoTextForEncoding = true; break; }

                    var Lz = LZ77Algm.Encode(Text1ForEncoding.Text);
                    foreach (var i in Lz.GetAnswer())
                        str += i.ToString();
                    Encoded1Text.Text = str;
                    CompressionRatio1.Text = Lz.GetCompressionRatio().ToString();
                    break;
                case 5:
                    Text2AfterDecoding.Text = string.Empty;
                    if (Text2ForEncoding.Text.Length == 0) { NoTextForEncoding = true; break; }

                    var Ham = HammingAlgm.Encode(Text2ForEncoding.Text);
                    Encoded2Text.Text = Ham.GetAnswer();
                    break;
                case 6:
                    break;
            }
            if (NoTextForEncoding)
            {
                // открыть сообщение о том, что нет текста для кодирования
            }
        }

        private void DecodeClicButton(object sender, RoutedEventArgs e)
        {
            bool NoTextForDecoding = false, NoDictionaryForDecoding = false;
            switch (IndexOfCurrentAlgorithm)
            {
                case 0:
                    if (EncodedText.Text.Length == 0) { NoTextForDecoding = true; break; }
                    if (FriqDictionary.Text.Length == 0) { NoDictionaryForDecoding = true; break; }

                    var Huf = HuffmanAlgm.Decode(CreateDictionary(FriqDictionary.Text), EncodedText.Text);
                    TextAfterDecoding.Text = Huf.GetAnswer();
                    break;
                case 1:
                    if (EncodedText.Text.Length == 0) { NoTextForDecoding = true; break; }
                    if (FriqDictionary.Text.Length == 0) { NoDictionaryForDecoding = true; break; }

                    var Sha = ShannonFanoAlgm.Decode(CreateDictionary(FriqDictionary.Text), EncodedText.Text);
                    TextAfterDecoding.Text = Sha.GetAnswer();
                    break;
                case 2:
                    if (EncodedText.Text.Length == 0) { NoTextForDecoding = true; break; }
                    if (FriqDictionary.Text.Length == 0) { NoDictionaryForDecoding = true; break; }

                    var Ari = ArithmeticCodingAlgm.Decode(CreateDictionary(FriqDictionary.Text).ToDictionary(x => x.Key, x => int.Parse(x.Value)),
                        EncodedText.Text, TextForEncoding.Text.Length);
                    TextAfterDecoding.Text = Ari.GetAnswer();
                    break;
                case 3:
                    if (Encoded1Text.Text.Length == 0) { NoTextForDecoding = true; break; }

                    var Rle = RLEAlgm.Decode(Encoded1Text.Text);
                    Text1AfterDecoding.Text = Rle.GetAnswer();
                    //Text1AfterDecoding.Text = "Пока не готово";
                    // отловить ошибку
                    break;
                case 4:
                    if (Encoded1Text.Text.Length == 0) { NoTextForDecoding = true; break; }

                    var Lz = LZ77Algm.Decode(Encoded1Text.Text);
                    Text1AfterDecoding.Text = Lz.GetAnswer();
                    //Text1AfterDecoding.Text = "Пока не готово";
                    // отловить ошибку
                    break;
                case 5:
                    if (Encoded2Text.Text.Length == 0) { NoTextForDecoding = true; break; }

                    var Ham = HammingAlgm.Decode(Encoded2Text.Text);
                    Text2AfterDecoding.Text = Ham.GetAnswer();
                    break;
                case 6:
                    break;
            }
            if (NoTextForDecoding)
            {
                // открыть сообщение о том, что нет текста для декодирования
            }
            if (NoDictionaryForDecoding)
            {
                // открыть сообщение о том, что нет кодов для декодирования
            }
        }

        private void EncodeFromFileClicButton(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Files|*.txt";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                if (new List<int>() { 0, 1, 2 }.Contains(IndexOfCurrentAlgorithm))
                {
                    ClearHuf_Fano_Arith_Border();
                    using (StreamReader sr = new StreamReader(filename))
                        TextForEncoding.Text = sr.ReadToEnd();
                }
                if (new List<int>() { 3, 4 }.Contains(IndexOfCurrentAlgorithm))
                {
                    ClearRLE_LZ77_Border();
                    using (StreamReader sr = new StreamReader(filename))
                        Text1ForEncoding.Text = sr.ReadToEnd();
                }
                if (new List<int>() { 5 }.Contains(IndexOfCurrentAlgorithm))
                {
                    ClearHam_Border();
                    using (StreamReader sr = new StreamReader(filename))
                        Text2ForEncoding.Text = sr.ReadToEnd();
                }
                // добавить для линейного кодирования
            }
        }

        private void DecodeFromFileClicButton(object sender, RoutedEventArgs e)
        {
            //диалоговое окно для открытия файла

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Files|*.txt";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;

                // НУЖНО ОПРЕДЕЛИТЬСЯ С ФОРМАТОМ ВХОДНОГО ФАЙКА ( КАКОЙ РАЗДЕЛИТЕЛЬ МЕЖДУ ДАННЫМИ)

                //if (new List<int>() { 0, 1, 2 }.Contains(IndexOfCurrentAlgorithm))
                //{
                //    ClearHuf_Fano_Arith_Border();
                //    using (StreamReader sr = new StreamReader(filename))
                //        TextForEncoding.Text = sr.ReadToEnd();
                //}
                //if (new List<int>() { 3, 4 }.Contains(IndexOfCurrentAlgorithm))
                //{
                //    ClearRLE_LZ77_Border();
                //    using (StreamReader sr = new StreamReader(filename))
                //        Text1ForEncoding.Text = sr.ReadToEnd();
                //}
                //if (new List<int>() { 5 }.Contains(IndexOfCurrentAlgorithm))
                //{
                //    ClearHam_Border();
                //    using (StreamReader sr = new StreamReader(filename))
                //        Text2ForEncoding.Text = sr.ReadToEnd();
                //}
                // добавить для линейного кодирования
            }
        }

        private void ClearAllClicButton(object sender, RoutedEventArgs e)
        {
            if (new List<int>() { 0, 1, 2 }.Contains(IndexOfCurrentAlgorithm))
                ClearHuf_Fano_Arith_Border();
            if (new List<int>() { 3, 4 }.Contains(IndexOfCurrentAlgorithm))
                ClearRLE_LZ77_Border();
            if (new List<int>() { 5 }.Contains(IndexOfCurrentAlgorithm))
                ClearHam_Border();
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

        private void EnterDown_Dictionary(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                FriqDictionary.Text += '\n';
                FriqDictionary.SelectionStart = FriqDictionary.Text.Length;
            }
        }// Написать такие методы для всех полей

        Dictionary<char, string> CreateDictionary(string str)
        {
            Dictionary<char, string> dic = new Dictionary<char, string>();
            int i = 0, index = 1;
            while (index < str.Length)
            {
                index = str.IndexOf('\n', ++index);
                dic.Add(str[i], str.Substring(i + 2, index - i - 2));
                index++;
                i = index;
            }
            return dic;
        }
    }
}

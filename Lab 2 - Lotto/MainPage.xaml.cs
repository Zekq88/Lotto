using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lab_2___Lotto
{
    /// <summary>
    /// Denna klass innehåller alla funktionell kod i applicationen.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        HashSet<int> datalist = new HashSet<int>();
        List<int> sevenList = new List<int>();
        List<int> sixList = new List<int>();
        List<int> fiveList = new List<int>();
     

        Random rand = new Random();
       


        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Denna metod innehåller en try-catch som lagrar User-Input in till den temporära listan som granska innan adderas in i datalist,
        /// som är en HashSet och därmed inte tar emot dubbleter. 
        /// </summary>
        private void ReadData()
        {
            try
            {
                List<int> list = new List<int>();
                list.Add(Convert.ToInt32(txt1.Text));
                list.Add(Convert.ToInt32(txt2.Text));
                list.Add(Convert.ToInt32(txt3.Text));
                list.Add(Convert.ToInt32(txt4.Text));
                list.Add(Convert.ToInt32(txt5.Text));
                list.Add(Convert.ToInt32(txt6.Text));
                list.Add(Convert.ToInt32(txt7.Text));

                foreach (int i in list)
                {
                    if ((i <= 0 || i > 35) || datalist.Contains(i))
                    {

                        resultTxt.Text += "Du måste välja ett tal mellan 1 - 35 och varje tal ska vara annorlunda \n";
                        throw new ArgumentOutOfRangeException();

                    }
                    else
                    {
                        datalist.Add(i);
                    }

                }
            }
            catch (Exception e)
            {
                resultTxt.Text += " ReadData(): " + e.Message.ToString();
            }

        }


        /// <summary>
        /// Denna funktion ansvarar för att generera sju olika unika tal per rad som Usern bestämmer.
        /// För varje rad så granskas alla tal för att uppfylla kraven och cheakData() utlöses.
        /// </summary>
        private void insertData()
        {
            try
            {
                HashSet<Tuple<int, HashSet<int>>> lottoTotalList = new HashSet<Tuple<int, HashSet<int>>>();
                for (int i = 0; i < Convert.ToInt32(antalDragnignar.Text); i++)
                {
                    HashSet<int> lottoColumList = new HashSet<int> ();

                    while (lottoColumList.Count() < 7)
                    {
                        int g = rand.Next(1, 36);
                        if (!lottoColumList.Contains(g))
                        {
                            lottoColumList.Add(g);
                        }
                            
                    } 
                    
                        lottoTotalList.Add(new Tuple<int, HashSet<int>>(i, lottoColumList));
                    

                    // cheakData(lottoColumList);
                    
                }

                foreach (var tuple in lottoTotalList)
                {
                    analysData(cheakData(tuple.Item2));
                }
            }
            catch (Exception e)
            {
                resultTxt.Text += " insertsData(): " + e.Message.ToString();
            }
        }

        /// <summary>
        /// Denna metoden analyserar antalet rätta tal per rad ur listan som cheakData() hanterar för att sedan addera den till varsin lista.
        /// </summary>
        private void analysData(int i)
        {
         
            
                if(i == 0)
            {
                return;
            }
                

                 else if (i == 7)
                {
                    sevenList.Add(1);
                    return;

                }
                else if (i == 6)
                {
                    sixList.Add(1);
                    return;
                }
                else if (i == 5)
                {
                    fiveList.Add(1);
                    return;
                }
            
            
            return;
        }

        /// <summary>
        /// Denna funktionen samspelar med analysData() då denna funktionen lägger till ett nummer för varje rätt som varje rad har innan den granskas i analysData().
        /// </summary>
        private int cheakData(HashSet<int> lottoColumList)
        {
            List<int> scoreList = new List<int>();
            try
            {

                
                foreach (int colum in lottoColumList)
                {
                    if (datalist.Contains(colum))
                    {
                        scoreList.Add(1);
                    }
                    
                }
                
                
            }
            catch (Exception e)
            {

                resultTxt.Text = " cheakData(): " + e.Message.ToString();
            }
            return scoreList.Count();
        }

        /// <summary>
        /// Denna metoden redovisar antalet rader som har fem, sex eller sju rätt i sina egna textrytor efter startar clearData().
        /// </summary>
        private void showData()
        {

            antalRatt7.Text = sevenList.Count.ToString();
            antalRatt6.Text = sixList.Count.ToString();
            antalRatt5.Text = fiveList.Count.ToString();
            clearData();
        }

        /// <summary>
        /// Denna metoden nollställer alla listor och counters på data.
        /// </summary>
        private void clearData()
        {
            datalist.Clear();
            sevenList.Clear();
            sixList.Clear();
            fiveList.Clear();
           
        }

        /// <summary>
        /// Denna metoden startar hela applicationen genom att trycka på Start knappen som nollställer resultat-textboxen innan utlöser ReadData(), insertData(), showData().
        /// </summary>
        public void Start_Click(object sender, RoutedEventArgs e)
        {
            resultTxt.Text = "";
            ReadData();
            insertData();
            showData();
        }
    }
}
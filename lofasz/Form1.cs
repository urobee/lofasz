using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.IO;

namespace lofasz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //szóval itt kezdődik  a userneves izé a listbox 2 minden elemén végig megy
            for(int username=0;username<listBox2.Items.Count;username++)
            {
                //szétválasztja , alapján
                string fbuser = listBox2.Items[username].ToString();
                

                //elindul a chrome kikapcsolja a notificationokat
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--disable-extensions"); // to disable extension
                options.AddArguments("--disable-notifications"); // to disable notification
                options.AddArguments("--disable-application-cache"); // to disable cache

                IWebDriver driver = new ChromeDriver(options);
                driver.Navigate().GoToUrl("https://www.facebook.com/marketplace/?ref=bookmark");

                //beírja az email címet és a jelszavat vagy jelszót
                string[] result = fbuser.Split(',');
                string fbname = (result[0]);
                string fbpass = (result[1]);
                
                IWebElement email = driver.FindElement(By.Name("email"));
                email.SendKeys(fbname);
                IWebElement password = driver.FindElement(By.Name("pass"));
                password.SendKeys(fbpass);

                //rákattint a bejelentkezés gomra
                IWebElement btn_login2 = driver.FindElement(By.XPath("//*[@id = 'loginbutton']"));
                btn_login2.Click();


                //beírja a random várost--kiválasztja a random várost onnantól hozzáadja a többit annyiszor ahányszor a decimal updownba van

                Random random1 = new Random();




                decimal ismetles = numericUpDown1.Value;


                for (int i = 1; i < ismetles + 1; i++)
                {

                    //rákattint a sell something gombra
                    IWebElement btn_sell = driver.FindElement(By.XPath("//*[contains (@class,'_54qk _43ff _')]"));
                    btn_sell.Click();

                    Thread.Sleep(20000);

                    //kiválasztja az első képet

                    IWebElement btn_picture = driver.FindElement(By.XPath("//*[@class='_3jk']"));

                    Thread.Sleep(2000);
                    btn_picture.Click();
                    Random random2 = new Random();
                    string elsokep = random2.Next(1, 9).ToString();

                    //string path2 = (@"C:\Users\mártika\Desktop\Bútor\képek másolata\3D\cropped-images\képem.png").Replace("képem", elsokep);
                    string pth1 = textBox1pic.Text;
                    string path1 = (pth1 + elsokep + ".png");

                    int kepek = 1;
                    Thread.Sleep(2000);
                    SendKeys.SendWait(path1);
                    Thread.Sleep(2000);
                    SendKeys.SendWait(@"{Enter}");
                    //első kép kiválasztásának vége

                    //beilleszti a többi képet. Itt adom meg azt is hogy hány képet akarok feltölteni
                    while (kepek < 4)
                    {

                        Thread.Sleep(2000);
                        // IWebElement btn_picture = driver.FindElement(By.XPath("//*[@class='_3jk']"));
                        btn_picture.Click();
                        //átalakítom az int et stringgé aztán új pathet adok meg neki--első kép mappája
                        string kepek2 = kepek.ToString();
                        string pth2 = textBox2pic.Text;

                        string path2 = (pth2 + kepek2 + ".png");



                        Thread.Sleep(2000);
                        SendKeys.SendWait(path2);
                        Thread.Sleep(2000);
                        SendKeys.SendWait(@"{Enter}");

                        kepek++;
                    }


                    //beírja címet a textboxból
                    IWebElement txt_cim = driver.FindElement(By.XPath("//*[@placeholder ='What are you selling?']"));
                    string txtbxcim = textBoxcim.Text;
                    txt_cim.SendKeys(txtbxcim);


                    //beírja az árat a textboxból
                    IWebElement txt_ar = driver.FindElement(By.XPath("//*[@placeholder ='Price']"));
                    string txtbxar = textBoxar.Text;
                    txt_ar.SendKeys(txtbxar);


                    //hát itt nem is tudom mi történik de remélem hogy műkszik
                    //elvileg a random szám a forban meghatározott szám és  a listbox egyes zámok között van valamiért kivonok belőle 4et

                    int rand = random1.Next(1, listBox1.Items.Count);
                    string varosnev = listBox1.Items[rand].ToString();
                    //IWebElement txt_hely = driver.FindElement(By.XPath("/html/body/div[6]/div[2]/div/div/div/div/div[2]/div/div/div[1]/div[3]/div[2]/div/span/label/input"));

                    //IWebElement txt_hely = driver.FindElement(By.XPath("//*[@class='_36g4  _4yei _3mtb _58ah' and @style='background-color: rgb(255, 255, 255); border-color: rgb(221, 223, 226); height: 28px; width: 250px;']"));
                    Thread.Sleep(1000);
                    SendKeys.SendWait("{TAB}");
                    Thread.Sleep(1000);
                    
                    Thread.Sleep(2000);
                    SendKeys.SendWait(varosnev);
                    Thread.Sleep(1000);
                    SendKeys.SendWait("{ENTER}");
                    Thread.Sleep(2000);


                    //beírja a kategóriát
                    //IWebElement txt_category = driver.FindElement(By.XPath("/html/body/div[6]/div[2]/div/div/div/div/div[2]/div/div/div[1]/div[4]/div/span/span/label/input"));

                    IWebElement txt_category = driver.FindElement(By.XPath("//*[@placeholder='Select a Category' and @class='_58al']"));
                    //txt_category.Click();
                    
                    Thread.Sleep(1000);
                    txt_category.SendKeys("furniture");
                    Thread.Sleep(1000);
                    SendKeys.SendWait("{DOWN}");
                    Thread.Sleep(1000);
                    SendKeys.SendWait("{ENTER}");

                    //beírja a leírást a textboxból
                    IWebElement txt_description = driver.FindElement(By.XPath("//*[@class='_1mf _1mj']"));
                    string txtbxleiras = richTextBox1.Text;
                    //string txtbxleiras = textBoxleiras.Text;
                    txt_description.SendKeys(txtbxleiras);


                    //savedraft
                    IWebElement mentes = driver.FindElement(By.XPath("//*[@class='_1mf7 _4jy0 _4jy3 _517h _51sy _42ft']"));
                    mentes.Click();

                    //pihi várja amíg betölt
                    Thread.Sleep(10000);
                    Random randvege = new Random();
                    int randvege2 = randvege.Next(10000, 50000);
                    Thread.Sleep(randvege2);
                    //elvileg itt a városbeírós loop vége
                }
                Thread.Sleep(20000);
                driver.Quit();

                //elvileg itt a userneves loop vége
            }

        }

        //behívja a megyéket
        private void btn_megye_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            int counter = 0;
            string line;                
            
            foreach (string s in checkedlist_megye.CheckedItems)
            {
                


                System.IO.StreamReader file = new System.IO.StreamReader(s);

                while ((line = file.ReadLine()) != null)
                {
                    //add the line to CheckedListBox, you need to pass the parameters "index" & "string"
                    listBox1.Items.Insert(counter, line);

                    //increase the index
                    counter++;
                }

                //close the file
                file.Close();


            }





        }



        //beírja az adott user. valahogy majd ketté kell választani a jelszót
        private void btn_user_Click(object sender, EventArgs e)
        {


            listBox2.Items.Clear();
            
            foreach (string s in checkedlistUser.CheckedItems)
            {
                listBox2.Items.Add(s);              
             
            }

        }



        //első kép pathwaye

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();

            if (fDialog.ShowDialog() != DialogResult.OK)

                return;

            System.IO.FileInfo fInfo = new System.IO.FileInfo(fDialog.FileName);

            string strFileName = fInfo.Name;

            string strFilePath = (fInfo.DirectoryName + @"\");
            textBox1pic.Text = strFilePath;
        }


        //többi kép pathwaye
        private void button3_Click(object sender, EventArgs e)
        {




            OpenFileDialog fDialog = new OpenFileDialog();

            if (fDialog.ShowDialog() != DialogResult.OK)

                return;

            System.IO.FileInfo fInfo = new System.IO.FileInfo(fDialog.FileName);

            string strFileName = fInfo.Name;

            string strFilePath = (fInfo.DirectoryName + @"\");
            textBox2pic.Text = strFilePath;
        }


        //megyék fájlneve

        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBDbtn4 = new FolderBrowserDialog();
            if (FBDbtn4.ShowDialog() == DialogResult.OK)
            {

                checkedlist_megye.Items.Clear();
                String[] files = Directory.GetFiles(FBDbtn4.SelectedPath);
                String[] dirs = Directory.GetDirectories(FBDbtn4.SelectedPath);
                

                foreach (string file in files)
                {
                    checkedlist_megye.Items.Add(Path.GetFullPath(file));
                    
                }
                foreach (string dir in dirs)
                {
                    checkedlist_megye.Items.Add(Path.GetFileNameWithoutExtension(dir));
                }
                
            }
        }
        //beolvassa a user neveket a checkedlistbe
        private void button5_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog FBDbtn5 = new FolderBrowserDialog();
            if (FBDbtn5.ShowDialog() == DialogResult.OK)
            {

                checkedlistUser.Items.Clear();
                String[] files = Directory.GetFiles(FBDbtn5.SelectedPath);
                String[] dirs = Directory.GetDirectories(FBDbtn5.SelectedPath);



                foreach (string file in files)
                {
                    string userpath = Path.GetFullPath(file);
                    int counter = 0;
                    string line;
                    string k = userpath;
                    System.IO.StreamReader file2 = new System.IO.StreamReader(k);
                    while ((line = file2.ReadLine()) != null)
                    {
                        //add the line to CheckedListBox, you need to pass the parameters "index" & "string"
                        checkedlistUser.Items.Insert(counter, line);

                        //increase the index
                        counter++;
                    }

                    //close the file
                    file2.Close();
                

                }


                
            }

            



                
            
        }  
    }
}

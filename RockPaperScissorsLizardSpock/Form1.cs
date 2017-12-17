using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RockPaperScissorsLizardSpock{
 public partial class Form1 : Form{
bool playMode = true; //Змінна для зберігання поточного режиму
        const int ROCK = 0;
const int PAPER = 1;
const int SCISSORS = 2; // //Кожному символу відповідає число
const int LIZARD = 3;
const int SPOCK = 4;
PictureBox[] pictureBoxes = new PictureBox[5];//Збереження посилень на зображення над кнопками
        int[] winRate;//Збереження значень перемого, нічиїх, поразок
        private int[] userGuesses; //Збереження значень "скільки разів гравець обрав символ"
        public Form1()
        {
            InitializeComponent();
        }
private void Form1_Load(object sender, EventArgs e){
            userGuesses = new int[5];

            pictureBoxes[0] = pictureBox1;
            pictureBoxes[1] = pictureBox2;// Завантаження посилань в масив зображень
            pictureBoxes[2] = pictureBox3;
            pictureBoxes[3] = pictureBox4;
            pictureBoxes[4] = pictureBox5;
            for (int i = 0; i < pictureBoxes.Length; i++)
            {
                pictureBoxes[i].Visible = false;//Всі зображення робляться невидимими
            }
        }
        private void animateGuesses(PictureBox userGuess, PictureBox keeperGuess)
        {
            for (int i = 0; i < 20; i++)
            {//Всі можливі зображення перебираються по черзі і підставляються в блоки 
                userGuess.Image = Image.FromFile("../pic/" + i%5 + ".png");
                keeperGuess.Image = Image.FromFile("../pic/" + i % 5 + ".png");
                this.Refresh();
                System.Threading.Thread.Sleep(15);//Затримка роботи
            }
        }
        //Кнопка в контекстному меню "ПРОСТИЙ РЕЖИМ"
        private void simpleGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            playMode = false;//Перехід в простий режим
            button4.Enabled = false;//Кнопки  "ЯЩІРКА" і "СПОК" недоступні
            button5.Enabled = false;
        }
        //Натискання на кнопку "РОЗШИРЕНиЙ РЕЖИМ
        private void exteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            playMode = true;
            button4.Enabled = true;
            button5.Enabled = true;
        }
        //Режим НАВЧАННЯ 
        private int computerPlayEx()
        {
            Random rnd = new Random();//Генерує випадкове число
            int index = 0;
            int guessCount = 0;//Кількість ігор, що були зіграні
            for (int i = 0; i < userGuesses.Length; i++)
            {
                guessCount += userGuesses[i];
            }
           

            if (guessCount > 1) //якщо гравець зробив хоча б один вибір/одна гра пройшла
            {
                int max = userGuesses[0];
                for (int i = 0; i < userGuesses.Length; i++)//розраховуємо імовірності вибору гравцем кожного з символів
                {
                    if (max < userGuesses[i])
                    {
                        max = userGuesses[i];
                        index = i;
                    }
                }
                if (rnd.Next(0, 2) == 0)
                {
                    switch (index)
                    {
                        case 0: return 1;
                        case 1: return 2;
                        case 2: return 0;
                    }
                }
            }
            return computerPlay();

        }
        private int computerPlay()
        {
            Random guess = new Random();
            if (playMode)
                return guess.Next(0, 5);
            else
                return guess.Next(0, 3);
        }//Функція порівняння символу гравця і ПК
        private int comparator(int userGuess, int keeperGuess)
        {
            const int USER_WIN = 1;
            const int USER_LOSE = -1;//МОЖЛВІ варіанти гри
            const int TIE = 0;
            animateGuesses(pictureBoxes[userGuess], pictureBox6);//Виклик анімації
            pictureBox6.Image = Image.FromFile("../pic/" + keeperGuess + ".png"); //Зображення яке обрав ПК

            String situation = userGuess.ToString() + keeperGuess.ToString();
            if (!playMode)//Кілкість ситуацій різна для 3 символів і для 5
            {
                switch (situation)
                {
                    case "00": return TIE;//два камені
                    case "01": return USER_LOSE;//камінь проти паперу
                    case "02": return USER_WIN;//камінь – ножиці і т. д.
                    case "10": return USER_WIN;
                    case "11": return TIE;
                    case "12": return USER_LOSE;
                    case "20": return USER_LOSE;
                    case "21": return USER_WIN;
                    case "22": return TIE;
                    default: return -1;
                }
            }else
            {
                switch (situation)
                {//ТЕ саме тільки варіантів більше
                    case "00": return TIE;
                    case "01": return USER_LOSE;
                    case "02": return USER_WIN;
                    case "03": return USER_WIN;
                    case "04": return USER_LOSE;

                    case "10": return USER_WIN;
                    case "11": return TIE;
                    case "12": return USER_LOSE;
                    case "13": return USER_LOSE;
                    case "14": return USER_WIN;

                    case "20": return USER_LOSE;
                    case "21": return USER_WIN;
                    case "22": return TIE;
                    case "23": return USER_WIN;
                    case "24": return USER_LOSE;

                    case "30": return USER_LOSE;
                    case "31": return USER_WIN;
                    case "32": return USER_LOSE;
                    case "33": return TIE;
                    case "34": return USER_WIN;

                    case "40": return USER_LOSE;
                    case "41": return USER_WIN;
                    case "42": return USER_WIN;
                    case "43": return USER_LOSE;
                    case "44": return TIE;

                    default: return -1;
                }
            }
        }//Робимо одну з картинок видимою а решту невидимими
        private void visiblePicture(int buttonNumber)
        {
            for (int i = 0; i < pictureBoxes.Length; i++)
            {
                pictureBoxes[i].Visible = false;
            }
            pictureBoxes[buttonNumber].Visible = true;
        }//Виводимо картинку відповідно до натиснутої кнопки
        private void showPictureBox(int buttonNumber)
        {
            pictureBoxes[buttonNumber].Image = Image.FromFile("../pic/" + buttonNumber + ".png");
        }
        //Показуємо повідомлення в "Короткій грі"
        private void showMessage(int result)
        {
            if (!toolStripMenuItem1.Checked)
            {
                if (result > 0)//якщо гравець виграв
                {
                    MessageBox.Show("Мои поздравления!!\nТы выиграл.", "Result");
                }
                else if (result < 0)//якщо програв
                {
                    MessageBox.Show("Оу,попробуй еще.\nТы проиграл.", "Result");
                }
                else//нічия
                {
                    MessageBox.Show("Ничья.", "Result");
                }
            }else
            {//ДОВГИЙ РЕЖИМ БЕЗ повідомлень
                if (result > 0)
                {
                    winRate[0]++;//Рахуємо кількість перемог
                }
                else if (result < 0)
                {
                    winRate[2]++;//ПОразок
                }
                else
                {
                    winRate[1]++;//НІЧЇХ
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            visiblePicture(0);//Робимов зображення видим
            userGuesses[0]++;//Гравець зробив вибір, додаємо
            int result;
            if (learningModeToolStripMenuItem.Checked)
                result = comparator(0, computerPlayEx());
            else//Якщо в звичайному режимі порівнюємо вибір гравця з випадковим числом від  до 4
                result = comparator(0, computerPlay());
            showPictureBox(0);//Показуємо зображення яке вибрав користувач
            showMessage(result);//Функція виведення повідомлення
        }
        //ТЕ саме, тільки 0 змінився на 1.
        private void button2_Click(object sender, EventArgs e)
        {
            visiblePicture(1);
            userGuesses[1]++;
            int result;
            if (learningModeToolStripMenuItem.Checked)
                result = comparator(1, computerPlayEx());
            else
                result = comparator(1, computerPlay());
            showPictureBox(1);
            showMessage(result);
        }
        //1 на 2
        private void button3_Click(object sender, EventArgs e)
        {
            visiblePicture(2);
            userGuesses[2]++;
            int result;
            if (learningModeToolStripMenuItem.Checked)
                result = comparator(2, computerPlayEx());
            else
                result = comparator(2, computerPlay());
            showPictureBox(2);
            showMessage(result);
        }
        //2 на3
        private void button4_Click(object sender, EventArgs e)
        {
            visiblePicture(3);
            userGuesses[3]++;
            int result;
            if (learningModeToolStripMenuItem.Checked)
                result = comparator(3, computerPlayEx());
            else
                result = comparator(3, computerPlay());
            showPictureBox(3);
            showMessage(result);
        }
        //3 на 4
        private void button5_Click(object sender, EventArgs e)
        {
            visiblePicture(4);
            userGuesses[4]++;
            int result;
            if (learningModeToolStripMenuItem.Checked)
                result = comparator(4, computerPlayEx());
            else
                result = comparator(4, computerPlay());
            showPictureBox(4);
            showMessage(result);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripMenuItem1.Checked = !toolStripMenuItem1.Checked;
            contextMenuStrip1.Items[3].Enabled = false;
            if (toolStripMenuItem1.Checked)
            {
                winRate = new int[3];
                contextMenuStrip1.Items[3].Enabled = true;
            }
        }
        //Контекстне меню показати результати
   //Записуємо значення в з масиву перемого/нічиїх/порахок і виводимо повідомлення
        private void longGameResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String message = "Результаты длинной игры:\nПобеда: "
                + winRate[0] + "\nНичья:" + winRate[1] + "\nПоражение: " + winRate[2];
            MessageBox.Show(message, "Results");
        }
        //Увімкнути режим навчання 
        //При кожному натисканні птичка змінюється на протилежне значення
        private void learningModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            learningModeToolStripMenuItem.Checked = !learningModeToolStripMenuItem.Checked;
            if (learningModeToolStripMenuItem.Checked)
            {
                playMode = false;//Переходимо в простий режим
                button4.Enabled = false;//ЯЩІРКА і СПОК недоступні
                button5.Enabled = false;
           }}
        private void pictureBox3_Click(object sender, EventArgs e) {}
        private void pictureBox6_Click(object sender, EventArgs e){} }}

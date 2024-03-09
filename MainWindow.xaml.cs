using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchGame
{
    public partial class MainWindow : Window 
    {
        public MainWindow() /* Специальный метод, который называется конструктором. Все его содержимое вызывается
                             *сразу же после запуска приложения. */
        {
            InitializeComponent();
            SetUpGame();
        }

        //Метод настраивает элементы TextBlock так, чтобы в них выводились эмодзи с изображением животных.
        private void SetUpGame()
        {
            //Создает список из 8 пар эмодзи.
            List<string> animalEmoji = new List<string>()  /* List - коллекция для хранения набора значений в
                                                            * определенном порядке. new - ключевое слово, которое
                                                            * используется для создания списка List. */
            {
                "🐺", "🐺", "🦊", "🦊", "🐸", "🐸", "🐻", "🐻",
                "🦁", "🦁", "🐵", "🐵", "🐰", "🐰", "🐴", "🐴"
            };
            //Создает генератор случайных чисел.
            Random random = new Random();
            //Находит каждый элемент TextBlock в сетке и повторяет перечисленные команды для этого элемента. 
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) 
            {
                /* Генерирует случайное число от 0 до количества эмодзи
                 * и присваивает его переменной index. */
                int index = random.Next(animalEmoji.Count);
                //Использует случайное число для получения одного из эмодзи.
                string nextEmoji = animalEmoji[index];
                //Обновляет элемент TextBlock полученным эмодзи.
                textBlock.Text = nextEmoji;
                //Удаляет случайный эмодзи из списка.
                animalEmoji.RemoveAt(index);
            }
        }

        /* Если щелчок сделан на первом животном в паре, сохранить информацию о том, на каком элементе
         * TextBlock щелкнул пользователь и убрать животное с экрана. Если это второе животное в паре
         * либо убрать его с экрана (при совпадении), либо вернуть на экран первое животное (при промахе
         * игрока). */
        TextBlock lastTextBlockClicked;
        bool findingMatch;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility= Visibility.Visible;
                findingMatch = false;
            }
        }
    }
}
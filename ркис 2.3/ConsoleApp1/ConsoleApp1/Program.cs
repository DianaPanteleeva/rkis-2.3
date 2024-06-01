namespace RKIS2_3
{
    internal class Program
    {
        public static void Main(string[] args)       
        {
            // Список команд
             Console.WriteLine ("0 - Войти\n" + 
             "1 - Регистрация\n\t");
            
            int input = int.Parse(Console.ReadLine());  
            int id = -1;
            bool checkLogIn = true; // Переменная для проверки входа в систему
            while (checkLogIn != false)   
            {
                string login = "";     
                string password = "";
                switch (input)
                {                  
                    case 0: 
                        Console.WriteLine("Введите свой логин: ");   
                        login = Console.ReadLine();
                        Console.WriteLine("Введите свой пароль: ");  
                        password = Console.ReadLine();
                        DatabaseRequests.LogInUsers(login, password); // метод авторизации                      
                        break;
                    case 1: 
                        string[] paramUser = DatabaseRequests.CheckUser();   
                        login = paramUser[0];
                        password = paramUser[1];             
                        DatabaseRequests.AddUserInTable(login, password); // метод регистрации
                        break;                    
                    default:           
                        Console.Write("Нет такой команды");
                        break;
                    
                }
                id = DatabaseRequests.GetId(login, password);    
                if (id != -1)
                {                  
                    checkLogIn = false;
                }                
            }
            bool exit = true;  
            while (exit != false)
            {
                Console.WriteLine("\n\t\tКоманды:\n\t" +
                                  "0 - Посмотреть список задач\n\t" +    
                                  "1 - Посмотреть задачи (предстоящие, прошедшие, на сегодня, завтра, неделю)\n\t" +
                                  "2 - Добавить задачу\n\t" +       
                                  "3 - Редактировать задачу\n\t" +
                                  "4 - Удалить задачу\n\t" +    
                                  "5 - Выход из приложения\n");
                input = int.Parse(Console.ReadLine()); 
                switch (input)
                {                  
                    case 0: //список всех задач
                        DatabaseRequests.GetAllTasksTable(id);     
                        break;
                    case 1: // Просмотр задач по категории
                        ViewingTasks(id);
                        break;              
                    case 2:
                        AddTask(id); // Добавление задачи
                        break;
                    case 3:               
                        EditionTask(id); // Изменение задачи
                        break;             
                    case 4:
                        DeletionTask(id); // Удаление задачи
                        break;
                    case 5:                    
                        exit = false; // Выход из программы
                        break;         
                    default:
                        Console.WriteLine("\n\tНет такой команды");  
                        break;
                }
            }
        }
                // Просмотр задач по категории
             public static void ViewingTasks(int id)
        {            
            Console.WriteLine("\tПосмотреть:\n\t" +           
                              "0 - Список предстоящих задач\n\t" +
                              "1 - Список прошедших задач\n\t" +       
                              "2 - Список задач на сегодня\n\t" +
                              "3 - Список задач на завтра\n\t" +     
                              "4 - Список задач на неделю\n\t" +
                              "5 - Назад\n\t");
            int input = int.Parse(Console.ReadLine());
            DatabaseRequests.GetTasksTable(input, id);
            
        }
        
        // Добавление задачи     
        public static void AddTask(int id) 
        {
            Console.Write("Введите название задачи: ");
            string title = Console.ReadLine();            
            Console.Write("Введите описание задачи: ");    
            string description = Console.ReadLine();
            Console.Write("Введите срок выполнения задачи (дд/мм/гггг - 01/01/0001): ");
            string date = Console.ReadLine();
            DatabaseRequests.AddTaskInTable(title, description, date, id);             
            Console.WriteLine("\n\tЗадача была добавлена");
            
        }
        
        // Изменение задачи        
        public static void EditionTask(int id)       
        {
            int code = DatabaseRequests.CheckTask(id);             
            Console.WriteLine("\t0 - Изменить название\n\t" +
                              "1 - Изменить описание\n\t" + 
                              "2 - Изменить срок выполнения задачи\n\t");
            int input = int.Parse(Console.ReadLine()); 
            DatabaseRequests.EditionTaskInTable(input, code, id); 
            
            Console.WriteLine("\n\tЗадача была изменена");
        }        
        // Удаление задачи        
        public static void DeletionTask(int id) 
        {
            int code = DatabaseRequests.CheckTask(id);             
                        DatabaseRequests.DeletionTaskInTable(code, id); 
                        Console.WriteLine("\n\tЗадача была удалена");
                        
        }
        
    }
}
namespace Reloj
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        ProgressBar progressBar;
        Task[] tasks;
        private randomly object locker = new object();

        public MainPage()
        {
            InitializeComponent();
            progressBar = new ProgressBar
            {
                Progress = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center
            };
            layout.Children.Add(progressBar); //añadir la barra de progreso al diseño
            Time();
            Progreso();
            tasks= new Task[100];
            for (int i = 0; i < 100; i++)
            {
                tasks[i] = new Thread(() => Texto(i.ToString()));
                tasks[i].Start();
            }
            
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void Time()
        {
            while (true)
            {
                RelojLBL.Text = DateTime.Now.ToString("HH:mm:ss"); //reloj que funciona en tiempo real, conectado al ordenador
                await Task.Delay(1000); //Un delay de 1000 ms. Sin el await solo esperará una vez y cargará al instante.

            }
        }

        private async void Progreso()
        {
            for (int i = 0; i<=100; i++)
            {
                progressBar.Progress = i/100.0;
                await Task.Delay(500);
            }
        }

        private async void Texto(String txt)
        {
            await Task.Delay(50);
            lock (locker)
            {
                RelojLBL.Text = txt;
            }
        }
        

        
    }

}

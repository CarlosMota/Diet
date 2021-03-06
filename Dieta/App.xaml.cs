﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using System.Text.RegularExpressions;
using Dieta.Model;
using Dieta.Classes;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using Dieta.DAO;
using Microsoft.Phone.Scheduler;
using System.Diagnostics;

namespace Dieta
{
    public partial class App : Application
    {

        public Usuario Usuario { set; get; }
        //public Alimento alimento;
        //public Dieta.Classes.Refeicoes.Dieta dieta;
        public const String ARQUIVO_USUARIO = "Usuario.xml";
        public static string ConnectionString = "Data Source=isostore:/MainDieta1.sdf";
        //public DataHelper datahelper;
        public List<String> dadosString = new List<string>();
        public List<Refeicao> ListaRefeicao;
        public Refeicao refeicaoSelecionada;
        public Alimento alimentoSelecionado;
        public Configuracoes configuracoes;
        public DataBaseContext database;
        public double CaloriasTotais;
        public double CaloriasTotaisConsumidas;

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();
            ThemeManager.ToLightTheme();
            
            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }
            if (!configuracoes.existeCadastro("cadastro")) 
            {
                popularBanco();
                ConfiguracoesIniciais();
            }
            CaloriasTotaisConsumidas = 0;
            if (LerXML())
            {
                CaloriasTotais = Calculo.caluloCalorias(Usuario.Sexo, Usuario.Altura, Usuario.Peso, Usuario.PesoDesejado, Usuario.Idade, Usuario.NivelDeAtividade);
                ListaRefeicao = Fabrica.FabricaRefeicao.criarRefeicoes(CaloriasTotais);
                for (int i = 0; i < ListaRefeicao.Count; i++)
                {
                    CaloriasTotaisConsumidas += ListaRefeicao.ElementAt(i).QuantidadeCaloricaConsumida;
                }
            }
        }

        private void ConfiguracoesIniciais()
        {
            configuracoes.SetReminderAguaOn(true);
            configuracoes.SetReminderRefeicaoOn(true);
            configuracoes.SetHorarioInicioAgua("08:53");
            configuracoes.SetHorarioFimAgua("22:00");
            configuracoes.SetIntervaloAgua("01:00");
        }        

        private bool LerXML()
        {
            try
            {
                using (IsolatedStorageFile myISF = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = myISF.OpenFile(ARQUIVO_USUARIO, FileMode.Open))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Usuario));
                        stream.Position = 0;
                        Usuario = (Usuario)serializer.Deserialize(stream);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void popularBanco() 
        {
            
            Stream txtStream = Application.GetResourceStream(new Uri("Dados/DadosTeste.txt", UriKind.Relative)).Stream;
            StreamReader sr = null;
            int i = 0;
            sr = new StreamReader(txtStream);
            string dados = sr.ReadToEnd();
            dados = dados.Replace("\r\n", "\t");
            string[] line = Regex.Split(dados, "\t");
            sr.Close();
            int cont = 0;

            while (i < line.Length - 1)
            {
                Alimento novoAlimento = new Alimento();

                novoAlimento.DescricaoDoAlimento = line[i];
                novoAlimento.DescricaoPreparacao = line[i += 1];
                novoAlimento.Calorias = double.Parse(line[i += 1]);
                novoAlimento.Proteinas = double.Parse(line[i += 1]);
                novoAlimento.GorduraTotais = double.Parse(line[i += 1]);
                novoAlimento.Carboidratos = double.Parse(line[i += 1]);
                novoAlimento.Fibra_Alimentar = double.Parse(line[i += 1]);
                novoAlimento.Sodio = double.Parse(line[i += 1]);
                novoAlimento.GorduraSaturada = double.Parse(line[i += 1]);
                novoAlimento.GorduraTrans = double.Parse(line[i += 1]);
                novoAlimento.Editavel = false;
                novoAlimento.Realizada = false;

                novoAlimento.Gravar();
                i++;
                cont++;

            }
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
            configuracoes = new Configuracoes();
            Uri uri;
            if (LerXML())
            {
     /*           if (configuracoes.getCadastro("Cadastro").Equals("false"))
                {
                    RootFrame.Navigate(new Uri("/View/TelaCadastro.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {*/
                    RootFrame.Navigate(new Uri("/View/Perfil.xaml", UriKind.RelativeOrAbsolute));
   //             }
            }
            else
            {
                RootFrame.Navigate(new Uri("/View/CadastroUsuario.xaml", UriKind.RelativeOrAbsolute));
            }        
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}
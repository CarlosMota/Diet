﻿#pragma checksum "C:\Users\Carlos\Documents\GitHub\Dieta\Dieta\View\TabelaNutricional.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8EDCE51CF525B26695ED935315FE8FA7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Dieta.View {
    
    
    public partial class TabelaNutricional : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock ValorEnergetico;
        
        internal System.Windows.Controls.TextBlock Carboidratos;
        
        internal System.Windows.Controls.TextBlock Proteinas;
        
        internal System.Windows.Controls.TextBlock GordurasTotais;
        
        internal System.Windows.Controls.TextBlock GordurasSaturadas;
        
        internal System.Windows.Controls.TextBlock GorduraTrans;
        
        internal System.Windows.Controls.TextBlock FibraAlimentar;
        
        internal System.Windows.Controls.TextBlock Sodio;
        
        internal System.Windows.Controls.TextBlock nomeDoAlimento;
        
        internal System.Windows.Controls.TextBox tBoxGramas;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Dieta;component/View/TabelaNutricional.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.ValorEnergetico = ((System.Windows.Controls.TextBlock)(this.FindName("ValorEnergetico")));
            this.Carboidratos = ((System.Windows.Controls.TextBlock)(this.FindName("Carboidratos")));
            this.Proteinas = ((System.Windows.Controls.TextBlock)(this.FindName("Proteinas")));
            this.GordurasTotais = ((System.Windows.Controls.TextBlock)(this.FindName("GordurasTotais")));
            this.GordurasSaturadas = ((System.Windows.Controls.TextBlock)(this.FindName("GordurasSaturadas")));
            this.GorduraTrans = ((System.Windows.Controls.TextBlock)(this.FindName("GorduraTrans")));
            this.FibraAlimentar = ((System.Windows.Controls.TextBlock)(this.FindName("FibraAlimentar")));
            this.Sodio = ((System.Windows.Controls.TextBlock)(this.FindName("Sodio")));
            this.nomeDoAlimento = ((System.Windows.Controls.TextBlock)(this.FindName("nomeDoAlimento")));
            this.tBoxGramas = ((System.Windows.Controls.TextBox)(this.FindName("tBoxGramas")));
        }
    }
}


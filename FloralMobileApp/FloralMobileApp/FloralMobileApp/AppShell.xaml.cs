using FloralMobileApp.ViewModels;
using FloralMobileApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace FloralMobileApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        #region Colors for MenuItems

        private Color expenseColor;
        private Color purchaseColor;
        private Color plansColor;
        private Color globalPlansColor;
        private Color diaryColor;
        private Color cameraColor;
        private Color postColor;
        private Color aboutColor;

        public Color ExpenseColor
        {
            get { return expenseColor; }
            set
            {
                expenseColor = value;
                OnPropertyChanged("ExpenseColor");
            }
        }
        public Color PurchaseColor
        {
            get { return purchaseColor; }
            set
            {
                purchaseColor = value;
                OnPropertyChanged("PurchaseColor");
            }
        }
        public Color PlansColor
        {
            get { return plansColor; }
            set
            {
                plansColor = value;
                OnPropertyChanged("PlansColor");
            }
        }
        public Color GlobalPlansColor
        {
            get { return globalPlansColor; }
            set
            {
                globalPlansColor = value;
                OnPropertyChanged("GlobalPlansColor");
            }
        }
        public Color DiaryColor
        {
            get { return diaryColor; }
            set
            {
                diaryColor = value;
                OnPropertyChanged("DiaryColor");
            }
        }
        public Color CameraColor
        {
            get { return cameraColor; }
            set
            {
                cameraColor = value;
                OnPropertyChanged("CameraColor");
            }
        }
        public Color PostColor
        {
            get { return postColor; }
            set
            {
                postColor = value;
                OnPropertyChanged("PostColor");
            }
        }
        public Color AboutColor
        {
            get { return aboutColor; }
            set
            {
                aboutColor = value;
                OnPropertyChanged("AboutColor");
            }
        }

        private void SetColorsToMain()
        {
            var mainColor = "343a40";
            ExpenseColor = Color.FromHex(mainColor);
            PurchaseColor = Color.FromHex(mainColor);
            PlansColor = Color.FromHex(mainColor);
            GlobalPlansColor = Color.FromHex(mainColor);
            DiaryColor = Color.FromHex(mainColor);
            CameraColor = Color.FromHex(mainColor);
            PostColor = Color.FromHex(mainColor);
            AboutColor = Color.FromHex(mainColor);
        }

        #endregion
        public AppShell()
        {
            SetColorsToMain();
            AboutColor = Color.FromHex("#495057");
            BindingContext = this;
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddEditItemPage), typeof(AddEditItemPage));
            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
            Routing.RegisterRoute(nameof(ExpensesPage), typeof(ExpensesPage));
        }

        private void OnMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                var menuItem = sender as MenuItem;
                SetColorsToMain();
                if (menuItem.Text == "О программе") {
                    AboutColor = Color.FromHex("#495057");
                    Shell.Current.CurrentItem.CurrentItem.Items.Add(new AboutPage());
                }
                else if (menuItem.Text == "Калькулятор расходов") {
                    ExpenseColor = Color.FromHex("#495057");
                    Shell.Current.CurrentItem.CurrentItem.Items.Add(new ExpensesPage()); 
                }
                else if (menuItem.Text == "Покупки")
                {
                    PurchaseColor = Color.FromHex("#495057");
                    Shell.Current.CurrentItem.CurrentItem.Items.Add(new ItemsPage(menuItem.Text));
                }
                else if (menuItem.Text == "Планы")
                {
                    PlansColor = Color.FromHex("#495057");
                    Shell.Current.CurrentItem.CurrentItem.Items.Add(new ItemsPage(menuItem.Text));
                }
                else if (menuItem.Text == "Глобальные планы")
                {
                    GlobalPlansColor = Color.FromHex("#495057");
                    Shell.Current.CurrentItem.CurrentItem.Items.Add(new ItemsPage(menuItem.Text));
                }
                else if (menuItem.Text == "Дневник")
                {
                    DiaryColor = Color.FromHex("#495057");
                    Shell.Current.CurrentItem.CurrentItem.Items.Add(new ItemsPage(menuItem.Text));
                }
                else if (menuItem.Text == "Идеи для фотографий")
                {
                    CameraColor = Color.FromHex("#495057");
                    Shell.Current.CurrentItem.CurrentItem.Items.Add(new ItemsPage(menuItem.Text));
                }
                else if (menuItem.Text == "Идеи для постов")
                {
                    PostColor = Color.FromHex("#495057");
                    Shell.Current.CurrentItem.CurrentItem.Items.Add(new ItemsPage(menuItem.Text));
                }
                Shell.Current.CurrentItem.CurrentItem.Items.RemoveAt(0);
                Shell.Current.FlyoutIsPresented = false;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}

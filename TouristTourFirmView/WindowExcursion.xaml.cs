﻿using System;
using System.Collections.Generic;
using System.Windows;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.BusinessLogic;
using TourFirmBusinessLogic.ViewModels;
using Unity;

namespace TouristTourFirmView
{
    /// <summary>
    /// Логика взаимодействия для WindowExcursion.xaml
    /// </summary>
    public partial class WindowExcursion : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly ExcursionLogic excursionLogic;
        private readonly PlaceLogic placeLogic;

        public int Id { set { id = value; } }
        private int? id;

        public WindowExcursion(ExcursionLogic excursionLogic, PlaceLogic placeLogic)
        {
            InitializeComponent();
            this.excursionLogic = excursionLogic;
            this.placeLogic = placeLogic;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Введите название экскурсии", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxPrice.Text))
            {
                MessageBox.Show("Выберите стоимость экскурсии", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxDuration.Text))
            {
                MessageBox.Show("Введите продолжительность экскурсии", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ComboBoxPlaces.SelectedItem == null)
            {
                MessageBox.Show("Выберите место проведения эксурсии", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                excursionLogic.CreateOrUpdate(new ExcursionBindingModel
                {
                    ID = id,
                    Name = TextBoxName.Text,
                    Price = Convert.ToDecimal(TextBoxPrice.Text),
                    Duration = Convert.ToInt32(TextBoxDuration.Text),
                    PlaceID = Convert.ToInt32(ComboBoxPlaces.SelectedValue),
                    TouristID = App.Tourist.ID 
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void WindowExcursion_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                List<PlaceViewModel> listPlaces = placeLogic.Read(null);

                if (listPlaces != null)
                {
                    ComboBoxPlaces.ItemsSource = listPlaces;
                    ComboBoxPlaces.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

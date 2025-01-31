﻿using Bolnica.Controller;
using Bolnica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bolnica
{
    /// <summary>
    /// Interaction logic for ReadPatient.xaml
    /// </summary>
    public partial class ReadPatient : Window
    {
        PatientController patientController = new PatientController();
        public string Ids { get; set; }
        public ReadPatient(string id)
        {
            this.Ids = id;
            InitializeComponent();

            Patient patient = patientController.FindById(Ids);

            if (patient == null) { MessageBox.Show("Error: User doesn't exist!"); return; }
            else { 
                Fullname.Text = patient.Name + " " + patient.Surname;
                DateOfBirth.Text = patient.DateOfBirth;
                PhoneNumber.Text = patient.PhoneNumber;
                Email.Text = patient.Email;
                Id.Text = patient.Id;
                Country.Text = patient.city.Name + ", " + patient.city.country.Name;
            }
        }

        private void BackWin(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdatePatient(object sender, RoutedEventArgs e)
        {
            Patient patient = patientController.FindByIdFull(Ids);

            string email = Email.Text.ToString();
            string phoneNumber = PhoneNumber.Text.ToString();

            System.Diagnostics.Debug.WriteLine("Email: " + email + "\nPhone: " + phoneNumber);

            string name = patient.Name;
            string surname = patient.Surname;
            string dateOfBirth = patient.DateOfBirth;
            string id = patient.Id;
            bool active = (bool)patient.Active;
            string username = patient.Username;
            string password = patient.Password;
            City city = patient.city;

            Patient updated = new Patient(name, surname, dateOfBirth, phoneNumber, email, id, active, username, password, city);

            if (patientController.Update(updated))
            {
                MessageBox.Show("Successfully updated the patient.");
            } else
            {
                MessageBox.Show("Error: Updating patient failed.");
                return;
            }

        }


    }
}

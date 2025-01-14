﻿using Bolnica.Controller;
using Bolnica.Model;
using Bolnica.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bolnica.View
{
    /// <summary>
    /// Interaction logic for FourCardsView.xaml
    /// </summary>
    public partial class FourCardsView : Page
    {
        Int64 skrol;
        Int64 korak = 4;
        Int64 temp;
        String eventNaClick = "";
        RoomController room_controller = new RoomController();
        RoomRepository room_repository = new RoomRepository();
        StaticEquipmentController staticEquipment_controller = new StaticEquipmentController();
        StaticEquipmentRepository staticEquipment_repository = new StaticEquipmentRepository();
        DynamicEquipmentController dynamicEquipment_controller = new DynamicEquipmentController();
        CureController cure_controller = new CureController();
        CureRepository cure_repository = new CureRepository();
        public FourCardsView(String oznaka)
        {
            InitializeComponent();

            if (oznaka == "DynamicEquipmentSelected")
            {
                List<DynamicEquipment> equipments = dynamicEquipment_controller.GetAllDynamicEquipments();
                dynamicEquipmentIspis(equipments);
            }
            if (oznaka == "CuresSelected")
            {
                AddNewButton.Content = "Add New Cure";
                AddNewButton.Visibility = Visibility.Visible;
                List<Cure> cures = cure_controller.GetAllCures();
                curePrint(cures);
            }
            skrol = 0;
            temp = skrol * korak;
            List<Room> rooms = room_controller.getAllRooms();
            List<StaticEquipment> staticEquipments = staticEquipment_controller.getAllStaticEquipment();
            if (oznaka == "PrikazSoba")
            {
                room_repository.advancedRenovationMergeSplit();


                AddNewButton.Content = "Add New Room";
                AddNewButton.Visibility = Visibility.Visible;

                eventNaClick = "Room";
                for (Int64 x = 0; x < 4; x++)
                {
                    Room room = new Room();
                    try
                    {
                        Room soba = rooms.ElementAt((int)temp);
                        room.Id = soba.Id; room.Name = soba.Name; room.Floor = soba.Floor; room.Description = soba.Description; room.RoomType = soba.RoomType;

                    }
                    catch (Exception nekaGreska)
                    {
                        room = null;
                    }
                    if (room == null && x == 0) { return; }
                    if (x == 0)
                    {
                        Type1.Text = room.RoomType.ToString(); Id1.Text = room.Id; AdditionInfo1.Text = room.Description;
                    }
                    if (x == 1)
                    {
                        if (room == null) { Type2.Text = ""; Id2.Text = ""; AdditionInfo2.Text = ""; Type3.Text = ""; Id3.Text = ""; AdditionInfo3.Text = ""; Type4.Text = ""; Id4.Text = ""; AdditionInfo4.Text = ""; return; }
                        else
                        {
                            Type2.Text = room.RoomType.ToString(); Id2.Text = room.Id; AdditionInfo2.Text = room.Description;
                        }
                    }
                    if (x == 2)
                    {
                        if (room == null) { Type3.Text = ""; Id3.Text = ""; AdditionInfo3.Text = ""; Type4.Text = ""; Id4.Text = ""; AdditionInfo4.Text = ""; return; }
                        else
                        {
                            Type3.Text = room.RoomType.ToString(); Id3.Text = room.Id; AdditionInfo4.Text = room.Description;
                        }
                    }
                    if (x == 3)
                    {
                        if (room == null) { Type4.Text = ""; Id4.Text = ""; AdditionInfo4.Text = ""; }
                        else
                        {
                            Type4.Text = room.RoomType.ToString(); Id4.Text = room.Id; AdditionInfo4.Text = room.Description;
                        }
                    }
                    temp++;
                }
            }
            if (oznaka == "StaticEquipmentSelected")
            {
                NewSupplyRequestButton.Visibility = Visibility.Visible;

                eventNaClick = "StaticEquipmentInRoom";
                Type1.Text = "All Equipment";
                Id1.Text = "-";
                foreach (StaticEquipment se in staticEquipments) {
                    Room r = room_repository.FindById(se.roomId);
                    AdditionInfo1.Text += " *" + se.Name + " " + r.Name + " " + se.Quantity + "* ";
                }
                for (Int64 x = 0; x < 3; x++)
                {
                    Room room = new Room();
                    try
                    {
                        Room soba = rooms.ElementAt((int)temp);
                        room.Id = soba.Id; room.Name = soba.Name; room.Floor = soba.Floor; room.Description = soba.Description; room.RoomType = soba.RoomType;

                    }
                    catch (Exception nekaGreska)
                    {
                        room = null;
                    }
                    if (room == null && x == 0) { return; }
                    if (x == 0)
                    {
                        if (room == null) { Type2.Text = ""; Id2.Text = ""; AdditionInfo2.Text = ""; Type3.Text = ""; Id3.Text = ""; AdditionInfo3.Text = ""; Type4.Text = ""; Id4.Text = ""; AdditionInfo4.Text = ""; return; }
                        else
                        {
                            Type2.Text = room.RoomType.ToString(); Id2.Text = room.Id;
                            foreach (StaticEquipment se in staticEquipments)
                            {
                                Room r = room_repository.FindById(se.roomId);
                                if (room.Id == se.roomId) { AdditionInfo2.Text += " *" + se.Name + " " + r.Name + " " + se.Quantity + "* "; }
                            }
                        }
                    }
                    if (x == 1)
                    {
                        if (room == null) { Type3.Text = ""; Id3.Text = ""; AdditionInfo3.Text = ""; Type4.Text = ""; Id4.Text = ""; AdditionInfo4.Text = ""; return; }
                        else
                        {
                            Type3.Text = room.RoomType.ToString(); Id3.Text = room.Id;
                            foreach (StaticEquipment se in staticEquipments)
                            {
                                Room r = room_repository.FindById(se.roomId);
                                if (room.Id == se.roomId) { AdditionInfo3.Text += " *" + se.Name + " " + r.Name + " " + se.Quantity + "* "; }
                            }
                        }
                    }
                    if (x == 2)
                    {
                        if (room == null) { Type4.Text = ""; Id4.Text = ""; AdditionInfo4.Text = ""; }
                        else
                        {
                            Type4.Text = room.RoomType.ToString(); Id4.Text = room.Id;
                            foreach (StaticEquipment se in staticEquipments)
                            {
                                Room r = room_repository.FindById(se.roomId);
                                if (room.Id == se.roomId) { AdditionInfo4.Text += " *" + se.Name + " " + r.Name + " " + se.Quantity + "* "; }
                            }
                        }
                    }

                    temp++;
                }
            }
        }
        private void addNew(object sender, RoutedEventArgs e)
        {
            if (eventNaClick == "Room")
            {
                FourCardsViewName.Visibility = Visibility.Hidden;
                MoveEquipmentFrame.Content = new AddNewRoom("AddNewRoom");
            }
            else if(eventNaClick == "cureScroll")
            {
                FourCardsViewName.Visibility = Visibility.Hidden;
                MoveEquipmentFrame.Content = new AddNewRoom("AddNewCure");
            }
        }
        private void Grid_MouseDown1(object sender, MouseButtonEventArgs e)
        {
            String IdSobe = Id1.Text;
            
            if (IdSobe == "") { return; }

            if (eventNaClick == "Room")
            {
                Room room = room_repository.FindById(IdSobe);
                if (room == null) { return; }
                FourCardsViewName.Visibility = Visibility.Hidden;
                SingleRoomFrame.Content = new SingleRoomPage(room);

            }
            if(eventNaClick == "cureScroll")
            {
                int IdCure = Int32.Parse(Id1.Text);
                Cure cure = cure_repository.FindById(IdCure);
                if (cure == null) { return; }
                FourCardsViewName.Visibility = Visibility.Hidden;
                SingleRoomFrame.Content = new SingleCurePage(cure);
            }
            if (eventNaClick == "SingleStaticEquipment")
            {
                FourCardsViewName.Visibility = Visibility.Hidden;
                MoveEquipmentFrame.Content = new MoveEquipmentPage(Int32.Parse(IdSobe));
            }
            if (eventNaClick == "StaticEquipmentInRoom")
            {
                if (IdSobe == "") { return; }

                if (IdSobe == "-")
                {
                    staticEquipmentInAllRoomsFunction();
                }
                else
                {
                    staticEquipmentInRoomFunction(IdSobe);
                }
            }
        }
        private void Grid_MouseDown2(object sender, MouseButtonEventArgs e)
        {
            String IdSobe = Id2.Text;
            if (IdSobe == "") { return; }
            if (eventNaClick == "Room")
            {
                Room room = room_repository.FindById(IdSobe);
                if (room == null) { return; }
                FourCardsViewName.Visibility = Visibility.Hidden;
                SingleRoomFrame.Content = new SingleRoomPage(room);

            }
            if (eventNaClick == "cureScroll")
            {
                int IdCure = Int32.Parse(Id2.Text);
                Cure cure = cure_repository.FindById(IdCure);
                if (cure == null) { return; }
                FourCardsViewName.Visibility = Visibility.Hidden;
                SingleRoomFrame.Content = new SingleCurePage(cure);
            }
            if (eventNaClick == "SingleStaticEquipment")
            {
                FourCardsViewName.Visibility = Visibility.Hidden;
                MoveEquipmentFrame.Content = new MoveEquipmentPage(Int32.Parse(IdSobe));
            }

            if (eventNaClick == "StaticEquipmentInRoom")
            {
                staticEquipmentInRoomFunction(IdSobe);
            }
        }
        private void Grid_MouseDown3(object sender, MouseButtonEventArgs e)
        {
            String IdSobe = Id3.Text;
            if (IdSobe == "") { return; }

            if (eventNaClick == "Room")
            {
                Room room = room_repository.FindById(IdSobe);
                if (room == null) { return; }
                FourCardsViewName.Visibility = Visibility.Hidden;
                SingleRoomFrame.Content = new SingleRoomPage(room);

            }
            if (eventNaClick == "cureScroll")
            {
                int IdCure = Int32.Parse(Id3.Text);
                Cure cure = cure_repository.FindById(IdCure);
                if (cure == null) { return; }
                FourCardsViewName.Visibility = Visibility.Hidden;
                SingleRoomFrame.Content = new SingleCurePage(cure);
            }
            if (eventNaClick == "SingleStaticEquipment")
            {
                FourCardsViewName.Visibility = Visibility.Hidden;
                MoveEquipmentFrame.Content = new MoveEquipmentPage(Int32.Parse(IdSobe));
            }
            if (eventNaClick == "StaticEquipmentInRoom")
            {
                staticEquipmentInRoomFunction(IdSobe);
            }
        }
        private void Grid_MouseDown4(object sender, MouseButtonEventArgs e)
        {
            String IdSobe = Id4.Text;
            if (IdSobe == "") { return; }
            if (eventNaClick == "Room")
            {
                Room room = room_repository.FindById(IdSobe);
                if (room == null) { return; }
                FourCardsViewName.Visibility = Visibility.Hidden;
                SingleRoomFrame.Content = new SingleRoomPage(room);

            }
            if (eventNaClick == "cureScroll")
            {
                int IdCure = Int32.Parse(Id4.Text);
                Cure cure = cure_repository.FindById(IdCure);
                if (cure == null) { return; }
                FourCardsViewName.Visibility = Visibility.Hidden;
                SingleRoomFrame.Content = new SingleCurePage(cure);
            }
            if (eventNaClick == "SingleStaticEquipment")
            {
                FourCardsViewName.Visibility = Visibility.Hidden;
                MoveEquipmentFrame.Content = new MoveEquipmentPage(Int32.Parse(IdSobe));
            }
            if (eventNaClick == "StaticEquipmentInRoom")
            {
                staticEquipmentInRoomFunction(IdSobe);
            }
        }
        public void staticEquipmentInAllRoomsFunction()
        {
            staticOpremaUOdabranojSobi.Clear();

            NewSupplyRequestButton.Visibility = Visibility.Hidden;
            CurrentRoomNameEq.Visibility = Visibility.Visible;
            CurrentRoomIdEq.Visibility = Visibility.Visible;
            searchName.Visibility = Visibility.Visible;

            CurrentRoomNameEq.Text = "All Equipment";
            CurrentRoomIdEq.Text = "-";

            eventNaClick = "SingleStaticEquipment";
            skrol = 0;
            temp = skrol * korak;
            List<StaticEquipment> equipments = staticEquipment_controller.getAllStaticEquipment();
            for (Int64 x = 0; x < equipments.Count; x++)
            {
                StaticEquipment equipment = new StaticEquipment();
                try
                {
                    StaticEquipment oprema = equipments.ElementAt((int)temp);
                    staticOpremaUOdabranojSobi.Add(oprema);
                    equipment.Id = oprema.Id; equipment.Name = oprema.Name; equipment.Quantity = oprema.Quantity; equipment.roomId = oprema.roomId;
                }
                catch (Exception nekaGreska)
                {
                    equipment = null;
                }
                if (equipment == null && x == 0) { return; }
                if (x == 0)
                {
                    Type1.Text = equipment.Name.ToString(); Id1.Text = equipment.Id.ToString(); AdditionInfo1.Text = "Available Quantity: " + equipment.Quantity.ToString();
                }
                if (x == 1)
                {
                    if (equipment == null) { Type2.Text = ""; Id2.Text = ""; AdditionInfo2.Text = ""; Type3.Text = ""; Id3.Text = ""; AdditionInfo3.Text = ""; Type4.Text = ""; Id4.Text = ""; AdditionInfo4.Text = ""; return; }
                    else
                    {
                        Type2.Text = equipment.Name.ToString(); Id2.Text = equipment.Id.ToString(); AdditionInfo2.Text = "Available Quantity: " + equipment.Quantity.ToString();
                    }
                }
                if (x == 2)
                {
                    if (equipment == null) { Type3.Text = ""; Id3.Text = ""; AdditionInfo3.Text = ""; Type4.Text = ""; Id4.Text = ""; AdditionInfo4.Text = ""; return; }
                    else
                    {
                        Type3.Text = equipment.Name.ToString(); Id3.Text = equipment.Id.ToString(); AdditionInfo3.Text = "Available Quantity: " + equipment.Quantity.ToString();
                    }
                }
                if (x == 3)
                {
                    if (equipment == null) { Type4.Text = ""; Id4.Text = ""; AdditionInfo4.Text = ""; }
                    else
                    {
                        Type4.Text = equipment.Name.ToString(); Id4.Text = equipment.Id.ToString(); AdditionInfo4.Text = "Available Quantity: " + equipment.Quantity.ToString();
                    }
                }
                temp++;
            }
            temp = 4;
        }
        List<StaticEquipment> staticOpremaUOdabranojSobi = new List<StaticEquipment>();
        public void staticEquipmentInRoomFunction(String IdSobe)
        {
            staticOpremaUOdabranojSobi.Clear();
            NewSupplyRequestButton.Visibility = Visibility.Hidden;

            CurrentRoomNameEq.Visibility = Visibility.Visible;
            CurrentRoomIdEq.Visibility = Visibility.Visible;
            searchName.Visibility = Visibility.Visible;
            Room RoomNameId = room_repository.FindById(IdSobe);
            CurrentRoomNameEq.Text = RoomNameId.RoomType.ToString() + " " + RoomNameId.Name;
            CurrentRoomIdEq.Text = RoomNameId.Id;


            eventNaClick = "SingleStaticEquipment";
            skrol = 0;
            List<StaticEquipment> equipments = staticEquipment_controller.getAllStaticEquipment();
            temp = 0;
            for (Int64 x = 0; x < equipments.Count; x++) // idi do kraja liste
            {
                StaticEquipment equipment = new StaticEquipment();
                StaticEquipment oprema = equipments.ElementAt((int)x);
                if (oprema.roomId == IdSobe)
                {
                    equipment.Id = oprema.Id; equipment.Name = oprema.Name; equipment.Quantity = oprema.Quantity; equipment.roomId = oprema.roomId;
                    if (temp == 0)
                    {
                        Type1.Text = equipment.Name.ToString(); Id1.Text = equipment.Id.ToString(); AdditionInfo1.Text = "Available Quantity: " + equipment.Quantity.ToString();
                    }
                    if (temp == 1)
                    {
                        Type2.Text = equipment.Name.ToString(); Id2.Text = equipment.Id.ToString(); AdditionInfo2.Text = "Available Quantity: " + equipment.Quantity.ToString();
                    }
                    if (temp == 2)
                    {
                        Type3.Text = equipment.Name.ToString(); Id3.Text = equipment.Id.ToString(); AdditionInfo3.Text = "Available Quantity: " + equipment.Quantity.ToString();
                    }
                    if (temp == 3)
                    {
                        Type4.Text = equipment.Name.ToString(); Id4.Text = equipment.Id.ToString(); AdditionInfo4.Text = "Available Quantity: " + equipment.Quantity.ToString();
                        return;
                    }
                    staticOpremaUOdabranojSobi.Add(oprema);
                    temp++;
                }
            }
            EmptyPrintChecker(temp);
        }
        public void dynamicEquipmentIspis(List<DynamicEquipment> equipments)
        {
            eventNaClick = "dynamicEquipmentScroll";
            skrol = 0;
            searchName.Visibility = Visibility.Visible;
            temp = 0;
            temp = PrintNameIdQuantity(null, equipments, null, temp);
            EmptyPrintChecker(temp);
        }
        public void curePrint(List<Cure> cures)
        {
            eventNaClick = "cureScroll";
            skrol = 0;
            temp = 0;

           temp = PrintNameIdQuantity(null,null,cures,temp);

           if(temp == 4) { return; }
            EmptyPrintChecker(temp);
        }
    
        public void PrintSearchStaticEquipment(List<StaticEquipment> equipments)
        {
            eventNaClick = "SingleStaticEquipment";
            skrol = 0;
            temp = 0;
            for (Int64 x = 0; x < equipments.Count; x++) // idi do kraja liste
            {
                StaticEquipment equipment = new StaticEquipment();
                StaticEquipment oprema = equipments.ElementAt((int)x);
                {
                    equipment.Id = oprema.Id; equipment.Name = oprema.Name; equipment.Quantity = oprema.Quantity;
                    if (temp == 0)
                    {
                        Type1.Text = equipment.Name.ToString(); Id1.Text = equipment.Id.ToString(); AdditionInfo1.Text = "Available Quantity: " + equipment.Quantity.ToString();
                    }
                    if (temp == 1)
                    {
                        Type2.Text = equipment.Name.ToString(); Id2.Text = equipment.Id.ToString(); AdditionInfo2.Text = "Available Quantity: " + equipment.Quantity.ToString();
                    }
                    if (temp == 2)
                    {
                        Type3.Text = equipment.Name.ToString(); Id3.Text = equipment.Id.ToString(); AdditionInfo3.Text = "Available Quantity: " + equipment.Quantity.ToString();
                    }
                    if (temp == 3)
                    {
                        Type4.Text = equipment.Name.ToString(); Id4.Text = equipment.Id.ToString(); AdditionInfo4.Text = "Available Quantity: " + equipment.Quantity.ToString();
                        return;
                    }
                    temp++;
                }
            }
            EmptyPrintChecker(temp);
        }
        public long PrintNameIdQuantity(List<StaticEquipment> staticEquipments, List<DynamicEquipment> dynamicEquipments, List<Cure> cures, long temp)
        {
            List<StaticEquipment> equipments = new List<StaticEquipment>();
            if(staticEquipments != null) { equipments.AddRange(staticEquipments); }
            if (dynamicEquipments != null) { foreach (DynamicEquipment de in dynamicEquipments) { equipments.Add(new StaticEquipment(de.Id, de.Name, de.Quantity)); } }
            if (cures != null) {
                foreach(Cure c in cures) { equipments.Add(new StaticEquipment(c.Id,c.Name, c.Quantity)); }
            }

            for (Int64 x = 0; x < equipments.Count; x++) // idi do kraja liste
            {
                StaticEquipment equipment = equipments.ElementAt((int)x);
                if (temp == 0)
                {
                    Type1.Text = equipment.Name; Id1.Text = equipment.Id.ToString(); AdditionInfo1.Text = "Available Quantity: " + equipment.Quantity.ToString();
                }
                if (temp == 1)
                {
                    Type2.Text = equipment.Name.ToString(); Id2.Text = equipment.Id.ToString(); AdditionInfo2.Text = "Available Quantity: " + equipment.Quantity.ToString();
                }
                if (temp == 2)
                {
                    Type3.Text = equipment.Name.ToString(); Id3.Text = equipment.Id.ToString(); AdditionInfo3.Text = "Available Quantity: " + equipment.Quantity.ToString();
                }
                if (temp == 3)
                {
                    Type4.Text = equipment.Name.ToString(); Id4.Text = equipment.Id.ToString(); AdditionInfo4.Text = "Available Quantity: " + equipment.Quantity.ToString();
                    //return;
                }
                temp++;
            }
            //temp = 4;
            return temp;
            
        }
        public void EmptyPrintChecker(long temp, long val = 4) {
            if (temp == 0)
            {
                Type1.Text = ""; Id1.Text = ""; AdditionInfo1.Text = ""; Type2.Text = ""; Id2.Text = ""; AdditionInfo2.Text = ""; Type3.Text = ""; Id3.Text = ""; AdditionInfo3.Text = ""; Type4.Text = ""; Id4.Text = ""; AdditionInfo4.Text = ""; return;
            }
            if (temp == 1)
            {
                Type2.Text = ""; Id2.Text = ""; AdditionInfo2.Text = ""; Type3.Text = ""; Id3.Text = ""; AdditionInfo3.Text = ""; Type4.Text = ""; Id4.Text = ""; AdditionInfo4.Text = ""; return;
            }
            if (temp == 2)
            {
                Type3.Text = ""; Id3.Text = ""; AdditionInfo3.Text = ""; Type4.Text = ""; Id4.Text = ""; AdditionInfo4.Text = ""; return;
            }
            if (temp == 3)
            {
                Type4.Text = ""; Id4.Text = ""; AdditionInfo4.Text = ""; return;
            }
            temp = val;
        }
        private void next(object sender, RoutedEventArgs e)
        {
           if(eventNaClick == "Room")
            {
                if (temp < 4)
                    temp = 4;
                List<Room> rooms = room_controller.getAllRooms();
                Room room = new Room();
                try
                {
                    Room soba = rooms.ElementAt((int)temp);
                    room.Id = soba.Id; room.Name = soba.Name; room.Floor = soba.Floor; room.Description = soba.Description; room.RoomType = soba.RoomType;
                    //Ako nije otisao u exception znaci da ima element iza i treba sve da zamenimo
                    Type1.Text = Type2.Text; Id1.Text = Id2.Text; AdditionInfo1.Text = AdditionInfo2.Text;
                    Type2.Text = Type3.Text; Id2.Text = Id3.Text; AdditionInfo2.Text = AdditionInfo3.Text;
                    Type3.Text = Type4.Text; Id3.Text = Id4.Text; AdditionInfo3.Text = AdditionInfo4.Text;

                    Type4.Text = room.RoomType.ToString(); Id4.Text = room.Id; AdditionInfo4.Text = room.Description;
                    if (ButtonPreviousName.Visibility == Visibility.Hidden) { ButtonPreviousName.Visibility = Visibility.Visible; }
                    temp++;
                }
                catch (Exception nekaGreska)
                {
                    room = null;
                    ButtonNextName.Visibility = Visibility.Hidden;
                    return;
                }
            }
            if (eventNaClick == "StaticEquipmentInRoom")
            {
                
                List<StaticEquipment> staticEquipments =  staticEquipment_controller.getAllStaticEquipment();
                List<Room> rooms = room_controller.getAllRooms();
                try
                {
                    Room room = rooms.ElementAt((int)temp);
                    //Ako nije otisao u exception znaci da ima element iza i treba sve da zamenimo
                    Type1.Text = Type2.Text; Id1.Text = Id2.Text; AdditionInfo1.Text = AdditionInfo2.Text;
                    Type2.Text = Type3.Text; Id2.Text = Id3.Text; AdditionInfo2.Text = AdditionInfo3.Text;
                    Type3.Text = Type4.Text; Id3.Text = Id4.Text; AdditionInfo3.Text = AdditionInfo4.Text;

                    Type4.Text = room.RoomType.ToString(); Id4.Text = room.Id;
                    AdditionInfo4.Text = "";
                    foreach (StaticEquipment se in staticEquipments)
                    {
                        Room r = room_repository.FindById(se.roomId);
                        if (room.Id == se.roomId) { AdditionInfo4.Text += " *" + se.Name + " " + r.Name + " " + se.Quantity + "* "; }
                    }
                    if (ButtonPreviousName.Visibility == Visibility.Hidden) { ButtonPreviousName.Visibility = Visibility.Visible; }
                    temp++;
                }
                catch (Exception nekaGreska)
                {
                    ButtonNextName.Visibility = Visibility.Hidden;
                    return;
                }
            }
            if ( eventNaClick == "dynamicEquipmentScroll")
            {
                if(temp < 4)
                    temp = 4;
                List<DynamicEquipment> staticEquipments = dynamicEquipment_controller.GetAllDynamicEquipments();

                try
                {
                    DynamicEquipment staticEquipment = staticEquipments.ElementAt((int)temp);
                    //Ako nije otisao u exception znaci da ima element iza i treba sve da zamenimo
                    Type1.Text = Type2.Text; Id1.Text = Id2.Text; AdditionInfo1.Text = AdditionInfo2.Text;
                    Type2.Text = Type3.Text; Id2.Text = Id3.Text; AdditionInfo2.Text = AdditionInfo3.Text;
                    Type3.Text = Type4.Text; Id3.Text = Id4.Text; AdditionInfo3.Text = AdditionInfo4.Text;

                    Type4.Text = staticEquipment.Name.ToString(); Id4.Text = staticEquipment.Id.ToString(); AdditionInfo4.Text = "Available Quantity: " + staticEquipment.Quantity.ToString();
                    if (ButtonPreviousName.Visibility == Visibility.Hidden) { ButtonPreviousName.Visibility = Visibility.Visible; }
                    temp++;
                }
                catch (Exception nekaGreska)
                {
                    ButtonNextName.Visibility = Visibility.Hidden;
                    return;
                }
            }
            if (eventNaClick == "cureScroll")
            {
                if (temp < 4)
                    temp = 4;
                List<Cure> cures = cure_controller.GetAllCures();

                try
                {
                    Cure cure = cures.ElementAt((int)temp);
                    //Ako nije otisao u exception znaci da ima element iza i treba sve da zamenimo
                    Type1.Text = Type2.Text; Id1.Text = Id2.Text; AdditionInfo1.Text = AdditionInfo2.Text;
                    Type2.Text = Type3.Text; Id2.Text = Id3.Text; AdditionInfo2.Text = AdditionInfo3.Text;
                    Type3.Text = Type4.Text; Id3.Text = Id4.Text; AdditionInfo3.Text = AdditionInfo4.Text;

                    Type4.Text = cure.Name.ToString(); Id4.Text = cure.Id.ToString(); AdditionInfo4.Text = "Available Quantity: " + cure.Quantity.ToString();
                    if (ButtonPreviousName.Visibility == Visibility.Hidden) { ButtonPreviousName.Visibility = Visibility.Visible; }
                    temp++;
                }
                catch (Exception nekaGreska)
                {
                    ButtonNextName.Visibility = Visibility.Hidden;
                    return;
                }
            }
            if (eventNaClick == "SingleStaticEquipment")
            {
                if (temp < 4)
                    temp = 4;
                try
                {
                    StaticEquipment staticEquipment = staticOpremaUOdabranojSobi.ElementAt((int)temp);
                    //Ako nije otisao u exception znaci da ima element iza i treba sve da zamenimo
                    Type1.Text = Type2.Text; Id1.Text = Id2.Text; AdditionInfo1.Text = AdditionInfo2.Text;
                    Type2.Text = Type3.Text; Id2.Text = Id3.Text; AdditionInfo2.Text = AdditionInfo3.Text;
                    Type3.Text = Type4.Text; Id3.Text = Id4.Text; AdditionInfo3.Text = AdditionInfo4.Text;

                    Type4.Text = staticEquipment.Name.ToString(); Id4.Text = staticEquipment.Id.ToString(); AdditionInfo4.Text = "Available Quantity: " + staticEquipment.Quantity.ToString();
                    if (ButtonPreviousName.Visibility == Visibility.Hidden) { ButtonPreviousName.Visibility = Visibility.Visible; }
                    temp++;
                }
                catch (Exception nekaGreska)
                {
                    ButtonNextName.Visibility = Visibility.Hidden;
                    return;
                }
            }
            
        }
        private void previous(object sender, RoutedEventArgs e)
        {
            if (eventNaClick == "Room")
            {
                List<Room> rooms = room_controller.getAllRooms();
                Room room = new Room();
                try
                {
                    Room soba = rooms.ElementAt((int)(temp - 5));
                    room.Id = soba.Id; room.Name = soba.Name; room.Floor = soba.Floor; room.Description = soba.Description; room.RoomType = soba.RoomType;
                    //Ako nije otisao u exception znaci da ima element iza i treba sve da zamenimo
                    Type4.Text = Type3.Text; Id4.Text = Id3.Text; AdditionInfo4.Text = AdditionInfo3.Text;
                    Type3.Text = Type2.Text; Id3.Text = Id2.Text; AdditionInfo3.Text = AdditionInfo2.Text;
                    Type2.Text = Type1.Text; Id2.Text = Id1.Text; AdditionInfo2.Text = AdditionInfo1.Text;

                    Type1.Text = room.RoomType.ToString(); Id1.Text = room.Id; AdditionInfo1.Text = room.Description;
                    temp--;
                    if (ButtonNextName.Visibility == Visibility.Hidden) { ButtonNextName.Visibility = Visibility.Visible; }
                }
                catch (Exception nekaGreska)
                {
                    ButtonPreviousName.Visibility = Visibility.Hidden;
                    return;
                }
            }
            
            if (eventNaClick == "StaticEquipmentInRoom")
            {
                List<StaticEquipment> staticEquipments = staticEquipment_controller.getAllStaticEquipment();
                if(Id1.Text.ToString() == "1") { // Poseban slucaj
                    Type4.Text = Type3.Text; Id4.Text = Id3.Text; AdditionInfo4.Text = AdditionInfo3.Text;
                    Type3.Text = Type2.Text; Id3.Text = Id2.Text; AdditionInfo3.Text = AdditionInfo2.Text;
                    Type2.Text = Type1.Text; Id2.Text = Id1.Text; AdditionInfo2.Text = AdditionInfo1.Text;
                    Type1.Text = "All Rooms"; Id1.Text = "-";
                    AdditionInfo1.Text = "";
                    foreach (StaticEquipment se in staticEquipments)
                    {
                        Room r = room_repository.FindById(se.roomId);
                        { AdditionInfo1.Text += " *" + se.Name + " " + r.Name + " " + se.Quantity + "* "; }
                    }
                    temp--; //?
                }
                List<Room> rooms = room_controller.getAllRooms();
                try
                {
                    Room room = rooms.ElementAt((int)(temp - 5));
                    
                    //Ako nije otisao u exception znaci da ima element iza i treba sve da zamenimo
                    Type4.Text = Type3.Text; Id4.Text = Id3.Text; AdditionInfo4.Text = AdditionInfo3.Text;
                    Type3.Text = Type2.Text; Id3.Text = Id2.Text; AdditionInfo3.Text = AdditionInfo2.Text;
                    Type2.Text = Type1.Text; Id2.Text = Id1.Text; AdditionInfo2.Text = AdditionInfo1.Text;

                    Type1.Text = room.RoomType.ToString(); Id1.Text = room.Id;
                    AdditionInfo1.Text = "";
                    foreach (StaticEquipment se in staticEquipments)
                    {
                        Room r = room_repository.FindById(se.roomId);
                        if (room.Id == se.roomId) { AdditionInfo1.Text += " *" + se.Name + " " + r.Name + " " + se.Quantity + "* "; }
                    }
                    temp--;
                    if (ButtonNextName.Visibility == Visibility.Hidden) { ButtonNextName.Visibility = Visibility.Visible; }
                }
                catch (Exception nekaGreska)
                {
                    ButtonPreviousName.Visibility = Visibility.Hidden;
                    return;
                }
            }

            if (eventNaClick == "dynamicEquipmentScroll")
            {
                List<DynamicEquipment> staticEquipments = dynamicEquipment_controller.GetAllDynamicEquipments();
                try
                {
                    DynamicEquipment staticEquipment = staticEquipments.ElementAt((int)(temp - 5));
                    //Ako nije otisao u exception znaci da ima element iza i treba sve da zamenimo
                    Type4.Text = Type3.Text; Id4.Text = Id3.Text; AdditionInfo4.Text = AdditionInfo3.Text;
                    Type3.Text = Type2.Text; Id3.Text = Id2.Text; AdditionInfo3.Text = AdditionInfo2.Text;
                    Type2.Text = Type1.Text; Id2.Text = Id1.Text; AdditionInfo2.Text = AdditionInfo1.Text;

                    Type1.Text = staticEquipment.Name.ToString(); Id1.Text = staticEquipment.Id.ToString(); AdditionInfo1.Text = "Available Quantity: " + staticEquipment.Quantity.ToString();
                    temp--;
                    if (ButtonNextName.Visibility == Visibility.Hidden) { ButtonNextName.Visibility = Visibility.Visible; }
                }
                catch (Exception nekaGreska)
                {
                    ButtonPreviousName.Visibility = Visibility.Hidden;
                    return;
                }
            }
            if (eventNaClick == "cureScroll")
            {
                List<Cure> cures = cure_controller.GetAllCures();
                try
                {
                    Cure cure = cures.ElementAt((int)(temp - 5));
                    //Ako nije otisao u exception znaci da ima element iza i treba sve da zamenimo
                    Type4.Text = Type3.Text; Id4.Text = Id3.Text; AdditionInfo4.Text = AdditionInfo3.Text;
                    Type3.Text = Type2.Text; Id3.Text = Id2.Text; AdditionInfo3.Text = AdditionInfo2.Text;
                    Type2.Text = Type1.Text; Id2.Text = Id1.Text; AdditionInfo2.Text = AdditionInfo1.Text;

                    Type1.Text = cure.Name.ToString(); Id1.Text = cure.Id.ToString(); AdditionInfo1.Text = "Available Quantity: " + cure.Quantity.ToString();
                    temp--;
                    if (ButtonNextName.Visibility == Visibility.Hidden) { ButtonNextName.Visibility = Visibility.Visible; }
                }
                catch (Exception nekaGreska)
                {
                    ButtonPreviousName.Visibility = Visibility.Hidden;
                    return;
                }
            }
            if (eventNaClick == "SingleStaticEquipment")
            {
                
                try
                {
                    StaticEquipment staticEquipment = staticOpremaUOdabranojSobi.ElementAt((int)(temp - 5));
                    //Ako nije otisao u exception znaci da ima element iza i treba sve da zamenimo
                    Type4.Text = Type3.Text; Id4.Text = Id3.Text; AdditionInfo4.Text = AdditionInfo3.Text;
                    Type3.Text = Type2.Text; Id3.Text = Id2.Text; AdditionInfo3.Text = AdditionInfo2.Text;
                    Type2.Text = Type1.Text; Id2.Text = Id1.Text; AdditionInfo2.Text = AdditionInfo1.Text;

                    Type1.Text = staticEquipment.Name.ToString(); Id1.Text = staticEquipment.Id.ToString(); AdditionInfo1.Text = "Available Quantity: " + staticEquipment.Quantity.ToString();
                    temp--;
                    if (ButtonNextName.Visibility == Visibility.Hidden) { ButtonNextName.Visibility = Visibility.Visible; }
                }
                catch (Exception nekaGreska)
                {
                    ButtonPreviousName.Visibility = Visibility.Hidden;
                    return;
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (eventNaClick == "dynamicEquipmentScroll")
            {
                List<DynamicEquipment> dynamicEquipments = dynamicEquipment_controller.search(searchName.Text);
                dynamicEquipmentIspis(dynamicEquipments);
            }
            if (eventNaClick == "SingleStaticEquipment")
            {
                List<StaticEquipment> staticEquipments = staticEquipment_controller.search(searchName.Text, CurrentRoomIdEq.Text);
                PrintSearchStaticEquipment(staticEquipments);
            }

        }
    }
}

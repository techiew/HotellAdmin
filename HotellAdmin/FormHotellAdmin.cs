﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// FormHotellAdmin.cs - Håndtere visning av data
// OrderData.cs - Håndtere henting av bestillingsdata
// RoomData.cs - Håndtere henting av romdata
// DatabaseManager.cs - Håndtere funksjoner for SQL kommandoer, og åpne/lukke DB tilkobling
// Både RoomData og OrderData kan bruke DatabaseManager.cs

namespace HotellAdmin {

	public partial class FormHotellAdmin : Form {

		int floors = 4;
		int roomsPerFloor = 11; // sett til # av labels i rutene?
		int selectedFloor = 1;
		List<Room> roomDataList;
		List<Order> orderDataList;

		OrderData od = new OrderData();
		RoomData rd = new RoomData();

		public FormHotellAdmin() {
			InitializeComponent();
		}

		private void FormHotellAdmin_Load(object sender, EventArgs e) {
			// RequestLogin(); // Logge inn for å bruke programmet?
			OpenDatabase();
			GetRoomData();
			ShowRoomData(1);
			GetOrderData();
			ShowOrderData();
			// ShowOrderSchema(); // Finn ut hvordan lasse vil ha det, manuelt eller automatisk laget skjema?

			// Disse stopper ekstrem lag og CPU usage når vi resizer
			ResizeBegin += new EventHandler(FormHotellAdmin_ResizeBegin);
			ResizeEnd += new EventHandler(FormHotellAdmin_ResizeEnd);

			foreach (Control c in tableLayoutRoomsPanel.Controls) {
				// Kan lage mouse event handlers for labels for rommene her
			}

		}

		private void OpenDatabase() {
			//string db = @"server=46.9.246.190;database=hotell;port=24440;userid=admin;password=admin;";
			DatabaseManager.Open("46.9.246.190", "24440", "hotell", "admin", "admin");
		}

		private void GetRoomData() {
			// TODO
			// Hent data om alle rom fra en class som heter "RoomData"
			// List<Room> roomData = RoomData.GetData(); --- Sånn ca. det burde se ut
			// RoomData sin GetData funksjon må returnere en liste over rom
			roomDataList = rd.GetData();

			if (roomDataList.Count != (floors * roomsPerFloor)) {
				ShowError("Feil med romdata."); // midlertidig feilhåndtering, burde endres?
			} 

		}

		private void ShowRoomData(int floor) {
			// TODO
			for (int i = 0; i < roomDataList.Count; i++) {
				Console.WriteLine(roomDataList[i].number);
			}

		}

		private void GetOrderData() {
            OrderData OrderData = new OrderData();
            orderDataList = OrderData.ReadTable();
        }

		private void ShowOrderData() {

            for (int i = 0; i <= roomDataList.Count; i++) {
                string fornavn = orderDataList[i].firstName;
                string etternavn = orderDataList[i].lastName;
                string romType = orderDataList[i].roomType;
                string bestilling = etternavn + ", " + fornavn + " - " + romType;
                listBoxOrders.Items.Add(bestilling);
             //   Console.WriteLine(bestilling);    bare en liten sjekker boi
            }

        }

		private void MakeNewOrder() {
			// TODO
			// OrderData.MakeOrder(parameters); ELLER
			// Legg funksjonen over i en action handler for skjemaet
		}

		private void SyncOnDatabaseUpdate() {
			// TODO ?
			// Sync XML med database når en oppdatering skjer fra programmet vårt
			// Kanskje bruke DatabaseManager til å synce med xml?
		}

		private void ShowError(string errorMsg) {
			// TODO
		}

		private void FormHotellAdmin_ResizeBegin(Object sender, EventArgs e) {
			SuspendLayout();
		}

		private void FormHotellAdmin_ResizeEnd(Object sender, EventArgs e) {
			ResumeLayout();
		}

		~FormHotellAdmin() {
			DatabaseManager.Close();
		}

	}

}

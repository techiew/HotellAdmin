﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellAdmin {

	class Room {

		public int number;
		public string type;
		public bool assigned;

		public Room(int number, string type, bool assigned) {
			this.number = number;
			this.type = type;
			this.assigned = false;
		}

	}

}

using System;
using System.Collections.Generic;
using System.Text;

namespace Backend
{
 
// <id>1215</id>
//<name>Luhtimäki</name>
//<x>24.8201851</x>
//<y>60.2689544</y>
//<bikesAvailable>9</bikesAvailable>
//<spacesAvailable>1</spacesAvailable>
//<allowDropoff>true</allowDropoff>
//<isFloatingBike>false</isFloatingBike>
//<isCarStation>false</isCarStation>
//<state>Station on</state>
   
    class BikeRentalStationList
    {

        public List<StationData> stations = new List<StationData> ( ); 

    }

    class StationData
    {
        public int id { get; set; }
        public string name { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public int bikesAvailable { get; set; }
        public int spacesAvailable { get; set; }
        public bool allowDropoff { get; set; }
        public bool isFloatingBike { get; set; }
        public bool isCarStation { get; set; }
        public string state { get; set; }
        public List<string> networks = new List<string> ( );
        public bool realTimeData { get; set; }
    }
}

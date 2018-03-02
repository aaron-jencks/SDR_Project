using System;
using System.Collections.Generic;
using System.Text;
using TransmissionLibrary;

namespace CommunicationLibrary
{
    public class RadioStation
    {
        private double frequency;
        private RadioStationType type;
        private static long stationCount = 0;
        private long id;

        public RadioStation(double frequency, RadioStationType type = RadioStationType.AM)
        {
            this.frequency = frequency;
            this.type = type;
            id = stationCount++;
        }

        public double Frequency { get => frequency; set => frequency = value; }
        public RadioStationType Type { get => type; set => type = value; }
        public long ID { get => id; set => id = value; }
    }

    public enum RadioStationType { narrowFM = 0, AM };

    public class RedPitayaObj
    {
        // Properties
        private PitayaComm comm;
        private List<RadioStation> bookmarks;
        private PitayaTx TransmitMode;

        // Constructor
        public RedPitayaObj(PitayaTx TransmissionMode)
        {
            TransmitMode = TransmissionMode;
            comm = new PitayaComm();
            bookmarks = new List<RadioStation>(10);
        }

        // Accessors
        public List<RadioStation> Bookmarks { get => bookmarks; set => bookmarks = value; }

        // Public Methods
        public long AddBookmark(double station, RadioStationType type = RadioStationType.AM)
        {
            return AddBookmark(new RadioStation(station, type));
        }

        public long AddBookmark(RadioStation station)
        {
            Bookmarks.Add(station);
            return station.ID;
        }

        public bool RemoveBookmark(long id)
        {
            bool temp = false;
            int index = 0;
            foreach (RadioStation station in bookmarks)
            {
                if (station.ID == id)
                {
                    bookmarks.RemoveAt(index);
                    temp = true;
                    break;
                }
                index++;
            }
            return temp;
        }

        public void SetFrequency(double freq)
        {
            TransmitMode.SetFrequency(freq);
        }
    }
}

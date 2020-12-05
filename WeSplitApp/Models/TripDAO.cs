using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace WeSplitApp.Models
{
    class TripDAO
    {
        private static string ReadFile()
        {
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "data.json";
            string result = File.ReadAllText(filepath);
            return result;
        }
        public static List<Trip> GetAll()
        {
            string jsonString = ReadFile();
            var result = JsonConvert.DeserializeObject<List<Trip>>(jsonString) ?? new List<Trip>();
            return result;
        }

        public static bool Save(List<Trip> trip)
        {
            var result = false;
            try
            {
                string jsonString = JsonConvert.SerializeObject(trip, Formatting.Indented);
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "data.json", jsonString);
                result = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            return result;
        }
        public static bool Insert(Trip trip)
        {
            var result = false;
            var oldID = trip.ID;
            var list = GetAll();
            trip.ID = list.Count == 0 ? 0 : list[list.Count - 1].ID + 1;
            list.Add(trip);
            result = Save(list);
            if (!result)
            {
                trip.ID = oldID;
            }
            return result;
        }

        public static int Delete(int id)
        {
            int result = 0;
            var list = GetAll();
            result = list.RemoveAll(e => e.ID == id);
            Save(list);
            return result;
        }

        public static bool Update(Trip trip)
        {
            var result = false;
            var list = GetAll();
            var oldTripIndex = list.FindIndex(e => e.ID == trip.ID);

            if (oldTripIndex >= 0 && oldTripIndex < list.Count)
            {
                list[oldTripIndex] = trip;
                result = Save(list);
            }

            return result;
        }
    }
}

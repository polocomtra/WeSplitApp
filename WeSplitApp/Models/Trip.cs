using System;
using System.Collections.Generic;

namespace WeSplitApp.Models
{
    class TripExpense
    {
        public string Description { get; set; }
        public double Cost { get; set; }

        public override string ToString()
        {
            return $"-TripExpense: Description: {Description}, Cost: {Cost}-";
        }
    }
    class Member
    {
        public string Name { get; set; }
        public List<TripExpense> Expenses { get; set; }

        public override string ToString()
        {
            return $"-Member: Name: \"{Name}\", ExpensesCount: {Expenses.Count}-";
        }
    }
    class Trip
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public List<string> Images { get; set; }
        public int State { get; set; }

        public List<Member> Members;

        public Trip()
        {
            ID = -1;
            Name = "";
            Place = "";
            Images = new List<string>();
            State = -1;
            Members = new List<Member>();
        }

        public override string ToString()
        {
            return $"-Trip: Name: \"{Name}\", Place: \"{Place}\", ImagesCount: {Images.Count}, State: {State}, MembersCount: {Members.Count}-";
        }
    }
}

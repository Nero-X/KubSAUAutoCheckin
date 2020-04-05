using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace AutoCheckin
{
    public class Schedule
    {
        [JsonProperty("model")]
        public List<Day> Days { get; set; }

        public Day Today => Days.Find(x => x.IsToday);

        public Dictionary<string, List<string>> Dictionary
        {
            get
            {
                var dict = new Dictionary<string, List<string>>();
                foreach (Day day in Days)
                {
                    foreach (Pair pair in day.Pairs)
                    {
                        if(pair.DisciplineName != "")
                        {
                            if (dict.ContainsKey(pair.DisciplineName)) dict[pair.DisciplineName] = dict[pair.DisciplineName].Union(pair.Employees).ToList();
                            else dict[pair.DisciplineName] = pair.Employees;
                        }
                    }
                }
                return dict;
            }
        }

        public static Schedule FromJson(string json) => JsonConvert.DeserializeObject<Schedule>(json);
    }

    public class Pair
    {
        public string DisciplineName { get; set; }
        public string EmployeesAuditoriums { get; set; }
        public int LessonNumber { get; set; }
        private DateTime start;
        private DateTime end;
        public DateTime CheckinDateStart { get => start; set => start = value.ToLocalTime(); }
        public DateTime CheckinDateEnd { get => end; set => end = value.ToLocalTime(); }
        public bool CheckedAtLesson { get; set; }

        public List<string> Employees => EmployeesAuditoriums.Split(',').Select(x => x.Split('(')[0].Trim()).ToList();
    }

    public class Day
    {
        public List<Pair> Pairs { get; set; }
        public bool IsToday { get; set; }

        public Pair GetCurrentPair()
        {
            foreach (Pair pair in Pairs)
            {
                if (pair.CheckinDateStart < DateTime.Now && pair.CheckinDateEnd > DateTime.Now && pair.DisciplineName != "") return pair;
            }
            return null;
        }
    }

    public class UserInfo
    {
        public string FullName { get; set; }
        public string GroupName { get; set; }

        public string Name
        {
            get
            {
                string[] arr = FullName.Split(' ');
                return $"{arr[0]} {arr[1][0]}. {arr[2][0]}. {GroupName}";
            }
        }

        public static UserInfo FromJson(string json) => JsonConvert.DeserializeObject<UserInfo>(json);
    }
}

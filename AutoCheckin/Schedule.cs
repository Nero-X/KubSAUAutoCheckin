using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AutoCheckin
{
    public class Schedule
    {
        [JsonProperty("model")]
        public List<Day> Days { get; set; }

        public Day Today => Days.Find(x => x.IsToday);

        public static Schedule FromJson(string json) => JsonConvert.DeserializeObject<Schedule>(json);
    }

    public class Pair
    {
        public string DisciplineName { get; set; }
        public int LessonNumber { get; set; }
        private DateTime start;
        private DateTime end;
        public DateTime CheckinDateStart { get => start; set => start = value.ToLocalTime(); }
        public DateTime CheckinDateEnd { get => end; set => end = value.ToLocalTime(); }
        public bool CheckedAtLesson { get; set; }
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
}

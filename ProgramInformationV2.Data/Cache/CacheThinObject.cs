using ProgramInformationV2.Data.DataModels;

namespace ProgramInformationV2.Data.Cache {

    public class CacheThinObject(string netid) {
        private const int _minutesValid = 60;

        public bool Expired => DateTime.Now > DateInvalid;
        public string? ItemId { get; set; }
        public string? ItemTitle { get; set; }
        public ScreenType? ScreenType { get; set; }
        public string? Source { get; set; }
        internal DateTime DateInvalid { get; set; } = DateTime.Now.AddMinutes(_minutesValid);
        internal string NetId { get; set; } = netid;

        public void Reset() => DateInvalid = DateTime.Now.AddMinutes(_minutesValid);
    }
}
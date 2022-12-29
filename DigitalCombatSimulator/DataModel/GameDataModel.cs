namespace DigitalCombatSimulator.DataModel
{
    internal class GameDataModel
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public string ServerIP { get; set; }
        public int ServerPort { get; set; }
        public int QuerySendPort { get; set; }
        public int QueryReceivePort { get; set; }
        public int TwoWayPort { get; set; }
        public bool EnableUpdater { get; set; }
        public string UpdaterPath { get; set; }
        public string UpdaterArguments { get; set; }
        public string RootPath { get; set; }
        public string ApplicationPath { get; set; }
        public bool IsSteamGame { get; set; }
        public string ServerVersion { get; set; }
        public string LatestVersion { get; set; }
        public bool UpdateLock { get; set; }
        public bool ForceUpdate { get; set; }
        public bool DedicatedAcknowledge { get; set; }
        public DateTime DedicatedAcknowledgeDate { get; set; }
        public DateTime DedicatedCompletedUpdated { get; set; }
        public List<string> Staff { get; set; }
        public int PlayerCount { get; set; }
        public int MostOnline { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime DateOfMostOnline { get; set; }
        public bool UpdateInProgress { get; set; }

    }
}

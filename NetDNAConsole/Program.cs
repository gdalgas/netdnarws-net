namespace NetDNAConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new NetDNARWS.Api("account alias", "consumer key","consumer secret");

            //var accountResult = api.Get("/account.json", true);            

            //var accountAddressResult = api.Get("/account.json/address");
            
            //var users = api.Get("/users.json");            

            //var zones = api.Get("/zones.json");

            //var zoneSummary = api.Get("/zones.json/summary");

            //var pullZonesCount = api.Get("/zones/pull.json/count");

            //var pullZones = api.Get("/zones/pull.json");

            //foreach (var pullZone in pullZones.data.pullzones)
            //{
            //    api.Delete("/zones/pull.json/" + pullZone.id + "/cache");
            //}                      

        }        

    }
}

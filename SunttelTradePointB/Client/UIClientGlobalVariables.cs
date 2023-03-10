using SunttelTradePointB.Shared.SquadsMgr;

namespace SunttelTradePointB.Client
{
    public static class UIClientGlobalVariables
    {
        public static string UserId { get; set; } = "";
        public static string UserName { get; set; } = "";
        public static string EntityUserId { get; set; } = "";

        public static string UserSkinImage { get; set; } = "";
        public static string PublicIpAddress { get; set; } = "";

        public static string UserCountryId { get; set; } = "";
        public static string UserLanguage { get; set; } = "";

        public static List<SquadsByUser>? CurrentUserSquads { get; set; }

        public static SquadsByUser? ActiveSquad { get; set; }


        static string _pathEntityImages = "/uploads/entityImages";
        static string _pathTransactionalItemsImages = "uploads/transactionalItemsImages";

        public static string PathEntityImages { 
            get { return _pathEntityImages; } 
            set { _pathEntityImages = value; } 
        } 
        public static string PathTransactionalItemsImages { get; set; } = "/uploads/transactionalItemsImages";


    }
}

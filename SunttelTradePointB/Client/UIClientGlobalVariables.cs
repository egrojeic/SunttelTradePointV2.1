using SunttelTradePointB.Shared.Common;
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

        public static List<Squad>? CurrentUserSquads { get; set; }

        public static Squad? ActiveSquad { get; set; }

        public static EntityActor? ActiveEntity { get; set; }


        public static string pathSquadsImages = "/SkinImagesByAccount";
        static string _pathEntityImages = "/uploads/entityImages";
        static string _pathTransactionalItemsImages = "uploads/transactionalItemsImages";
        public static string _pathQualityEvaluationImages = "uploads/qualityImages";

        public static string PathEntityImages
        {
            get { return _pathEntityImages; }
            set { _pathEntityImages = value; }
        }
        public static string PathTransactionalItemsImages { get; set; } = "/uploads/transactionalItemsImages";


    }
}

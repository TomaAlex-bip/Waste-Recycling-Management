namespace WasteRecyclingManagementApi.Core.Configuration
{
    public class EntityHelperConstants
    {
        public const int USERNAME_MAX_LENGTH = 50;
        public const int USERNAME_MIN_LENGTH = 2;
        public const int PASSWORD_MAX_LENGTH = 100;
        public const int PASSWORD_MIN_LENGTH = 4;

        public const decimal CONTAINER_MAX_SIZE = 1000;
        public const decimal CONTAINER_MIN_SIZE = (decimal)0.1;

        public const int WASTE_TYPE_MAX_LENGTH = 30;
        public const int WASTE_TYPE_MIN_LENGTH = 2;

        public const int MEASURE_UNIT_MIN_LENGTH = 1;
        public const int MEASURE_UNIT_MAX_LENGTH = 20;
        
        public const int RECYCLING_POINT_NAME_MAX_LENGTH = 100;
        public const int RECYCLING_POINT_NAME_MIN_LENGTH = 6;
        
        public const int USER_MIN_ROLE = 0;
        public const int USER_MAX_ROLE = 1;
        public const int MAX_ROLE = 2;
        
        public const double MIN_LATITUDE = -90;
        public const double MAX_LATITUDE = 90;

        public const double MIN_LONGITUDE = -180;
        public const double MAX_LONGITUDE = 180;
    }
}

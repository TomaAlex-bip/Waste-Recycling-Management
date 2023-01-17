using System.Reflection;
using System.Resources;

namespace WasteRecyclingManagementApi.Services.ErrorMessages
{
    public static class ErrorMessageHelper
    {
        private const string ERROR_FILE_NAMESPACE = "WasteRecyclingManagementApi.Services.ErrorMessages.ErrorTypesMessages";

        public static string GetUserNotFoundError(string username)
        {
            string error = GetErrorWithName("UserNameNotFound");
            return error.Replace("{0}", username);
        }

        public static string GetUserNotFoundError(int id)
        {
            string error = GetErrorWithName("UserNameNotFound");
            return error.Replace("{0}", id.ToString());
        }

        public static string GetContainerNotFoundError(string containerWasteType, string recyclingPointName)
        {
            string error = GetErrorWithName("ContainerNotFound");
            return error.Replace("{0}", containerWasteType)
                        .Replace("{1}", recyclingPointName);
        }

        public static string GetContainerNotFoundError(int id)
        {
            string error = GetErrorWithName("ContainerIdNotFound");
            return error.Replace("{0}", id.ToString());
        }

        public static string GetRecyclingPointNotFoundError(int id)
        {
            string error = GetErrorWithName("RecyclingPointIdNotFound");
            return error.Replace("{0}", id.ToString());
        }
        public static string GetRecyclingPointNotFoundError(string name)
        {
            string error = GetErrorWithName("RecyclingPointNameNotFound");
            return error.Replace("{0}", name);
        }

        public static string GetRecyclingPointDuplicateError(string name)
        {
            string error = GetErrorWithName("RecyclingPointDuplicate");
            return error.Replace("{0}", name);
        }

        public static string GetContainerDuplicateError(string containerType, int recyclingPointId)
        {
            string error = GetErrorWithName("ContainerDuplicate");
            return error.Replace("{0}", containerType)
                        .Replace("{1}", recyclingPointId.ToString());
        }

        public static string GetUserDuplicateError(string username)
        {
            string error = GetErrorWithName("UserDuplicate");
            return error.Replace("{0}", username);
        }

        public static string GetContainerCapacityError()
        {
            string error = GetErrorWithName("ContainerCapacity");
            return error;
        }

        public static string GetTokenToProvidedError()
        {
            string error = GetErrorWithName("TokenNotProvided");
            return error;
        }

        private static string GetErrorWithName(string name)
        {
            ResourceManager rm = new ResourceManager(ERROR_FILE_NAMESPACE, Assembly.GetExecutingAssembly());
            var message = rm.GetString(name);
            if (message == null)
                return string.Empty;

            return message;
        }

    }
}

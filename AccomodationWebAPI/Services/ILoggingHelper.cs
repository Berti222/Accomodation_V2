using AccomodationWebAPI.CustomExceptions;

namespace AccomodationWebAPI.Services
{
    public interface ILoggingHelper
    {
        void LogGeneralException(Exception exception, string controllerName, string methodName);
        void LogHttpException(HTTPStatusException exception, string controllerName, string methodName);
        void LogObjectPassedAsParameter(string controllerName, string methodName, object obj);
        void LogObjectReturnedAtCreateOrUpdate(string controllerName, string methodName, object obj);
        void LogInformation(string message, params object[] parameters);
    }
}
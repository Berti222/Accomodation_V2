using AccomodationWebAPI.CustomExceptions;
using Serilog;

namespace AccomodationWebAPI.Services
{
    public class LoggingHelper : ILoggingHelper
    {
        public void LogObjectReturnedAtCreateOrUpdate(string methodName, string controllerName, object obj)
           => Log.Information("{controllerName}/{methodeName} action was successful! Object returned: {@obj}", controllerName, methodName, obj);

        public void LogObjectPassedAsParameter(string controllerName, string methodName, object obj)
            => Log.Information("{controllerName}/{methodeName} Called with object passed: {@obj}", controllerName, methodName, obj);

        public void LogHttpException(HTTPStatusException exception, string controllerName, string methodName)
            => Log.Error(exception, "An HTTP exception occured at {controllerName}/{methodName} method. Statuscode: {statuscode}",
                                    controllerName, methodName, exception.StatusCode.ToString());
        public void LogGeneralException(Exception exception, string controllerName, string methodName)
           => Log.Error(exception, "An Exception occured at {controllerName}/{methodName} method.",
                                    controllerName, methodName);

        public void LogInformation(string message, params object[] parameters)
            => Log.Information(message, parameters);
    }
}

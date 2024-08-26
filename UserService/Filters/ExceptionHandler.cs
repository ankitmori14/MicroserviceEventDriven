using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Filters
{
    /// <summary>
    /// This class is exception handler
    /// </summary>
    public sealed class ExceptionHandler : ExceptionFilterAttribute
    {

        /// <summary>
        /// httpcontext object
        /// </summary>
        private readonly IHttpContextAccessor httpContextAccessor = null;

        /// <summary>
        /// Exception handler constructor
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public ExceptionHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Onexception handler
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(ExceptionContext actionExecutedContext)
        {
            ServiceResponseError cpResponse = new ServiceResponseError();
            cpResponse.Status = "Error";
            cpResponse.Message = actionExecutedContext.Exception.Message;
            try
            {
                //SerilogManager.Error(actionExecutedContext.Exception.Message, actionExecutedContext.Exception);
            }
            catch (Exception ex)
            {
                //SerilogManager.Error(ex.Message, ex);
            }
            finally
            {
                actionExecutedContext.Result = new JsonResult(string.Empty)
                {
                    Value = cpResponse
                };
            }
        }
    }
}

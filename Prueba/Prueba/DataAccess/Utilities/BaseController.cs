using Microsoft.Extensions.Logging;
using System.Web.Http;

namespace Prueba.DataAccess.Utilities
{
    public class BaseController<TEntity> : ApiController where TEntity : EntityBase
    {
        public virtual IHttpActionResult Return(object data, Exception ex = null)
        {
            ResultObject result = null;

            if ((ex != null) || (data == null))
                result = Catch(ex, data);
            else
            {
                return Ok(data);
            }

            return Ok(result);
        }

        public ResultObject Catch(Exception ex, object obj)
        {
            var internalEx = GetError(ex);
            //System.Data.SqlClient.SqlException
            var message = internalEx.Message;
            var validationUI = (internalEx is BusinessException) ? (internalEx as BusinessException).Message : null;


            if ((internalEx is System.Data.Entity.Validation.DbEntityValidationException) &&
                (internalEx as System.Data.Entity.Validation.DbEntityValidationException).EntityValidationErrors.Count() > 0)
            {
                message = string.Empty;
                foreach (var error in (internalEx as System.Data.Entity.Validation.DbEntityValidationException).EntityValidationErrors)
                {
                    message += $"Validacion Entidad:{error.Entry.Entity.GetType().Name}{Environment.NewLine}";
                    foreach (var _error in error.ValidationErrors)
                        message += $"{_error.PropertyName}:{_error.ErrorMessage}{Environment.NewLine}";
                }
            }
            ResultObject result = new ResultObject()
            {
                Ok = false,
                Error = message,
                TrazaError = $"Error: {internalEx.ToString()}, Traza:{internalEx.StackTrace}",
                IsAuthenticated = (internalEx is AuthorizationException),
                ValidationUI = validationUI

            };

            InitValues();

            Logger.Log(ex, obj);

            return result;
        }
    }
}

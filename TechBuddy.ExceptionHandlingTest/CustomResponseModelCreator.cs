using TechBuddy.Middlewares.ExceptionHandling;
using TechBuddy.Middlewares.ExceptionHandling.Infrastructure;

namespace TechBuddy.ExceptionHandlingTest
{
    public class CustomResponseModelCreator : IResponseModelCreator
    {
        public object CreateModel(ModelCreatorContext model)
        {
            return new 
            {
                  ExMes = model.ErrorMessage,
                  DetailedExMes = model.Exception.ToString()
            };
        }
    }
}

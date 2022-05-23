namespace Solari.Data.Api.Helpers
{
    public static class ModelStateError
    {
        public static string Create(dynamic modelState)
        {
            string message = "";

            foreach (dynamic modelStateKey in modelState.Keys)
            {
                dynamic modelStateVal = modelState[modelStateKey];

                foreach (dynamic error in modelStateVal.Errors)
                {
                    dynamic key = modelStateKey;
                    dynamic errorMessage = error.ErrorMessage;

                    message = $"{key}: {errorMessage}\n";
                }
            }

            return message;
        }
    }
}
